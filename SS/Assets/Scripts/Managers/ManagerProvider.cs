using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ManagerProvider : MonoBehaviour
    {
        private static Dictionary<string, BaseManager> _managers = new Dictionary<string, BaseManager>();
        public static T GetManager<T>() where T:BaseManager
        {
            return (T)_managers[typeof(T).Name];
        }

        public static void RegisterManager<T>(T manager) where T : BaseManager
        {
            _managers.Add(typeof(T).Name, manager);
        }
    }
}