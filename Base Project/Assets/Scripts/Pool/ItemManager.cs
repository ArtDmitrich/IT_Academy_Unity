using UnityEngine;
[RequireComponent (typeof(Spawner))]
public class ItemManager<T> : Singleton<T> where T : MonoBehaviour
{
    protected Spawner _spawner;
    protected PoolManager _poolManager;

    protected void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _poolManager = gameObject.AddComponent<PoolManager>();
        _poolManager.Init(_spawner);
    }
}
