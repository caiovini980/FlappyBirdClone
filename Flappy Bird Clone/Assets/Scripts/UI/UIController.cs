using Base.Controllers;
using Score;
using UnityEngine;
using TMPro;

namespace UI
{
    public class UIController : ControllerBase
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ScoreController scoreController;

        // METHODS
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
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            scoreText.text = "" + scoreController.GetCurrentScore();
        }
        
        // EVENTS

        private void SetupEvents()
        {
            scoreController.OnScoreUpdated += UpdateScoreUI;
        }
        
        private void DisableEvents()
        {
            scoreController.OnScoreUpdated -= UpdateScoreUI;
        }
    }
}
