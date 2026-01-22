using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private GameObject PausePanel;
    private GameObject SettingsPanel;
    private bool IsPaused = false;

    private void Awake()
    {
        PausePanel = transform.Find("PausePanel").gameObject;
        SettingsPanel = transform.Find("SettingsPanel").gameObject;
        SettingsPanel.SetActive(false);
        Resume();
    }

    public void TogglePauseMenu(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            IsPaused = !IsPaused;

            if (IsPaused) { Pause(); return; }
            else { Resume(); return; }
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
    }

    private void Pause() 
    {
        PausePanel.SetActive(true);
        SettingsPanel.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Settings() { SettingsPanel.SetActive(!SettingsPanel.activeSelf); }

    public void Quit() { Application.Quit(); }
}
