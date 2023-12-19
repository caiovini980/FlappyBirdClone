using Gameplay;
using Input;
using Obstacles;
using ScriptableObjects;
using UnityEngine;
using Utils.TimeUtils;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private InputController inputController;
        [SerializeField] private CountdownHandler countdownHandler;
        [SerializeField] private GameController gameController;
        [SerializeField] private float speed;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _physicsComponent;
        private CircleCollider2D _collider;

        private bool _canMove; 

        private void Awake()
        {
            _physicsComponent = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();

            if (playerInfo == null)
            {
                Debug.LogError("Can't find Player Info reference for setup.\nPlease add one.");
            }

            _canMove = false;
        }

        // METHODS
        private void Start()
        {
            SetupPlayer();
            gameController.OnGameStarted += StartGame;
        }

        private void Update()
        {
            if (!_canMove) return;
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }

        private void StartGame()
        {
            _collider.enabled = true;
            SubscribeEvents();
        }

        private void SetupPlayer()
        {
            _spriteRenderer.sprite = playerInfo.playerSprite;
            gameObject.transform.position = Vector3.zero;
            _physicsComponent.simulated = false; 
        }
        
        private void Jump()
        {
            if (!_canMove) return;
            _physicsComponent.velocity = Vector2.up * playerInfo.jumpForce;
        }

        private void Die()
        {
            Debug.Log("Player Died!");
            Jump();
            // stop time for a brief
            // play die sfx
            _canMove = false;
            _collider.enabled = false;
            UnsubscribeEvents();
        }
        
        private void EnableMovement()
        {
            Debug.Log("ENABLING MOVEMENT!");
            _physicsComponent.simulated = true;
            _canMove = true;
        }
        
        // SUBSCRIPTIONS
        private void SubscribeEvents()
        {
            gameController.OnGameStarted -= StartGame;
            
            inputController.OnInputHappened += Jump;
            countdownHandler.OnTimerFinished += EnableMovement;
            ObstacleBehaviour.OnPlayerTouchedObstacle += Die;
        }

        private void UnsubscribeEvents()
        {
            inputController.OnInputHappened -= Jump;
            countdownHandler.OnTimerFinished -= EnableMovement;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= Die;
        }
    }
}
