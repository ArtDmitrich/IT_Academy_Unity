using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class UniversalPool : MonoBehaviour
{
    public string PooledItemName;

    [SerializeField] private bool _collectionChecks = true;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxPoolSize = 50;
    [SerializeField] private int _startPoolSize = 10;


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

    [Inject] private UniversalSpawner _spawner;

    private PooledItem CreatePooledItem()
    {
        var item = _spawner.GetPooledItem(PooledItemName);
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
