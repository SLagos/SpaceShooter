using Managers;
using System.Collections;
using Systems.PoolSystem;
using UnityEngine;

namespace Systems.PoolSystem
{
    public class DespawnAfterSeconds : MonoBehaviour
    {
        [SerializeField]
        private float _lifeSpan;
        private void OnEnable()
        {
            StartCoroutine(DespawnRoutine());
        }

        IEnumerator DespawnRoutine()
        {
            yield return new WaitForSeconds(_lifeSpan);
            gameObject.SetActive(false);
            var pm = ManagerProvider.GetManager<PoolManager>();
            pm.Despawn(EPool.HitVfx, gameObject);
        }
    }
}