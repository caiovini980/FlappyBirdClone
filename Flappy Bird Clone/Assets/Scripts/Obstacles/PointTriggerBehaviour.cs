using System;
using Player;
using UnityEngine;

namespace Obstacles
{
    public class PointTriggerBehaviour : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private bool _isPlayerOnTrigger;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        // EVENTS
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_collider.isTrigger || !other.TryGetComponent(out PlayerBehaviour player))
            {
                return;
            }
            
            _isPlayerOnTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_isPlayerOnTrigger)
            {
                // add points to player
                Debug.Log("Add points to the player!");
                _isPlayerOnTrigger = false;
            }
        }
    }
}
