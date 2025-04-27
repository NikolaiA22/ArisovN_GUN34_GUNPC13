using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameControls gameControls;

    public override void InstallBindings()
    {
        Container.Bind<GameControls>().FromInstance(gameControls).AsSingle();

        //Container.Bind<GameControls>().AsTransient();
    }
}