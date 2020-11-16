using Managers.Game;
using Managers;
using UnityEngine;

namespace Data.WinCondition
{
    [CreateAssetMenu(fileName = "TimedWinConditionData", menuName = "Data / Create TimedWinCondition", order = 0)]
    public class TimedWinConditionData : WinConditionData
    {
        [TooltipAttribute("Time in seconds that the player need to be alive to win")]
        [SerializeField]
        private int _timeToSurvive;

        private GameManager _manager;
        public override bool IsConditionMeet()
        {
            if (_manager == null)
                _manager = ManagerProvider.GetManager<GameManager>();
            return _manager.TimePlayed >= _timeToSurvive;
        }
    }
}