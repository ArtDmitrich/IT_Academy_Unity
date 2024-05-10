using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _standartBuletPrefab;
    [SerializeField] private Bullet _explosiveBuletPrefab;
    [SerializeField] private Bullet _pingPongBuletPrefab;


    public Bullet GetBullet(BulletType bullet, Transform bulletPos)
    {
        switch (bullet)
        {
            case BulletType.Standart:
                {
                    return Instantiate(_standartBuletPrefab, bulletPos.position, Quaternion.identity);
                }
            case BulletType.Explosive:
                {
                    return Instantiate(_explosiveBuletPrefab, bulletPos.position, Quaternion.identity);
                }
            case BulletType.PingPong:
                {
                    return Instantiate(_pingPongBuletPrefab, bulletPos.position, Quaternion.identity);
                }
            default:
                {
                    return null;
                }
        }
    }
}
