using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public GameControls controls;
    public Image progressBar;
    private float fillAmount = 0f;
    private bool isFilling = false;

    private void Awake()
    {
        controls = new GameControls();
        controls.Game.Restart.performed += ctx => StartFilling();
        controls.Game.Restart.canceled += ctx => StopFilling();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (isFilling)
        {
            fillAmount += Time.deltaTime;
            progressBar.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                RestartScene();
            }
        }
    }

    private void StartFilling()
    {
        isFilling = true;
        fillAmount = 0f;
        progressBar.fillAmount = fillAmount;
        progressBar.gameObject.SetActive(true);
    }

     private void StopFilling()
     {
        isFilling = false;
        progressBar.gameObject.SetActive(false);
     }

     private void RestartScene()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }
}
