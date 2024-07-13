using System;
using UnityEngine;

public class BulletManager : ItemManager<BulletManager>
{
    [SerializeField] PoolSettings poolSettings;
    public Bullet GetBullet(string bulletName)
    {
        return _poolManager.GetPooledItem<Bullet>(bulletName);
    }
}
