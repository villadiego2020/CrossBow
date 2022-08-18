using Win.Character;
using Win.Enemy;
using Win.Input;
using Win.Obstacle;
using Win.UI;
using UnityEngine;

namespace Win.Ground
{
    public class GroundController : MonoBehaviour
    {
        public static GroundController Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    m_Instance = (GroundController)FindObjectOfType(typeof(GroundController));
                }

                return m_Instance;
            }
        }

        protected static GroundController m_Instance;

        [SerializeField] private float m_Margin = 0.7f;
        [SerializeField] private Collider m_GroundCollider;

        public float Margin { get { return m_Margin; } }
        public float BoundsMinX { get { return m_GroundCollider.bounds.min.x + m_Margin; } }
        public float BoundMaxX { get { return m_GroundCollider.bounds.max.x - m_Margin; } }
        public float BoundsMinZ { get { return m_GroundCollider.bounds.min.z; } }
        public float BoundMaxZ { get { return m_GroundCollider.bounds.max.z; } }
        public Bounds BoundSize { get { return m_GroundCollider.bounds; } }

        public bool IsStart { get; private set; }

        [SerializeField] private ObstacleBehavior[] m_Obstacles;
        [SerializeField] private EnemyBehavior m_EnemyBehavior;
        [SerializeField] private CharacterBehavior m_CharacterBehavior;

        private void Start()
        {
            UserInput.Instance.ESC = ExitGame;

            UIController.Instance.StartGame = StartGame;
            UIController.Instance.Timeout = EndGame;
        }

        public void StartGame()
        {
            for (int i = 0; i < m_Obstacles.Length; i++)
            {
                ObstacleBehavior obstacleBehavior = m_Obstacles[i];
                obstacleBehavior.Init();
            }

            m_EnemyBehavior.Init();
            m_CharacterBehavior.Init();

            IsStart = true;
        }

        public void EndGame()
        {
            Bullet[] bullets = FindObjectsOfType<Bullet>();

            for (int i = 0; i < bullets.Length; i++)
            {
                Bullet bullet = bullets[i];
                bullet.Return();
            }

            IsStart = false;
            UIController.Instance.EndGame();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
