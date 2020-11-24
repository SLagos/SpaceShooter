﻿using Managers;
using Patterns.Observer;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PoolSystem
{
    public class PoolManager : BaseManager
    {
        [SerializeField]
        private List<Pool> _pools = new List<Pool>();
        public async override Task Init()
        {
            if (_initialized)
                return;
            foreach (var pool in _pools)
            {                
                await pool.Init(this.transform);
            }
        }

        public T Spawn<T>(EPool poolType, Vector3 position ,Quaternion rotation,Transform parent = null) where T : ISubject
        {
            Pool pool = _pools.Find(p => p.Type == poolType);
            GameObject obj = pool.GetObject();
            if (parent != null)
                obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            //obj.SetActive(true);

            return obj.GetComponent<T>();
        }

        public void Despawn(EPool poolType, GameObject obj)
        {
            Pool pool = _pools.Find(p => p.Type == poolType);
            pool.ReturnObject(obj);
        }


        public override void OnAwake()
        {
            ManagerProvider.RegisterManager(this,_priority);
        }
    }
}