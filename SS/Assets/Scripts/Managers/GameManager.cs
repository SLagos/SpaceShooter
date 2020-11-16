using System.Collections;
using UnityEngine;
using Managers;
using Managers.Transition;
using Data.GameData;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Controllers.Enemy;

namespace Managers.Game
{
    public class GameManager : BaseManager
    {
        [SerializeField]
        private List<SpawnPatternData> _spawnPatterns;
        private float _timePlayed;
        public float TimePlayed => _timePlayed;
        private int _currentWave = 0;
        public int CurrentWave => _currentWave;
        private static bool _isPaused = true;
        public static bool IsPaused => _isPaused;

        private GameData _data;
        private int _score;

        private static bool _gameOver = false;
        public static bool GameOver => _gameOver;

        private float _nextSpawn = 0;
        private int _currentSpawn = 0;
        public override void OnAwake()
        {
            ManagerProvider.RegisterManager(this, _priority);
        }

        public override async Task Init()
        {
            AsyncOperationHandle<GameData> op = Addressables.LoadAssetAsync<GameData>("GameData");
            op.Completed += (o) =>
            {
                _data = o.Result;
            };
            await op.Task;
        }

        public void StartGame()
        {
            _timePlayed = 0f;
            _score = 0;
            _isPaused = false;
            TransitionManager tM = ManagerProvider.GetManager<TransitionManager>();
            tM.LoadSceneAsync("Level1"); //not using Await due Fire and Forget
        }

        private void Update()
        {
            if(!_isPaused && !_gameOver)
            {
                _timePlayed += Time.deltaTime;
                if(_data.WinCondition.IsConditionMeet())
                {
                    TransitionManager tM = ManagerProvider.GetManager<TransitionManager>();
                    tM.LoadSceneAdditiveAsync("GameOver");
                    _gameOver = true;
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

        private void SpawnEnemys(SpawnPatternData pattern)
        {
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            foreach (var pos in pattern.Patterns)
            {
                pm.Spawn<EnemyController>(PoolManager.EPool.Enemy1, pos, Quaternion.Euler(0,0,180));
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