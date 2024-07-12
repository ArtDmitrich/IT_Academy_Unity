public class BulletManager : ItemManager<BulletManager>
{
    public Bullet GetBullet(string bulletName)
    {
        var item = _poolManager.GetPooledItem(bulletName);

        if (item == null)
        {
            return null;
        }
        else if (item.TryGetComponent<Bullet>(out var bullet))
        {
            return bullet;
        }

        return null;
    }
}
