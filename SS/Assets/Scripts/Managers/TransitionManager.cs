using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Managers.Transition
{
    public class TransitionManager : BaseManager
    {
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
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public async Task UnloadLastScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            await SceneManager.UnloadSceneAsync(currentScene);
        }


    }
}