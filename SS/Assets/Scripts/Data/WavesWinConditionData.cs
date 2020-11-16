using Managers;
using Managers.Game;
using UnityEngine;

namespace Data.WinCondition
{
    [CreateAssetMenu(fileName = "WavesWinConditionData", menuName = "Data / Create WavesWinCondition", order = 0)]
    public class WavesWinConditionData : WinConditionData
    {
        [TooltipAttribute("Waves that the player need to be alive to win")]
        [SerializeField]
        private int _wavesToSurvive;
        private GameManager _manager;
        public override bool IsConditionMeet()
        {
            if (_manager == null)
                _manager = ManagerProvider.GetManager<GameManager>();
            return _manager.CurrentWave > _wavesToSurvive;
        }
    }
}