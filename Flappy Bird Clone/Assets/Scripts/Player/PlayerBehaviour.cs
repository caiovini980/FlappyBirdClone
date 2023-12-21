using System.Collections;
using Gameplay;
using Input;
using Obstacles;
using ScriptableObjects;
using SFX;
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
        [SerializeField] private SfxController sfxController;
        [SerializeField] private float speed;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _physicsComponent;
        private CircleCollider2D _collider;

        private Coroutine _deathSoundCoroutine;

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
            gameController.OnGameStarted += StartGame;
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Update()
        {
            if (!_canMove) return;
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }

        private void StartGame()
        {
            SetupPlayer();
            _collider.enabled = true;
        }

        private void SetupPlayer()
        {
            Debug.Log("SETTING UP PLAYER");
            
            gameObject.transform.position = Vector3.zero;
            _physicsComponent.velocity = Vector2.zero;
            _spriteRenderer.sprite = playerInfo.playerSprite;
            _physicsComponent.simulated = false; 
        }
        
        private void Jump()
        {
            if (!_canMove) return;
            _physicsComponent.velocity = Vector2.up * playerInfo.jumpForce;
            sfxController.PlayAudio(playerInfo.jumpSound);
        }

        private void Die()
        {
            sfxController.PlayAudio(playerInfo.hitSound);
            Jump();
            _deathSoundCoroutine = StartCoroutine(WaitToPlayDeathSound());
            _canMove = false;
            _collider.enabled = false;
            gameController.OnGameStarted += StartGame;
        }
        
        private void EnableMovement()
        {
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
        
        // COROUTINES
        private IEnumerator WaitToPlayDeathSound()
        {
            yield return new WaitForSeconds(.5f);
            sfxController.PlayAudio(playerInfo.deathSound);
        }
    }
}
