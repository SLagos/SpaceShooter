using Controllers.StatsNS;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.BarLife
{
    /// <summary>
    /// This class is responsible of displaying animations regarding the health status of the player
    /// </summary>
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
        [SerializeField]
        private string _animNormalizedLifeString = "NormalizedLife";
        [SerializeField]
        private string _animDamageNormalizedString = "DamageNormalized";
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
            _animatorHealthBar.SetFloat(_animNormalizedLifeString, lifeNormalized);
            UpdateDecal(lifeNormalized);
        }

        /// <summary>
        /// This use a blendtree in animator to handle 4 posible states of decals
        /// using the inversed normalized lifes
        /// </summary>
        /// <param name="value"></param>
        private void UpdateDecal(float value)
        {
            if(_animDecal != null)
            {
                _animDecal.SetFloat(_animDamageNormalizedString, 1 - value);
            }
        }
    }
}