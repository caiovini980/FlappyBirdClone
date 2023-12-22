using Player;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        // DELEGATES
        public delegate void PlayerTouchedObstacle();

        // EVENTS
        public static event PlayerTouchedObstacle OnPlayerTouchedObstacle;

        // EVENTS
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerBehaviour player))
            {
                return;
            }
            
            OnPlayerTouchedObstacle?.Invoke();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerBehaviour player))
            {
                OnPlayerTouchedObstacle?.Invoke();
            }
        }
    }
}
