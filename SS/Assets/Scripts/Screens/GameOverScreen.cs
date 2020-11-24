using Data.GameData;
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

        private GameManager _gManager;
        private GameManager GManager
        {
            get
            {
                if (_gManager == null)
                    _gManager = ManagerProvider.GetManager<GameManager>();
                return _gManager;
            }
        }

        private void Start()
        {
            string scoreKey = GManager.Data.KeyHighScoreSavedData;
            int highScore = PlayerPrefs.HasKey(scoreKey) ? PlayerPrefs.GetInt(scoreKey) : 0;
            int currentScore = ManagerProvider.GetManager<GameManager>().CurrentScore;
            _highScoreLabel.text = (highScore> currentScore )?highScore.ToString(): currentScore.ToString();
            _scoreLabel.text = currentScore.ToString();
            _exitBtn.onClick.AddListener(ExitGame);
            replayBtn.onClick.AddListener(Replay);
            _newRecord.SetActive(currentScore > highScore);
            if (currentScore > highScore)
                PlayerPrefs.SetInt(scoreKey, currentScore);
        }

        private void ExitGame()
        {
            Application.Quit(0);
        }

        private async void Replay()
        {
            await ManagerProvider.GetManager<TransitionManager>().UnloadLastScene();
            await GManager.StartGame();
        }
    }
}