using Managers;
using Managers.Game;
using Presenter.Lives;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Screens.GameScreen
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreLabel;
        [SerializeField]
        private LivesPresenter _livesPresenter;

        private GameManager _gManager;

        private async void Start()
        {
            _gManager = ManagerProvider.GetManager<GameManager>();
            GameManager.OnScoreUpdate += UpdateScore;
            GameManager.OnLivesUpdate += UpdateLives;
            _scoreLabel.text = _gManager.CurrentScore.ToString();
            await new WaitUntil(() => _gManager.Initialized);
            await _livesPresenter.Init(_gManager.Data.PlayersLives);
        }

        private void UpdateLives(int lives)
        {
            _livesPresenter.UpdateLives(lives);
        }

        private void UpdateScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }

        private void OnDestroy()
        {
            GameManager.OnScoreUpdate -= UpdateScore;
            GameManager.OnLivesUpdate -= UpdateLives;
        }
    }
}