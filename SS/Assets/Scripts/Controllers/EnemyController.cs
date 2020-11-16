using Controllers.Entity;
using Controllers.StatsNS;
using System.Collections;
using UnityEngine;

namespace Controllers.Enemy
{
    [RequireComponent(typeof(StatsController))]
    public class EnemyController : EntityController
    {
        private StatsController _statsController;

        public override void Despawn()
        {
            Destroy(gameObject);
            //TODO: Add Explosion VFX
        }

        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
        }
    }
}