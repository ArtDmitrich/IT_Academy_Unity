using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private List<BulletData> _bulletsPrefabs;

    public Bullet GetBullet(BulletType bulletType, Transform bulletPos)
    {
        foreach (BulletData bullet in _bulletsPrefabs)
        {
            if (bullet.Key == bulletType)
            {
                return Instantiate(bullet.Value, bulletPos.position, Quaternion.identity);
            }
        }

        return null;
    }
}
