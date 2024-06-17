using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private MeshGenerator _generator;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Pool _pool;
    [SerializeField] private BlocksController _blocksController;
    [SerializeField] private CanvasController _canvas;

    public override void InstallBindings()
    {
        Container.Bind<MeshGenerator>().FromInstance(_generator).AsSingle().NonLazy();
        Container.Bind<Spawner>().FromInstance(_spawner).AsSingle().NonLazy();
        Container.Bind<Pool>().FromInstance(_pool).AsSingle().NonLazy();
        Container.Bind<BlocksController>().FromInstance(_blocksController).AsSingle().NonLazy();
        Container.Bind<CanvasController>().FromInstance(_canvas).AsSingle().NonLazy();
    }
}