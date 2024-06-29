using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool: MonoBehaviour
{
    public Bullet Item;
    public IObjectPool<Bullet> Pool;

    private void OnDisable()
    {
        Pool.Release(Item);
    }
}
