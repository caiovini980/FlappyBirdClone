using Base.Controllers;
using Input;
using Obstacles;
using SFX;
using UnityEngine;
using Utils.TimeUtils;

namespace Gameplay
{
    public class GameController : ControllerBase
    {
        public delegate void GameStarted();

        public event GameStarted OnGameStarted;
        
        [SerializeField] private InputController inputController;
        [SerializeField] private SfxController sfxController;
        [SerializeField] private CountdownHandler countdownHandler;
        [Space(10)] 
        [SerializeField] private AudioClip restartAudio;

        private bool _hasGameStarted = false;
        
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
            sfxController.PlayAudio(restartAudio);
            StartGame();
        }

        private void StartGame()
        {
            if (!_hasGameStarted)
            {
                _hasGameStarted = true;
                OnGameStarted?.Invoke();
                countdownHandler.StartCountdown(4);
                
                inputController.OnInputHappened -= StartGame;
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
