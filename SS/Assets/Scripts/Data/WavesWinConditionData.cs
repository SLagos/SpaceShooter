using UnityEngine;

namespace Data.WinCondition
{
    [CreateAssetMenu(fileName = "WavesWinConditionData", menuName = "Data / Create WavesWinCondition", order = 0)]
    public class WavesWinConditionData : WinConditionData
    {
        public override bool IsConditionMeet()
        {
            throw new System.NotImplementedException();
        }
    }
}