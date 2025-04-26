using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private InputActionAsset inputActions;

    public override void InstallBindings()
    {
        Container.BindInstance(inputActions.FindActionMap("Game")).AsSingle();
    }
}
