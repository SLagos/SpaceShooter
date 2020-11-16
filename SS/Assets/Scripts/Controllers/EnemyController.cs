using Controllers.Entity;
using Controllers.StatsNS;
using Controllers.Weapons;
using Managers;
using Managers.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers.Enemy
{
    [RequireComponent(typeof(StatsController))]
    public class EnemyController : EntityController
    {
        private StatsController _statsController;

        private Vector3 _currentVelocity = Vector3.zero;
        [SerializeField]
        private float _smoothFactor = 1f;
        private float _nextShoot;

        private WeaponsController _wpController;

        private bool _wasOnScreen = false;
        private bool _givePoints = true;



        public override void Despawn()
        {
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            pm.Despawn(PoolManager.EPool.Enemy1, gameObject);
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            DoDamage(collision.gameObject, collision.GetContact(0).point);
            Despawn();
        }

        private void DoDamage(GameObject collidedGo, Vector3 hitPoint)
        {
            collidedGo.SendMessageUpwards("ApplyDamage", (int)(_statsController.GetcurrentLife()/2), SendMessageOptions.DontRequireReceiver);
            //Move this to a better system that use the poolsystem
            Addressables.InstantiateAsync("HitVfx1", position: hitPoint, Quaternion.identity);
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
    }
}