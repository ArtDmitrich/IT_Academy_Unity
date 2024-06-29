using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private BulletPoolsController _poolsController;

    public override void InstallBindings()
    {
        Container.Bind<BulletSpawner>().FromInstance(_spawner).AsSingle().NonLazy();
        Container.Bind<BulletPoolsController>().FromInstance(_poolsController).AsSingle().NonLazy();
    }
}