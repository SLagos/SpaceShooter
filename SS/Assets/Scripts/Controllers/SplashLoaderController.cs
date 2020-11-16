using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Managers;
using Managers.Game;

namespace Controllers.SplashLoader
{
    public class SplashLoaderController : MonoBehaviour
    {
        [SerializeField]
        private AssetReference _managers;
        async void Start()
        {
            AsyncOperationHandle<GameObject> op = Addressables.LoadAssetAsync<GameObject>(_managers);
            await op.Task;
            op = Addressables.InstantiateAsync(_managers);
            op.Completed += (obj) => 
            {
                DontDestroyOnLoad(obj.Result);
            };
            await op.Task;

            await ManagerProvider.InitializeManagers();
            GameManager gm = ManagerProvider.GetManager<GameManager>();
            gm.StartGame();
        }
    }
}