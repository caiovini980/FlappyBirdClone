using Base.Controllers;
using Obstacles;
using UnityEngine;

namespace Score
{
    public class ScoreController : ControllerBase
    {
        // VARIABLES
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
            _score = initialScore;
        }

        private void AddPointsToPlayer()
        {
            _score += scorePerObstacle;
            Debug.Log($"Player score is: {_score}");
        }

        private void SaveHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
            }

            // SAVE SCORE AND HIGHSCORE
        }
        
        // EVENTS
        private void SubscribeEvents()
        {
            PointTriggerBehaviour.OnScoreAdded += AddPointsToPlayer;
            ObstacleBehaviour.OnPlayerTouchedObstacle += SaveHighScore;
        }
        
        private void UnsubscribeEvents()
        {
            PointTriggerBehaviour.OnScoreAdded -= AddPointsToPlayer;
            ObstacleBehaviour.OnPlayerTouchedObstacle -= SaveHighScore;
        }
    }
}
