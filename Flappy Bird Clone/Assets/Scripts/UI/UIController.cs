using System.Collections;
using Base.Controllers;
using Score;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Gameplay;
using Obstacles;
using SFX;
using Utils.TimeUtils;

namespace UI
{
    public class UIController : ControllerBase
    {
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject endGameUI;
        [SerializeField] private SfxController sfxController;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private GameController gameController;
        [SerializeField] private CountdownHandler countdownHandler;
        [Space(10)] 
        [SerializeField] private AudioClip selectSound;
        
        private RectTransform _menuRectTransform;
        private RectTransform _endGameRectTransform;
        
        private readonly float _menuFadeInTime = 1f;
        private readonly float _menuFadeOutTime = .5f;
        private readonly float _endGameFadeInTime = .5f;
        private readonly float _endGameFadeOutTime = 1f;

        private readonly float _minYPosition = -2000f;
        private readonly float _timeToEnableScore = 1f;
        private readonly float _timeToEnableInGameUI = 1f;

        private Coroutine _enableScoreCoroutine;
        private Coroutine _enableInGameUICoroutine;

        // OVERRIDES
        protected override void AwakeController()
        {
            _menuRectTransform = mainMenuUI.GetComponent<RectTransform>();
            _endGameRectTransform = endGameUI.GetComponent<RectTransform>();
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
            EnterMainMenuAnimation();
        }

        // METHODS
        private void UpdateScoreUI()
        {
            scoreText.text = "" + scoreController.GetCurrentScore();
        }
        
        private void UpdateHighScoreUI()
        {
            highScoreText.text = "" + scoreController.GetHighScore();
        }

        private void UpdateTimerUI(float seconds)
        {
            timerText.gameObject.SetActive(true);
            
            float secondsOffset = 1;
            float newSeconds = seconds - secondsOffset;

            if (newSeconds > 0)
            {
                timerText.text = "" + newSeconds;
                return;
            }
            
            timerText.text = "GO!";
            _enableScoreCoroutine = StartCoroutine(WaitToEnableScore());
        }

        private void EnterMainMenuAnimation()
        {
            EnableMainMenuUI();
            _menuRectTransform.transform.localPosition = new Vector3(0f, _minYPosition, 0f);
            _menuRectTransform.DOAnchorPos(
                new Vector2(0, 0), 
                _menuFadeInTime).SetEase(Ease.InQuint);
        }
        
        private void ExitMainMenuAnimation()
        {
            sfxController.PlayAudio(selectSound);
            _menuRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
            _menuRectTransform.DOAnchorPos(
                new Vector2(0, _minYPosition), 
                _menuFadeOutTime).SetEase(Ease.InQuint);
        }

        private void EnterGameOverAnimation()
        {
            UpdateHighScoreUI();
            EnableEndGameUI();
            
            _endGameRectTransform.transform.localPosition = new Vector3(0f, _minYPosition, 0f);
            _endGameRectTransform.DOAnchorPos(
                new Vector2(0, 0), 
                _endGameFadeInTime).SetEase(Ease.InQuint);
        }

        public void ExitGameOverAnimation()
        {
            scoreText.gameObject.SetActive(false);
            
            _endGameRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
            _endGameRectTransform.DOAnchorPos(
                new Vector2(0, _minYPosition), 
                _endGameFadeOutTime).SetEase(Ease.InQuint);
        }

        private void StartGame()
        {
            UpdateScoreUI();
            _enableInGameUICoroutine = StartCoroutine(WaitToEnableInGameUI());
        }
        
        private void EnableInGameUI()
        {
            mainMenuUI.SetActive(false);
            inGameUI.SetActive(true);
            endGameUI.SetActive(false);
        }
        
        private void EnableMainMenuUI()
        {
            mainMenuUI.SetActive(true);
            inGameUI.SetActive(false);
            endGameUI.SetActive(false);
        }
        
        private void EnableEndGameUI()
        {
            mainMenuUI.SetActive(false);
            inGameUI.SetActive(false);
            endGameUI.SetActive(true);
        }
        
        // EVENTS
        private void SetupEvents()
        {
            scoreController.OnScoreUpdated += UpdateScoreUI;
            gameController.OnGameStarted += StartGame;
            
            countdownHandler.OnTimerFinished += EnableInGameUI;
            countdownHandler.OnTimerUpdated += UpdateTimerUI;
            
            ObstacleBehaviour.OnPlayerTouchedObstacle += EnterGameOverAnimation;
        }
        
        private void DisableEvents()
        {
            scoreController.OnScoreUpdated -= UpdateScoreUI;
            gameController.OnGameStarted -= StartGame;
            
            countdownHandler.OnTimerFinished -= EnableInGameUI;
            countdownHandler.OnTimerUpdated -= UpdateTimerUI;
            
            ObstacleBehaviour.OnPlayerTouchedObstacle -= EnterGameOverAnimation;
        }
        
        // COROUTINES
        IEnumerator WaitToEnableScore()
        {
            timerText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_timeToEnableScore);
            scoreText.gameObject.SetActive(true);
        }

        IEnumerator WaitToEnableInGameUI()
        {
            ExitMainMenuAnimation();
            yield return new WaitForSeconds(_timeToEnableInGameUI);
            EnableInGameUI();
        }
    }
}
