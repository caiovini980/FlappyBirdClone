using System;
using Input;
using Obstacles;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private InputController inputController;
        [SerializeField] private float speed;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _physicsComponent;

        private bool _canMove = false; // true when countdown ends

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

        private void Update()
        {
            // if (!_canMove) return;
            transform.position += Vector3.right * (speed * Time.deltaTime);
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
            // stop time for a brief
            // play die sfx
            // impulse player up a little so it dies like mario 
            UnsubscribeEvents();
        }
        
        // SUBSCRIPTIONS
        private void SubscribeEvents()
        {
            inputController.OnInputHappened += Jump;
            ObstacleBehaviour.OnPlayerTouchedObstacle += Die;
        }

        private void UnsubscribeEvents()
        {
            inputController.OnInputHappened -= Jump;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= Die;
        }
    }
}
