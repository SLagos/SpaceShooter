using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class BaseManager: MonoBehaviour
    {
        /// <summary>
        /// This field is for initialization order, the higher the later will be initialized
        /// </summary>
        [SerializeField]
        protected int _priority;

        public int Priority => _priority;

        protected bool _initialized = false;

        public bool Initialized => _initialized;
        /// <summary>
        /// This is an initialization method that can be call sync/async 
        /// </summary>
        public async virtual Task Init() 
        {
        }

        public void Awake()
        {
            DontDestroyOnLoad(this);
            OnAwake();
        }

        public virtual void OnAwake()
        {

        }
    }
}