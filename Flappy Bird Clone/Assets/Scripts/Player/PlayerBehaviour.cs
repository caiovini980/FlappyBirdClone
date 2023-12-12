using System;
using Input;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;

        private InputController _inputController;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _physicsComponent;

        private void Awake()
        {
            _inputController = FindFirstObjectByType<InputController>();
            _physicsComponent = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (!_inputController)
            {
                Debug.LogError("Can't find InputController on Hierarchy.\nPlease add one.");
            }

            if (!playerInfo)
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
        
        // SUBSCRIPTIONS
        private void SubscribeEvents()
        {
            _inputController.OnInputHappened += Jump;
        }

        private void UnsubscribeEvents()
        {
            _inputController.OnInputHappened -= Jump;
        }
    }
}
