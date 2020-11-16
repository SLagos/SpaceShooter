using System.Collections;
using UnityEngine;
using Managers;
using Managers.Transition;
using Data.GameData;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Managers.Game
{
    public class GameManager : BaseManager
    {

        private float _timePlayed;
        public float TimePlayed => _timePlayed;
        private bool _isPaused = true;
        public bool IsPaused => _isPaused;

        private GameData _data;
        private int _score;
        public override void OnAwake()
        {
            ManagerProvider.RegisterManager(this, _priority);
        }

        public override async Task Init()
        {
            AsyncOperationHandle<GameData> op = Addressables.LoadAssetAsync<GameData>("GameData");
            op.Completed += (o) =>
            {
                _data = o.Result;
            };
            await op.Task;
        }

        public void StartGame()
        {
            _timePlayed = 0f;
            _score = 0;
            _isPaused = false;
            TransitionManager tM = ManagerProvider.GetManager<TransitionManager>();
            tM.LoadScene("Level1"); //not using Await due Fire and Forget
        }

        private void Update()
        {
            if(!_isPaused)
            {
                _timePlayed += Time.deltaTime;
                if(_data.WinCondition.IsConditionMeet())
                {
                    //Win Screen
                }
            }
        }
    }
}