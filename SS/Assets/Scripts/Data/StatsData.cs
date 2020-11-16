﻿using UnityEngine;

namespace Data.StatsData
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Data/Create Stat", order = 0)]
    public class StatsData : ScriptableObject
    {
        [SerializeField]
        private Stats _data;
        public Stats Data => Data;

        /// <summary>
        /// This method its to get a Copy fo Stats struct so it can be handle/modify by the owner at will without
        /// having to worry of changing the original values
        /// Used to differentiate Max Life vs Current Life for example
        /// </summary>
        /// <returns></returns>
        public Stats GetStats()
        {
            Stats nstats = new Stats()
            {
                Life = _data.Life,
                Speed = _data.Speed
            };

            return nstats;
        }
        
    }
    [System.Serializable]
    public struct Stats
    {
        public int Life;
        public float Speed;
    }
}