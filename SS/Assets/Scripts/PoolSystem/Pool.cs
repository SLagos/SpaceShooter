using Patterns.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PoolSystem
{
    [System.Serializable]
    public class Pool
    {
        public EPool Type;
        public AssetReference Object;
        public int StartAmount;
        private Stack<GameObject> _objects = new Stack<GameObject>();
        private Transform _root;

        public async Task Init(Transform parent)
        {
            string poolName = string.Empty;
            AsyncOperationHandle<GameObject> op = Addressables.LoadAssetAsync<GameObject>(Object);
            op.Completed += (o) =>
            {
                poolName = o.Result.name;
            };
            await op.Task;
            GameObject poolRoot = new GameObject(poolName + "Pool");
            poolRoot.transform.SetParent(parent);
            _root = poolRoot.transform;


            for (int i = 0; i < StartAmount; i++)
            {
                AsyncOperationHandle<GameObject> handler = Addressables.InstantiateAsync(Object, poolRoot.transform);
                handler.Completed += (obj =>
                {
                    obj.Result.SetActive(false);
                    _objects.Push(obj.Result);
                });
                await handler.Task;
            }
        }

        public GameObject GetObject()
        {
            if (_objects.Count > 0)
                return _objects.Pop();
            else
            {
                //[TODO]This shoud be fine because the asset is preloaded when the pool is initialized, so this load will be sync
                //If i have spare time i can fin a better way for this.
                return Addressables.InstantiateAsync(Object, _root).Result;
            }
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.parent = _root;
            obj.transform.position = Vector3.zero;
            _objects.Push(obj);

        }
    }

    public enum EPool
    {
        None = 0,
        Bullets = 1,
        Enemy1 = 2,
        HitVfx = 4
    }
}