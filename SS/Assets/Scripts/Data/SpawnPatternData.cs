using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "SpawnPatternData", menuName = "Data / Create SpawnPatternData", order = 0)]
    public class SpawnPatternData : ScriptableObject
    {
        [SerializeField]
        private List<Vector3> _patterns;

        public List<Vector3> Patterns => _patterns;
    }
}