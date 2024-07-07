using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private UniversalSpawner _universalSpawner;
    [SerializeField] private PoolsManager _poolsManager;

    public override void InstallBindings()
    {
        Container.Bind<UniversalSpawner>().FromInstance(_universalSpawner).AsSingle().NonLazy();
        Container.Bind<PoolsManager>().FromInstance(_poolsManager).AsSingle().NonLazy();
    }
}