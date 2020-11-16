using Controllers.Entity;
using Controllers.StatsNS;
using Managers;
using Data.StatsData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using Managers.Game;

namespace Controllers.Player
{
    [RequireComponent(typeof(StatsController))]
    public class PlayerController : EntityController
    {
        private Vector2 _direction = Vector2.zero;
        private StatsController _statsController;
        private Stats _currentStats;

        private Vector3 _currentVelocity = Vector3.zero;
        [SerializeField]
        private float _smoothFactor = 1f;


        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
        }

        private void FixedUpdate()
        {
            if(!GameManager.IsPaused && !GameManager.GameOver)//TODO:Change this accordingly to the game state
            {
                Vector3 nextPos = transform.position + ((Vector3)_direction * _statsController.GetCurrentSpeed());
                transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref _currentVelocity, _smoothFactor);
            }
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        public override void Despawn()
        {
            throw new System.NotImplementedException();
        }
    }
}

