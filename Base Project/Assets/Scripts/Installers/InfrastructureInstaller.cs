using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private TimelinesController _timelinesController;
    [SerializeField] private CanvasController _canvas;
    [SerializeField] private ObstaclesController _obstacles;

    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromNew().AsSingle().NonLazy();
        Container.Bind<TimelinesController>().FromInstance(_timelinesController).AsSingle().NonLazy();
        Container.Bind<CanvasController>().FromInstance(_canvas).AsSingle().NonLazy();
        Container.Bind<ObstaclesController>().FromInstance(_obstacles).AsSingle().NonLazy();
    }
}