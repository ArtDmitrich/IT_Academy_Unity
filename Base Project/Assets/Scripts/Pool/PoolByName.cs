using System;
using UnityEngine;
using UnityEngine.Pool;

public class PoolByName : MonoBehaviour
{
    public string PooledItemName;

    [SerializeField] private bool _collectionChecks = true;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxPoolSize = 50;

    public IObjectPool<PooledItem> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new ObjectPool<PooledItem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, _defaultCapacity, _maxPoolSize);
            }

            return m_Pool;
        }
    }

    private IObjectPool<PooledItem> m_Pool;
    private Spawner _spawner;

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
    }

    private PooledItem CreatePooledItem()
    {
        if (_spawner == null)
        {
            throw new ArgumentNullException(gameObject.name, "The spawner is null. You need to call the method Init(Spawner spawner) and set the spawner");
        }

        var item = _spawner.GetPooledItem(PooledItemName);

        if (item == null)
        {
            throw new ArgumentNullException(_spawner.name, $"The spawner does not contain a link to the prefab with name: {PooledItemName}");
        }

        item.transform.SetParent(transform);
        item.Pool = Pool;

        return item;
    }

    private void OnReturnedToPool(PooledItem item)
    {
        item.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(PooledItem item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PooledItem item)
    {
        Destroy(item.gameObject);
    }
}
