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
        private Animator _animator;

        private StatsController _statsController;

        private Camera _camera;

        private void Awake()
        {
            _statsController = GetComponent<StatsController>();
            _camera = Camera.main;
        }

        private void Update()
        {
           Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);
            screenPos.y += _yOffset;
            _container.transform.position = screenPos;
            float lifeNormalized = _statsController.GetLifeNormalized();
            _lifeBar.fillAmount = lifeNormalized;
            _animator.SetFloat("NormalizedLife", lifeNormalized);
        }
    }
}