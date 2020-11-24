using Managers;
using Managers.Game;
using Managers.Transition;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.GameOver
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _exitBtn, replayBtn;

        [SerializeField]
        private TextMeshProUGUI _highScoreLabel, _scoreLabel;

        [SerializeField]
        private GameObject _newRecord;

        private void Start()
        {
            int highScore = PlayerPrefs.HasKey("score") ? PlayerPrefs.GetInt("score"): 0;
            int currentScore = ManagerProvider.GetManager<GameManager>().CurrentScore;
            _highScoreLabel.text = (highScore> currentScore )?highScore.ToString(): currentScore.ToString();
            _scoreLabel.text = currentScore.ToString();
            _exitBtn.onClick.AddListener(ExitGame);
            replayBtn.onClick.AddListener(Replay);
            _newRecord.SetActive(currentScore > highScore);
            if (currentScore > highScore)
                PlayerPrefs.SetInt("score", currentScore);
        }

        private void ExitGame()
        {
            Application.Quit(0);
        }

        private async void Replay()
        {
            await ManagerProvider.GetManager<TransitionManager>().UnloadLastScene();
            ManagerProvider.GetManager<GameManager>().StartGame();
        }
    }
}