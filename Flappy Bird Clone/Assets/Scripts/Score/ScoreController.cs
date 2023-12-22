using Base.Controllers;
using Gameplay;
using Obstacles;
using SFX;
using UnityEngine;

namespace Score
{
    public class ScoreController : ControllerBase
    {
        // DELEGATES
        public delegate void ScoreUpdated();
        
        // EVENTS
        public event ScoreUpdated OnScoreUpdated;
        
        // VARIABLES
        [SerializeField] private GameController gameController;
        [SerializeField] private SfxController sfxController;
        [SerializeField] private AudioClip scoreAudioClip;
        [Space(10)]
        [SerializeField] private int initialScore;
        [SerializeField] private int scorePerObstacle;
        
        private int _score;
        private int _highScore;

        // METHODS
        protected override void AwakeController()
        {
            // Try to get player highscore
            
        }

        protected override void EnableController()
        {
            SubscribeEvents();
        }

        protected override void DisableController()
        {
            // Save player Highscore
            UnsubscribeEvents();
        }

        protected override void StartController()
        {
            ResetScore();
        }

        private void AddPointsToPlayer()
        {
            _score += scorePerObstacle;
            sfxController.PlayAudio(scoreAudioClip);
            OnScoreUpdated?.Invoke();
        }

        private void SaveHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
            }

            // SAVE SCORE AND HIGHSCORE
            ResetScore();
        }

        private void ResetScore()
        {
            _score = initialScore;
        }
        
        // EVENTS
        private void SubscribeEvents()
        {
            gameController.OnGameStarted += ResetScore;
            PointTriggerBehaviour.OnPointTriggerActivated += AddPointsToPlayer;
            ObstacleBehaviour.OnPlayerTouchedObstacle += SaveHighScore;
        }

        private void UnsubscribeEvents()
        {
            gameController.OnGameStarted -= ResetScore;
            PointTriggerBehaviour.OnPointTriggerActivated -= AddPointsToPlayer;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= SaveHighScore;
        }
        
        // GETTERS
        public int GetCurrentScore()
        {
            return _score;
        }

        public int GetHighScore()
        {
            return _highScore;
        }
    }
}
