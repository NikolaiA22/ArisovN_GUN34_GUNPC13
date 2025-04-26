using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Image restartProgress;
    [SerializeField] private float restartHoldTime = 2f;

    private Controls controls;
    private float restartHoldTimer;
    private bool isRestarting;

    private void Awake()
    {
        controls = new Controls();
        controls.Game.Restart.performed += ctx => StartRestart();
        controls.Game.Restart.canceled += ctx => StopRestart();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        if (isRestarting)
        {
            restartHoldTimer += Time.deltaTime;
            restartProgress.fillAmount = restartHoldTimer / restartHoldTime;

            if (restartHoldTimer >= restartHoldTime)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }

    private void StartRestart()
    {
        isRestarting = true;
        restartPanel.SetActive(true);
        restartHoldTimer = 0f;
    }

    private void StopRestart()
    {
        isRestarting = false;
        restartPanel.SetActive(false);
    }
}
