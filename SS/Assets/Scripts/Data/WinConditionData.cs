using UnityEngine;

namespace Data.WinCondition
{
    public abstract class WinConditionData : ScriptableObject
    {
        public abstract bool IsConditionMeet();
    }
}