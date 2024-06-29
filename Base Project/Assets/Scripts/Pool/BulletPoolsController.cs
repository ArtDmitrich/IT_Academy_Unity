using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolsController : MonoBehaviour
{
    [SerializeField] private List<BulletPool> _bulletPools;

    public Bullet Get(BulletType type)
    {
        for (int i = 0; i < _bulletPools.Count; i++)
        {
            if (type == _bulletPools[i].BulletType)
            {
                return _bulletPools[i].Pool.Get();
            }
        }

        return null;
    }
}
