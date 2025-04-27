using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameControl : MonoBehaviour
{
    private GameControls controls;

    [Inject]
    public void Construct(GameControls gameControls)
    {
        controls = gameControls;

        controls.Game.Restart.performed += ctx => OnRestart();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnRestart()
    {
        Debug.Log("Game Restarted!");
    }
}