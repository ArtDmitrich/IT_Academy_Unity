using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PoolSettings", order = 1)]
public class PoolSettings : ScriptableObject
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
