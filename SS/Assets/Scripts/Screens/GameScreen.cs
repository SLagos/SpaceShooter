using Managers;
using Managers.Game;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Screens.GameScreen
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreLabel, _lifes;

        private GameManager _gManager;

        private void Start()
        {
            _gManager = ManagerProvider.GetManager<GameManager>();
            GameManager.OnScoreUpdate += UpdateScore;
            _scoreLabel.text = "Score: " + _gManager.CurrentScore.ToString();
        }
        private void UpdateScore(int score)
        {
            _scoreLabel.text = "Score: " + score.ToString();
        }

        private void OnDestroy()
        {
            GameManager.OnScoreUpdate -= UpdateScore;
        }
    }
}