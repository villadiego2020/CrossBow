using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private bool m_IsSelfInit = true;
        [SerializeField] private List<GameObject> m_Pool = new List<GameObject>();
        [SerializeField] private int m_InitialPoolSize = 10;
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private string m_PoolName;

        #region Getter

        public string Name
        {
            get
            {
                return m_PoolName;
            }
        }

        #endregion

        private void Start()
        {
            if (m_IsSelfInit)
                Init(gameObject.transform);
        }

        #region Public 

        public void Init(Transform parent = null)
        {
            for (int i = 0; i < m_InitialPoolSize; i++)
            {
                GameObject g = CreateNewObject();

                if (g == null)
                {
                    Debug.LogError("Cannot create new object.");
                    continue;
                }

                if (parent != null)
                {
                    g.transform.SetParent(parent, false);
                }

                m_Pool.Add(g);
            }
        }

        public void Init(int initialPoolSize, GameObject prefab, string name, Transform parent = null)
        {
            m_InitialPoolSize = initialPoolSize;
            m_Prefab = prefab;
            m_PoolName = name;

            for (int i = 0; i < m_InitialPoolSize; i++)
            {
                GameObject g = CreateNewObject();

                if (g == null)
                {
                    Debug.LogError("Cannot create new object.");
                    continue;
                }

                if (parent != null)
                {
                    g.transform.SetParent(parent, false);
                }

                m_Pool.Add(g);
            }
        }

        public void Pool(GameObject g)
        {
            g.transform.SetParent(gameObject.transform, false);
            g.SetActive(false);

            m_Pool.Add(g);
        }

        public void Pool(GameObject g, bool isParent)
        {
            m_Pool.Add(g);

            g.SetActive(false);
        }

        public void Pool(List<GameObject> gList)
        {
            foreach (var g in gList)
            {
                g.SetActive(false);

                m_Pool.Add(g);
            }
        }
        public void Remove(List<GameObject> itemList)
        {
            if (itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    m_Pool.Remove(item);
                    Destroy(item);
                }
            }
        }
        public void Remove(GameObject item)
        {
            m_Pool.Remove(item);
            Destroy(item);
        }

        public void ClearPool()
        {
            if (m_Pool.Count > 0)
            {
                foreach (var item in m_Pool)
                {
                    Destroy(item);
                }
            }
            m_Pool = new List<GameObject>();
        }
        public GameObject GetObject()
        {
            if (m_Pool.Count > 0)
            {
                GameObject g = m_Pool[0];
                m_Pool.RemoveAt(0);
                g.transform.SetParent(null);
                g.SetActive(true);

                return g;
            }
            else
            {
                GameObject g = CreateNewObject();
                g.transform.SetParent(null);
                g.SetActive(true);

                return g;
            }
        }

        public GameObject GetObject(bool isParent)
        {
            if (m_Pool.Count > 0)
            {
                GameObject g = m_Pool[0];
                m_Pool.RemoveAt(0);
                g.SetActive(true);

                return g;
            }
            else
            {
                GameObject g = CreateNewObject(true);
                g.SetActive(true);

                return g;
            }
        }

        public void ResetPool()
        {
            m_Pool = new List<GameObject>();
        }

        public T GetObject<T>() where T : MonoBehaviour
        {
            return GetObject().GetComponent<T>();
        }

        public T GetObject<T>(bool isParent) where T : MonoBehaviour
        {
            return GetObject(true).GetComponent<T>();
        }
        #endregion

        #region Private

        private int current = 0;
        private GameObject CreateNewObject()
        {
            GameObject g = Instantiate(m_Prefab) as GameObject;

            g.name = g.name.Replace("(Clone)", " " + current++);
            g.SetActive(false);

            return g;
        }

        private GameObject CreateNewObject(bool isParent)
        {
            GameObject g = Instantiate(m_Prefab, m_Prefab.transform.parent) as GameObject;

            g.name = g.name.Replace("(Clone)", " " + current++);
            g.SetActive(false);

            return g;
        }
        #endregion
    }
}