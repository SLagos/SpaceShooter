using Controllers.Bullet;
using Managers;
using Managers.Game;
using PoolSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers.Weapons
{
    public class WeaponsController : MonoBehaviour
    {
        public void OnFire(InputAction.CallbackContext context)
        {
            if (GameManager.IsPaused || GameManager.IsGameOver)
                return;
            if (context.performed)
            {
                Fire();
            }
        }

        public void Fire()
        {
            PoolManager pm = ManagerProvider.GetManager<PoolManager>();
            BulletController bc = pm.Spawn<BulletController>(EPool.Bullets, transform.position, transform.rotation);
            bc.Init(1, gameObject.layer);
        }

    }
}