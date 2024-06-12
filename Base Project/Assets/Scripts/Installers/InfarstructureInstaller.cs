using UnityEngine;
using Zenject;

public class InfarstructureInstaller : MonoInstaller
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Pool _pool;
    [SerializeField] private GameController _gameController;

    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<Spawner>().FromInstance(_spawner).AsSingle().NonLazy();
        Container.Bind<Pool>().FromInstance(_pool).AsSingle().NonLazy();
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
    }
}