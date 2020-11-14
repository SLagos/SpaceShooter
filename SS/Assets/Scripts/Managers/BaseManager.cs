using System.Collections;
using UnityEngine;

namespace Managers
{
    public class BaseManager: MonoBehaviour
    {
        /// <summary>
        /// This is an initialization method that can be call sync/async 
        /// </summary>
        public async virtual void Init() { }

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