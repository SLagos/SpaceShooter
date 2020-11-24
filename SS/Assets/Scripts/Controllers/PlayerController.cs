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
using Managers.Transition;
using Utils;
using Systems.DamageSystem;
using System;

namespace Controllers.Player
{
    [RequireComponent(typeof(StatsController))]
    public class PlayerController : EntityController, IDamageable
    {
        private Vector2 _direction = Vector2.zero;
        private StatsController _statsController;
        private Stats _currentStats;

        private Vector3 _currentVelocity = Vector3.zero;
        [SerializeField]
        private float _smoothFactor = 1f;
        [SerializeField]
        private CollisionHandler _cHandler;


        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
            _cHandler.Init(this);
        }
        private void FixedUpdate()
        {
            if(!GameManager.IsPaused && !GameManager.IsGameOver)
            {
                Vector3 nextPos = transform.position + ((Vector3)_direction * _statsController.GetCurrentSpeed());
                Vector3 newPos = Vector3.SmoothDamp(transform.position, nextPos, ref _currentVelocity, _smoothFactor);
                //Is neccesary to check if the new position is in boundry, otherwise we stop the current movement to not go out
                if (ScreenBorderUtility.InScreenBounds(newPos))
                    transform.position = newPos;
                else
                {
                    _currentVelocity = Vector3.zero;
                    //This is used in case that a suden change of screen resolution we make sure that the player will keep in bound to be playable
                    transform.position = ScreenBorderUtility.GetValidPosition(transform.position);
                }
                    
            }
            if (GameManager.IsGameOver)
                gameObject.SetActive(false);

        }
        public void OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        public override void Despawn()
        {
            //Change this to only happen when no lifes remains
            ManagerProvider.GetManager<GameManager>().GameOver();
            this.gameObject.SetActive(false);
        }

        public void ReceiveDamage(DamageInfo info)
        {
            _statsController.ApplyDamage(info.Damage);
        }

    }
}

