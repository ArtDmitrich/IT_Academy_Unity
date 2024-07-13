using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class PoolManager : MonoBehaviour
{
    [SerializeField] protected List<PoolByName> _pools;
    [SerializeField] protected bool _poolPrewarming = true;
    [SerializeField] protected int _startPoolSize = 5;

    private Spawner _spawner;

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
        _pools = new List<PoolByName>();
    }

    public T GetPooledItem<T>(string pooledItemName) where T : MonoBehaviour
    {
        var pool = GetPool(pooledItemName);

        if (pool == null)
        {
            AddPool(pooledItemName, out pool);
        }

        try
        {
            return pool.Pool.Get().GetComponent<T>();
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogWarning(ex.Message);
            return null;
        }
    }

    private PoolByName GetPool(string pooledItemName)
    {
        foreach (var pool in _pools)
        {
            if (pool.PooledItemName == pooledItemName)
            {
                return pool;
            }
        }

        return null;
    }

    private void AddPool(string pooledItemName, out PoolByName newPool)
    {
        var newObj = new GameObject();
        newPool = newObj.AddComponent<PoolByName>();

        newPool.transform.SetParent(transform);
        newPool.PooledItemName = pooledItemName;
        newPool.name = pooledItemName + "Pool";
        newPool.Init(_spawner);
        
        _pools.Add(newPool);

        if (_poolPrewarming)
        {
            Prewarm(newPool);
        }
    }

    private void Prewarm(PoolByName pool)
    {
        var startingItems = new List<PooledItem>();

        for (int i = 0; i < _startPoolSize; i++)
        {
            try
            {
                var item = pool.Pool.Get();
                startingItems.Add(item);
            }
            catch (ArgumentNullException ex)
            {
                Debug.LogWarning(ex.Message);
                return;
            }
        }

        foreach (var item in startingItems)
        {
            item.Release();
        }
    }
}
