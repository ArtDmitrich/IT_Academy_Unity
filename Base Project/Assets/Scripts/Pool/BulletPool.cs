using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class BulletPool: MonoBehaviour
{
    public BulletType BulletType;

    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // Collection checks will throw errors if we try to release an Item that is already in the Pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    IObjectPool<Bullet> m_Pool;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (poolType == PoolType.Stack)
                    m_Pool = new ObjectPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    m_Pool = new LinkedPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }

            return m_Pool;
        }
    }

    [Inject] private BulletSpawner _spawner;

    private Bullet CreatePooledItem()
    {
        var item = _spawner.GetBullet(BulletType);

        // This is used to return ParticleSystems to the Pool when they have stopped.
        var returnToPool = item.gameObject.AddComponent<ReturnToPool>();
        returnToPool.Item = item;
        returnToPool.Pool = Pool;

        return item;
    }

    // Called when an Item is returned to the Pool using Release
    private void OnReturnedToPool(Bullet item)
    {
        item.gameObject.SetActive(false);
    }

    // Called when an Item is taken from the Pool using Get
    private void OnTakeFromPool(Bullet system)
    {
        system.gameObject.SetActive(true);
    }

    // If the Pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    private void OnDestroyPoolObject(Bullet system)
    {
        Destroy(system.gameObject);
    }
}
