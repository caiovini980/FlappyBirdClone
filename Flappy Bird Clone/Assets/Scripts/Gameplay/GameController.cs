using Base.Controllers;
using Input;
using Obstacles;
using UnityEngine;
using Utils.TimeUtils;

namespace Gameplay
{
    public class GameController : ControllerBase
    {
        public delegate void GameStarted();

        public event GameStarted OnGameStarted;
        
        [SerializeField] private InputController inputController;
        [SerializeField] private CountdownHandler countdownHandler;

        private bool _hasGameStarted = false;
        
        // Start is called before the first frame update
        protected override void AwakeController()
        {
        }

        protected override void EnableController()
        {
            SetupEvents();
        }

        protected override void DisableController()
        {
            DisableEvents();
        }

        protected override void StartController()
        {
        }

        public void RestartGame()
        {
            // play transition
            // replace player and camera
            // replace pipes
        }

        private void StartGame()
        {
            if (!_hasGameStarted)
            {
                _hasGameStarted = true;
                OnGameStarted?.Invoke();
                countdownHandler.StartCountdown(4);
            }
        }

        private void EndGame()
        {
            _hasGameStarted = false;
        }

        private void SetupEvents()
        {
            inputController.OnInputHappened += StartGame;
            ObstacleBehaviour.OnPlayerTouchedObstacle += EndGame;
        }

        private void DisableEvents()
        {
            inputController.OnInputHappened -= StartGame;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= EndGame;
        }
    }
}
