using Controllers.StatsNS;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.BarLife
{
    [RequireComponent(typeof(StatsController))]
    public class LifeBarPresenter : MonoBehaviour
    {
        [SerializeField]
        private Image _lifeBar;

        [SerializeField]
        private Transform _container;
        [SerializeField]
        private float _yOffset;
        [SerializeField]
        private Animator _animatorHealthBar,_animDecal;
        private StatsController _statsController;

        private Camera _camera;

        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
            _camera = Camera.main;
            _statsController.OnDamageReceived += HealthUpdate;

        }

        private void Start()
        {
            HealthUpdate();
        }
        private void HealthUpdate()
        {
            float lifeNormalized = _statsController.GetLifeNormalized();
            _lifeBar.fillAmount = lifeNormalized;
            _animatorHealthBar.SetFloat("NormalizedLife", lifeNormalized);
            UpdateDecal(lifeNormalized);
        }

        private void UpdateDecal(float value)
        {
            if(_animDecal != null)
            {
                _animDecal.SetFloat("DamageNormalized", 1 - value);
            }
        }
    }
}