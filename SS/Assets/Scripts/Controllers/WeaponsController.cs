using Controllers.Bullet;
using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers.Weapons
{
    public class WeaponsController : MonoBehaviour
    {

        private void Awake()
        {
            
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PoolManager pm = ManagerProvider.GetManager<PoolManager>();
                BulletController bc = pm.Spawn<BulletController>(PoolManager.EPool.Bullets, transform.position, Quaternion.identity);
                bc.Init(1,gameObject.layer);
            }

        }
    }
}