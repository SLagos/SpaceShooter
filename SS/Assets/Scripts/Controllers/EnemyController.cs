using Controllers.Entity;
using Controllers.StatsNS;
using Controllers.Weapons;
using Managers;
using Managers.Game;
using Systems.DamageSystem;
using Systems.PoolSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers.Enemy
{
    [RequireComponent(typeof(StatsController))]
    public class EnemyController : EntityController, IDamageable
    {
        private StatsController _statsController;

        private Vector3 _currentVelocity = Vector3.zero;
        [SerializeField]
        private float _smoothFactor = 1f;
        private float _nextShoot;

        private WeaponsController _wpController;

        private bool _wasOnScreen = false;
        private bool _givePoints = true;

        [SerializeField]
        private CollisionHandler _cHandler;

        public override void Despawn()
        {
            gameObject.SetActive(false);
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            pm.Despawn(EPool.Enemy1, gameObject);
            if(_givePoints)
            {
                ManagerProvider.GetManager<GameManager>().AddScore(_statsController.GetCurrentPoints());
            }
            //TODO: Add Explosion VFX
        }
        private void FixedUpdate()
        {
            if (!GameManager.IsPaused && !GameManager.IsGameOver)
            {
                Vector3 nextPos = transform.position + (Vector3.down * _statsController.GetCurrentSpeed());
                transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref _currentVelocity, _smoothFactor);
                _nextShoot += Time.fixedDeltaTime;
                Fire();
            }
            if(GameManager.IsGameOver)
            {
                _givePoints = false;
                Despawn();
            }
        }

        private void Fire()
        {
            if (_wpController == null)
                return;
            if(_nextShoot >= _statsController.GetCurrentFireRate())
            {
                _wpController.Fire();
                _nextShoot = 0;
            }

        }

        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
            _wpController = GetComponent<WeaponsController>();
            _nextShoot = 0;
            _cHandler.Init(this);
        }

        void OnBecameInvisible()
        {
            if(_wasOnScreen)
            {
                _givePoints = false;
                Despawn();
            }
                
        }

        private void OnBecameVisible()
        {
            _wasOnScreen = true;
        }

        private void OnEnable()
        {
            _givePoints = true;
        }

        public void ReceiveDamage(DamageInfo info)
        {
            _statsController.ApplyDamage(info.Damage);
        }
    }
}