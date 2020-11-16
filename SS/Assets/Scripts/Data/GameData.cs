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
    }
}