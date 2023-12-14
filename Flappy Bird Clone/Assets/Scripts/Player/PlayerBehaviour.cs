using Input;
using Obstacles;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _physicsComponent;

        private void Awake()
        {
            _physicsComponent = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (playerInfo == null)
            {
                Debug.LogError("Can't find Player Info reference for setup.\nPlease add one.");
            }
        }

        // METHODS
        private void Start()
        {
            SetupPlayer();
            SubscribeEvents();
        }

        private void SetupPlayer()
        {
            _spriteRenderer.sprite = playerInfo.playerSprite;
            gameObject.transform.position = Vector3.zero;
            // _physicsComponent.simulated = false; // enable when countdown ends
        }
        
        private void Jump()
        {
            _physicsComponent.velocity = Vector2.up * playerInfo.jumpForce;
        }

        private void Die()
        {
            Debug.Log("Player Died!");
            UnsubscribeEvents();
        }
        
        // SUBSCRIPTIONS
        private void SubscribeEvents()
        {
            InputController.OnInputHappened += Jump;
            ObstacleBehaviour.OnPlayerTouchedObstacle += Die;
        }

        private void UnsubscribeEvents()
        {
            InputController.OnInputHappened -= Jump;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= Die;
        }
    }
}
