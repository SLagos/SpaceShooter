using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public static void RegisterManager<T>(T manager, int priority = 0) where T : BaseManager
        {
            _managers.Add(typeof(T).Name, manager);
        }

        public async static Task InitializeManagers()
        {
            //First order the managers by priority then initialize them
            _managers.OrderBy(v => v.Value.Priority);
            foreach (var manager in _managers)
            {
                await manager.Value.Init();
            }
        }
    }
}