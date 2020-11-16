using Controllers.Loading;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Managers.Transition
{
    [RequireComponent(typeof(LoadingController))]
    public class TransitionManager : BaseManager
    {

        private LoadingController _lController;

        public override void OnAwake()
        {
            ManagerProvider.RegisterManager(this, _priority);
        }

        public async Task LoadSceneAsync(string sceneName)
        {
            //_lController.ActiveTransition();
            Scene currentScene = SceneManager.GetActiveScene();
            await SceneManager.UnloadSceneAsync(currentScene);

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            //_lController.HideTransition();

        }
        public async Task LoadSceneAdditiveAsync(string sceneName)
        {
            //_lController.ActiveTransition();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            //_lController.HideTransition();

        }

        public async Task UnloadLastScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            await SceneManager.UnloadSceneAsync(currentScene);
        }


    }
}