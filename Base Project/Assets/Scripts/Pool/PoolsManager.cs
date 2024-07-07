using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] private List<UniversalPool> _pools;

    public PooledItem GetPooledItem(string pooledItemName)
    {
        foreach (var pool in _pools)
        {
            if (pool.PooledItemName == pooledItemName)
            {
                return pool.Pool.Get();
            }
        }

        return null;
    }
    private void AddPool(string pooledItemName)
    {
        var newPool = new UniversalPool();
        newPool.transform.SetParent(transform);
        newPool.PooledItemName = pooledItemName;
        newPool.name = pooledItemName + "Pool";
        _pools.Add(newPool);
    }
}
