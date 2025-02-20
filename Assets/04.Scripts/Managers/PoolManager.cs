using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MondayCatWorld.Managers
{
    // 오브젝트 풀링을 관리하는 클래스
    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<string, Queue<GameObject>> poolDictionary = new();

        private readonly Dictionary<string, GameObject> prefabDictionary = new();
        private readonly Dictionary<string, Transform> poolParentDictionary = new();
        private readonly HashSet<GameObject> despawnedObjects = new();
        private Transform poolParent = null;

        public void Init()
        {
            poolParent = new GameObject("PoolParent").transform;
        }

        public void CreatePool(GameObject prefab, int poolSize)
        {
            string poolKey = prefab.name;

            if (!poolDictionary.ContainsKey(poolKey))
            {
                poolDictionary.Add(poolKey, new Queue<GameObject>());
                prefabDictionary.Add(poolKey, prefab);

                var poolRoot = new GameObject(poolKey + "Root");
                poolRoot.transform.SetParent(poolParent);
                poolParentDictionary.Add(poolKey, poolRoot.transform);

                for (int i = 0; i < poolSize; i++)
                {
                    var parent = poolParentDictionary[poolKey];
                    GameObject obj = Instantiate(prefab, parent, true);
                    obj.name = poolKey;
                    obj.SetActive(false);
                    poolDictionary[poolKey].Enqueue(obj);
                }
            }
        }

        public GameObject Spawn(string poolKey, Vector3 position = default, Quaternion rotation = default)
        {
            if (poolDictionary.ContainsKey(poolKey))
            {
                if (poolDictionary[poolKey].Count == 0)
                {
                    var prefab = prefabDictionary[poolKey];
                    GameObject obj = Instantiate(prefab);
                    obj.name = poolKey;
                    obj.SetActive(false);
                    poolDictionary[poolKey].Enqueue(obj);
                }

                GameObject spawnObj = poolDictionary[poolKey].Dequeue();
                spawnObj.SetActive(true);
                spawnObj.transform.position = position;
                spawnObj.transform.rotation = rotation;
                despawnedObjects.Remove(spawnObj);
                
                return spawnObj;
            }

            Debug.LogWarning($"PoolManager: {poolKey} is not exist.");
            return null;
        }

        public GameObject Spawn(string poolKey, Transform parent)
        {
            GameObject obj = Spawn(poolKey);
            obj.transform.SetParent(parent);
            return obj;
        }

        public T Spawn<T>(string poolKey, Vector3 position = default, Quaternion rotation = default) where T : Component
        {
            GameObject obj = Spawn(poolKey, position, rotation);
            if (obj.TryGetComponent(out T component))
            {
                return component;
            }
            
            Debug.LogWarning($"PoolManager: {poolKey}<{typeof(T)}> is not exist.");
            return null;
        }

        public void Despawn(GameObject obj)
        {
            if (despawnedObjects.Contains(obj))
            {
                Debug.LogWarning($"Object {obj.name} is already despawned.");
                return;
            }
            
            string poolKey = obj.name;

            if (!poolDictionary.ContainsKey(poolKey)) return;
            
            obj.SetActive(false);
            obj.transform.SetParent(poolParentDictionary[poolKey]);
            poolDictionary[poolKey].Enqueue(obj);
            despawnedObjects.Add(obj);
        }

        public void LoadSceneClearPool()
        {
            prefabDictionary.Clear();
            poolDictionary.Clear();
            poolParentDictionary.Clear();
        }
        
        public bool IsExistPool(string poolKey)
        {
            return poolDictionary.ContainsKey(poolKey);
        }
    }
}