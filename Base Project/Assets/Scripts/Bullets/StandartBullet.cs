using UnityEngine;

public class StandartBullet : Bullet
{
    [SerializeField] private float _bulletLifetime;

    private void Start()
    {
        Destroy(gameObject, _bulletLifetime);
    }
}
