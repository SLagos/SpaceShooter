using UnityEngine;
using Data.WinCondition;

namespace Data.GameData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/Create GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField]
        private Data.WinCondition.WinConditionData _winCondition;
        public Data.WinCondition.WinConditionData WinCondition => _winCondition;
        [SerializeField]
        private float _timeBetweenSpawn;
        public float TimeBetweenSpawn => _timeBetweenSpawn;

        [SerializeField]
        private int _spawnsPerWave;
        public int SpawnsPerWave => _spawnsPerWave;
        [SerializeField]
        private float _timeBetweenWave;
        public float TimeBetweenWave => _timeBetweenWave;

        [SerializeField]
        private int _playersLives;

        public int PlayersLives => _playersLives;

        [SerializeField]
        private float _playerRespawnTime;
        public float PlayerRespawnTime=> _playerRespawnTime;

        [SerializeField]
        private Vector3 _playerSpawnPoint;
        public Vector3 PlayerSpawnPoint => _playerSpawnPoint;

        [SerializeField]
        private string _playerAddress;
        public string PlayerAddress => _playerAddress;
        [SerializeField]
        private string _levelId;
        public string LevelId => _levelId;

        [SerializeField]
        private string _keyHighScoreSavedData;
        public string KeyHighScoreSavedData => _keyHighScoreSavedData;
    }
}