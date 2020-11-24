using Controllers.Entity;
using Managers;
using Managers.Game;
using Systems.PoolSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Systems.DamageSystem;

namespace Controllers.Bullet
{
    /// <summary>
    /// This script handle the movement and the logic
    /// </summary>
    public class BulletController : EntityController, IDamageable
    {
        [SerializeField]
        private float _speed = 15f;
        [SerializeField]
        private float _lifeSpan = 3f;
        [SerializeField]
        private CollisionHandler _cHandler;

        private int _damage = 0;

        private Coroutine _destroyRoutine;

        /// <summary>
        /// Using fixed update to handle positions and collisions at the same rate to avoid issues
        /// </summary>
        private void FixedUpdate()
        {
            if (!GameManager.IsPaused && !GameManager.IsGameOver)
            {
                transform.position += (transform.up * _speed * Time.fixedDeltaTime);
            }
        }

        private void Awake()
        {
            //This is just to make sure that is deactivated once instantiated, but it should come inactive from prefab
            gameObject.SetActive(false);
        }

        public void Init(int damage, int layerowner)
        {
            _damage = damage;
            gameObject.layer= layerowner;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject go = gameObject.transform.GetChild(i).gameObject;
                go.layer = layerowner;
            }
            gameObject.SetActive(true);
            _destroyRoutine = StartCoroutine(DespawnRoutine());
            _cHandler.Init(this);
            _cHandler.OnCollision += OnCollision;
        }

        /// <summary>
        /// Easy way to check if this object is being render by any camera, so i can dispose it.
        /// <ref href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnBecameInvisible.html">Unity Doc</ref>
        /// This doesn't work on editor due to Editor's Cameras is always looking at the scene
        /// </summary>
        void OnBecameInvisible()
        {
            if (_destroyRoutine != null)
                StopCoroutine(_destroyRoutine);
            Despawn();
        }

        private void OnCollision(Collision collision)
        {
            if (_destroyRoutine != null)
                StopCoroutine(_destroyRoutine);
            var cHandler = collision.collider.GetComponent<CollisionHandler>();
            cHandler.NotifyCollision(new DamageInfo
            {
                Damage = _damage
            });
            Despawn();
            var pm = ManagerProvider.GetManager<PoolManager>();
            var vfx = pm.Spawn(EPool.HitVfx, collision.GetContact(0).point, Quaternion.identity);
            vfx.SetActive(true);
        }

        public override void Despawn()
        {
            gameObject.SetActive(false);
            var pm = ManagerProvider.GetManager<PoolManager>();
            pm.Despawn(EPool.Bullets, gameObject);
            _cHandler.OnCollision -= OnCollision;
        }

        private IEnumerator DespawnRoutine()
        {
            yield return new WaitForSeconds(_lifeSpan);
            Despawn();
            _destroyRoutine = null;
        }

        public void ReceiveDamage(DamageInfo info)
        {
            //This do nothing due bullet being destroy at the moment of the collision, can be used if we want some penetration bullets or different behaviours
        }
    }
}