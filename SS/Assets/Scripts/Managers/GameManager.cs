using UnityEngine;
using Managers.Transition;
using Data.GameData;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Controllers.Enemy;
using Systems.PoolSystem;
using Controllers.Player;

namespace Managers.Game
{
    public class GameManager : BaseManager
    {
        public delegate void ScoreUpdate(int score);
        public static event ScoreUpdate OnScoreUpdate;

        public delegate void LivesUpdate(int lives);
        public static event LivesUpdate OnLivesUpdate;

        [SerializeField]
        private List<SpawnPatternData> _spawnPatterns;
        [SerializeField]
        private AssetReference _gameDataRef;
        private float _timePlayed;
        public float TimePlayed => _timePlayed;
        private int _currentWave = 0;
        public int CurrentWave => _currentWave;
        private static bool _isPaused = true;
        public static bool IsPaused => _isPaused;

        private GameData _data;
        public GameData Data => _data;
        private int _score;

        private static bool _gameOver = false;
        public static bool IsGameOver => _gameOver;

        private float _nextSpawn = 0;
        private int _currentSpawn = 0;

        private int _currentScore;
        public int CurrentScore => _currentScore;

        private int _currentLives;
        public int CurrentLifes => _currentLives;

        private PlayerController _playerController;
        public override void OnAwake()
        {
            ManagerProvider.RegisterManager(this, _priority);
        }

        public override async Task Init()
        {
            AsyncOperationHandle<GameData> op = Addressables.LoadAssetAsync<GameData>(_gameDataRef);
            op.Completed += (o) =>
            {
                _data = o.Result;
            };
            await op.Task;
            _initialized = true;
        }

        public async Task StartGame()
        {
            TransitionManager tM = ManagerProvider.GetManager<TransitionManager>();
            await tM.LoadSceneAsync(_data.LevelId);

            var op = Addressables.LoadAssetAsync<GameObject>(_data.PlayerAddress);
            await op.Task;
            Addressables.InstantiateAsync(_data.PlayerAddress).Completed += (obj)=>
            {
                _playerController = obj.Result.GetComponent<PlayerController>();
            }; //This is already preloaded due "LoadAssetAsync" so its in memory
            _timePlayed = 0f;
            _score = 0;
            _currentSpawn = 0;
            _currentWave = 0;
            _nextSpawn = 0;
            _isPaused = false;
            _gameOver = false;
            _currentLives = _data.PlayersLives;
            OnLivesUpdate(_currentLives);

        }

        public void AddScore(int score)
        {
            _currentScore += score;
            if (OnScoreUpdate != null)
                OnScoreUpdate.Invoke(_currentScore);
        }

        public void GameOver()
        {
            if (_gameOver)
                return;
            TransitionManager tM = ManagerProvider.GetManager<TransitionManager>();
            tM.LoadSceneAdditiveAsync("GameOver");
            _gameOver = true;
        }

        private void Update()
        {
            if(!_isPaused && !_gameOver)
            {
                _timePlayed += Time.deltaTime;
                if(_data.WinCondition.IsConditionMeet())
                {
                    GameOver();
                }
                if(_timePlayed>=_nextSpawn)
                {
                    int index = Random.Range(0, _spawnPatterns.Count);
                    SpawnEnemys(_spawnPatterns[index]);
                    _nextSpawn = _timePlayed + _data.TimeBetweenSpawn;
                    _currentSpawn++;
                    if (_currentSpawn >= _data.SpawnsPerWave)
                    {
                        NextWave();
                    }

                }
            }
        }

        internal void PlayerDie()
        {
            _currentLives--;
            if (OnLivesUpdate != null)
                OnLivesUpdate(_currentLives);
            if (_currentLives > 0)
            {
                StartCoroutine(_playerController.RespawnRoutine());
            }
            else
                GameOver();
        }

        private void SpawnEnemys(SpawnPatternData pattern)
        {
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            foreach (var pos in pattern.Patterns)
            {
                pm.Spawn<EnemyController>(EPool.Enemy1, pos, Quaternion.Euler(0,0,180)).gameObject.SetActive(true);
            }
        }

        private void NextWave()
        {
            _currentWave++;
            _currentSpawn = 0;
            _nextSpawn = _timePlayed + _data.TimeBetweenWave;
        }
    }
}