using Obstacles;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ObstacleHideController : MonoBehaviour
    {
        public delegate void ReturnObstacle();

        public event ReturnObstacle OnReturnObstacle;
        
        [SerializeField] private ObjectPool pool;
        
        // EVENTS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ObstacleBehaviour obstacle))
            {
                pool.ReturnObjectToThePool(other.transform.parent.gameObject);
                OnReturnObstacle?.Invoke();
            }
        }
    }
}
