using Data.GameData;
using Managers.Game;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Presenter.Lives
{
    public class LivesPresenter : MonoBehaviour
    {
        private List<GameObject> _livesGo;
        [SerializeField]
        private AssetReference _lifePrefab;
        [SerializeField]
        private Transform _container;

        public async Task Init(int lives)
        {
            _livesGo = new List<GameObject>(lives);
            var op = Addressables.LoadAssetAsync<GameObject>(_lifePrefab);
            await op.Task;
            for (int i = 0; i < lives; i++)
            {
                Addressables.InstantiateAsync(_lifePrefab,parent: _container).Completed += (obj) =>
                {
                    _livesGo.Add(obj.Result);
                }; //This is already preloaded due "LoadAssetAsync" so its in memory
            }
            UpdateLives(lives);
        }
        public void UpdateLives(int lives)
        {
            for (int i = 0; i < _livesGo.Count; i++)
            {
                _livesGo[i].SetActive(i < lives);
            }
        }


    }
}