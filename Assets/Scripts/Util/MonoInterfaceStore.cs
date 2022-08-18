using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utility
{
    [Serializable]
    /// <summary>
    /// !! If your variable don't need to assign from inspector, just use normal type and initial it yourself.
    /// Store Type T and MonoBehaviour which set from inspector and will be convert into type T by GetComponent
    /// after the first call of .Value or after MakeCache was called.
    /// </summary>
    /// <typeparam name="T">Class type T</typeparam>
    public class MonoInterfaceStore<T> where T : class
    {
        [SerializeField, HideInInspector]
        protected Component m_Behaviour;
        /// <summary>
        /// Get behaviour reference, can be null if Value is change without calling SetReferenceBehaviour
        /// </summary>
        public Component ReferenceBehaviour => m_Behaviour;
        private T m_CachedValue;
        public bool IsValid => null != m_Behaviour && (null != m_CachedValue || null != m_Behaviour as T);
        public T Value
        {
            get
            {
                if (null == m_CachedValue && null != m_Behaviour)
                    m_CachedValue = m_Behaviour as T;
                return m_CachedValue;
            }
            set
            {
                m_Behaviour = null;
                m_CachedValue = value;
            }
        }
        public MonoInterfaceStore(Component component = null)
        {
            m_Behaviour = component;
        }
        /// <summary>
        /// Change reference behaviour and T value
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool SetReferenceBehaviour(Component component)
        {
            if (null == component)
            {
                m_CachedValue = null;
                m_Behaviour = null;
                return true;
            }
            T value = component as T;
            if (null == value)
                return false;
            m_Behaviour = component;
            m_CachedValue = value;
            return true;
        }
        public static MonoInterfaceStore<T>[] GetFromChildrens(Transform target)
        {
            return Array.ConvertAll(
                target.GetComponentsInChildren<T>(),
                item => new MonoInterfaceStore<T>
                {
                    m_Behaviour = item as Component
                }
            );
        }
    }
    /// <summary>
    /// Store interface type or any type you want that able to attach on MonoBehaviour object.
    /// </summary>
    public class MonoStoreAttribute : PropertyAttribute
    {
        public System.Type InterfaceType;
        public string Message;
        public MonoStoreAttribute(System.Type type, string message = null)
        {
            InterfaceType = type;
            Message = message;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Property drawer of MonoInterface attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(MonoStoreAttribute))]
    public class MonoStoreDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Get attribute
            MonoStoreAttribute propAttribute = attribute as MonoStoreAttribute;
            SerializedProperty behavior = property.FindPropertyRelative("m_Behaviour");

            // Define height of adding help box.
            float detailLabelHeight = EditorGUIUtility.singleLineHeight;

            // Create rect of adding help box base on receive rect (position) of unity ui.
            Rect labelRect = new Rect(position);
            // Set high as detailLabelHeight
            labelRect.height = detailLabelHeight;

            Rect propertyReact = new Rect(position);
            propertyReact.height -= detailLabelHeight;
            // and 
            propertyReact.y += detailLabelHeight;

            // Create rect to draw ObjectField
            Rect propRect = new Rect(
                position.x,
                // Add detailLabelHeight and vertical space to Y to make it draw below help box.
                position.y + detailLabelHeight + EditorGUIUtility.standardVerticalSpacing,
                position.width,
                EditorGUIUtility.singleLineHeight
            );
            EditorGUI.BeginProperty(position, label, property);
            string message = null == propAttribute.Message ? $"Type of {propAttribute.InterfaceType.Name}" : propAttribute.Message;

            // Draw help box, use input message or message generate by this script.
            EditorGUI.HelpBox(labelRect, message, MessageType.None);
            GUI.enabled = !Application.isPlaying;
            DrawMonoField(propAttribute, propRect, behavior, label);
            GUI.enabled = true;

            EditorGUI.EndProperty();
        }
        private void DrawMonoField(MonoStoreAttribute attribute, Rect rect, SerializedProperty property, GUIContent label)
        {
            // Draw object field and begin change check.
            EditorGUI.BeginChangeCheck();
            EditorGUI.ObjectField(rect, property, typeof(Component), label);
            if (EditorGUI.EndChangeCheck() && property.objectReferenceValue != null)
            {
                Type type = property.objectReferenceValue.GetType();
                if (!attribute.InterfaceType.IsAssignableFrom(type))
                {
                    // Get MonoBehaviour from objectReferenceValue.
                    Component target = property.objectReferenceValue as Component;
                    // Get first component that assignable type of 'InterfaceType'
                    property.objectReferenceValue = target.GetComponent(attribute.InterfaceType);
                }
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = base.GetPropertyHeight(property, label);
            return baseHeight + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
#endif

}
