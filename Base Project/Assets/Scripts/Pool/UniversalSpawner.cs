using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSpawner : Singleton<UniversalSpawner>
{
    [SerializeField] private List<PooledItemData> _pooledItems;

    public PooledItem GetPooledItem(string itemName)
    {
        foreach (var item in _pooledItems)
        {
            if (item.Key == itemName)
            {
                return Instantiate(item.Value);
            }
        }

        return null;
    }
}
