using Controllers.Entity;
using Data.StatsData;
using System.Collections;
using UnityEngine;

namespace Controllers.StatsNS
{
    [RequireComponent(typeof(EntityController))]
    public class StatsController : MonoBehaviour
    {
        //TODO: Move this to addressables
        [SerializeField]
        private StatsData _statsData;

        private Stats _currentStats;

        private EntityController _controller;

        private void Awake()
        {
            _controller = GetComponent<EntityController>();
            _currentStats = _statsData.GetStats();
        }

        public float GetCurrentSpeed()
        {
            return _currentStats.Speed;
        }

        public int GetcurrentLife()
        {
            return _currentStats.Life;
        }

        public float GetCurrentFireRate()
        {
            return _currentStats.FireRate;
        }

        public void ApplyDamage(int damage)
        {
            _currentStats.Life--;
            if(_currentStats.Life <=0)
            {
                Despawn();
            }
        }

        private void Despawn()
        {
            _controller.Despawn();
        }
    }
}