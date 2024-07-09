using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : Singleton<PoolsManager>
{
    [SerializeField] private List<UniversalPool> _pools;
    [SerializeField] private bool _poolPrewarming;
    [SerializeField] private int _startPoolSize = 5;

    public PooledItem GetPooledItem(string pooledItemName)
    {
        var pool = GetPool(pooledItemName);

        if (pool == null)
        {
            AddPool(pooledItemName, out pool);
        }

        try
        {
            return pool.Pool.Get();
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogWarning(ex.Message);
            return null;
        }
    }

    private UniversalPool GetPool(string pooledItemName)
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

    private void AddPool(string pooledItemName, out UniversalPool newPool)
    {
        var newObj = new GameObject();
        newPool = newObj.AddComponent<UniversalPool>();
        newPool.transform.SetParent(transform);
        newPool.PooledItemName = pooledItemName;
        newPool.name = pooledItemName + "Pool";
        _pools.Add(newPool);

        if (_poolPrewarming)
        {
            Prewarm(newPool);
        }
    }

    private void Prewarm(UniversalPool pool)
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
