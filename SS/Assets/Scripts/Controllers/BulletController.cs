using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers.Bullet
{
    /// <summary>
    /// This script handle the movement and the logic
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

        private int _damage = 0;

        /// <summary>
        /// Using fixed update to handle positions and collisions at the same rate to avoid issues
        /// </summary>
        private void FixedUpdate()
        {
            if (true)//TODO:Change this accordingly to the game state
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
        }
        /// <summary>
        /// Easy way to check if this object is being render by any camera, so i can dispose it.
        /// </summary>
        void OnBecameInvisible()
        {
            Despawn();
        }

        private void OnCollisionEnter(Collision collision)
        {
            DoDamage(collision.gameObject, collision.GetContact(0).point);
            Despawn();
        }

        private void DoDamage(GameObject collidedGo, Vector3 hitPoint)
        {
            collidedGo.SendMessageUpwards("ApplyDamage", _damage, SendMessageOptions.DontRequireReceiver);
            //Move this to a better system that use the poolsystem
            Addressables.InstantiateAsync("HitVfx1", position: hitPoint, Quaternion.identity);
        }

        private void Despawn()
        {
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            pm.Despawn(PoolManager.EPool.Bullets, gameObject);
        }
    }
}