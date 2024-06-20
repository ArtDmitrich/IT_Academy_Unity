using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pool : MonoBehaviour
{
    [Inject] Spawner _spawner;

    private Dictionary<string, Queue<GameObject>> _poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public T GetPooledItem<T>(string poolName) where T : MonoBehaviour
    {
        if (_poolDictionary.ContainsKey(poolName))
        {
            var counter = 0;
            var pool = _poolDictionary[poolName];

            while (counter < pool.Count)
            {
                var itemToGet = pool.Dequeue().GetComponent<T>();
                pool.Enqueue(itemToGet.gameObject);

                if (!itemToGet.gameObject.activeInHierarchy)
                {
                    itemToGet.gameObject.SetActive(true);
                    return itemToGet;
                }

                counter++;
            }

            var newItem = _spawner.Spawn(poolName).GetComponent<T>();

            if (newItem == null)
            {
                return null;
            }

            pool.Enqueue(newItem.gameObject);
            newItem.gameObject.SetActive(true);

            return newItem;
        }
        else
        {
            AddNewPool(poolName);
            AddItemInPool(poolName);
        }

        return GetPooledItem<T>(poolName);
    }

    public void DeactivatePooledItems(string poolName)
    {
        var pool = _poolDictionary[poolName];
        var counter = 0;

        while (counter < pool.Count)
        {
            var itemToGet = pool.Dequeue();
            itemToGet.SetActive(false);

            pool.Enqueue(itemToGet);

            counter++;
        }
    }

    private void AddNewPool(string poolName)
    {
        var objectPool = new Queue<GameObject>();
        _poolDictionary.Add(poolName, objectPool);
    }

    private void AddItemInPool(string poolName)
    {
        var objectPool = _poolDictionary[poolName];

        var newItem = _spawner.Spawn(poolName);
        newItem.SetActive(false);
        objectPool.Enqueue(newItem);
    }
}
