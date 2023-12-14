using System;
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
        
        // VARIABLES
        private BoxCollider2D _collider;
        
        // METHODS
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        // EVENTS
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.isTrigger || !other.gameObject.TryGetComponent(out PlayerBehaviour player))
            {
                return;
            }

            OnPlayerTouchedObstacle?.Invoke();
            // Kill player
        }
    }
}
