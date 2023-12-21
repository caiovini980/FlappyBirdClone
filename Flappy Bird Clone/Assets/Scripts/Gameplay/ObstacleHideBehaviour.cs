using Obstacles;
using UnityEngine;
using Utils.PoolingUtils;

namespace Gameplay
{
    public class ObstacleHideController : MonoBehaviour
    {
        // DELEGATES
        public delegate void ReturnObstacle();

        // EVENTS
        public event ReturnObstacle OnReturnObstacle;
        
        // VARIABLES
        [SerializeField] private ObjectPoolHandler pool;

        private float _offset;
        private float _worldWidth;
        
        // METHODS
        private void Start()
        {
            float aspect = (float) Screen.width / Screen.height;
            
            if (Camera.main != null)
            {
                float worldHeight = Camera.main.orthographicSize * 2;
                _worldWidth = worldHeight * aspect;
                _offset = _worldWidth * 0.18f;
            }
            
            SetupHidePosition();
        }

        private void SetupHidePosition()
        {
            Vector3 position = transform.position;
            position.x = -(_worldWidth) + _offset;
            
            transform.position = position;
        }

        // EVENTS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ObstacleBehaviour obstacle))
            {
                pool.AddToThePool(other.transform.parent.gameObject);
                OnReturnObstacle?.Invoke();
            }
        }
    }
}
