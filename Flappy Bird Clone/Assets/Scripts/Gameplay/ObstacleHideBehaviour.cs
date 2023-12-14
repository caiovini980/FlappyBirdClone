using Obstacles;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ObstacleHideController : MonoBehaviour
    {
        [SerializeField] private ObjectPool pool;
        
        // EVENTS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ObstacleBehaviour obstacle))
            {
                pool.ReturnObjectToThePool(other.transform.parent.gameObject);
            }
        }
    }
}
