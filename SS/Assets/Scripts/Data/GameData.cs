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
    }
}