using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Bullet
{
    /// <summary>
    /// This script handle the movement and the logic
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 15f;

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
        /// <summary>
        /// Easy way to check if this object is being render by any camera, so i can dispose it.
        /// </summary>
        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}