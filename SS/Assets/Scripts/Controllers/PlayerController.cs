using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        private Vector2 _direction = Vector2.zero;

        private void FixedUpdate()
        {
            if(true)//TODO:Change this accordingly to the game state
            {
                transform.position += ((Vector3)_direction*_speed*Time.fixedDeltaTime);
            }
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PoolManager pm = ManagerProvider.GetManager<PoolManager>();
                pm.Spawn<Bullet.BulletController>(PoolManager.EPool.Bullets, transform.position, Quaternion.identity);
            }
                
        }
    }
}

