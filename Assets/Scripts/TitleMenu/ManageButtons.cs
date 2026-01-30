using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageButtons : MonoBehaviour
{
    [SerializeField] private Animator Lift;
    [SerializeField] private CanvasGroup UI;
    [SerializeField] private CanvasGroup Main;
    [SerializeField] private CanvasGroup Settings;
    [SerializeField] private CanvasGroup Controls;
    private bool IsPaused = false;

    private void Start()
    {
        Controls.alpha = 0;
        Controls.interactable = false;
        Controls.gameObject.SetActive(false);
        Settings.alpha = 0;
        Settings.interactable = false;
        Settings.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        if (Lift == null) return;
        Lift.SetTrigger("Start");
        UI.DOFade(0f, 1.25f).SetEase(Ease.InOutSine);
        Main.interactable = false;
    }

    public void ChangeScene() { UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene"); }

    public void TogglePauseMenu(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            IsPaused = !IsPaused;

            if (IsPaused) { Pause(); return; }
            else { Resume(); return; }
        }
    }

    private void Pause()
    {
        Main.gameObject.SetActive(true);
        Main.alpha = 1f;
        Main.interactable = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Main.gameObject.SetActive(false);
        Controls.alpha = 0;
        Controls.interactable = false;
        Controls.gameObject.SetActive(false);
        Settings.alpha = 0;
        Settings.interactable = false;
        Settings.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
    }

    public void ChangePanel(CanvasGroup target)
    {
        if (target.gameObject.activeSelf)
        {
            target.interactable = false;

            target.DOFade(0f, 1.25f)
                  .SetEase(Ease.InOutSine)
                  .SetUpdate(true)
                  .OnComplete(() =>
                  {
                      target.gameObject.SetActive(false);
                  });

            Main.gameObject.SetActive(true);
            Main.alpha = 0f;

            Main.DOFade(1f, 1.25f)
                .SetEase(Ease.InOutSine)
                .SetUpdate(true)
                .OnComplete(() => Main.interactable = true);

            return;
        }

        Main.interactable = false;

        Main.DOFade(0f, 1.25f)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .OnComplete(() => Main.gameObject.SetActive(false));

        target.gameObject.SetActive(true);
        target.alpha = 0f;
        target.interactable = false;

        target.DOFade(1f, 1.25f)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .OnComplete(() => target.interactable = true);
    }


    public void QuitGame() { Application.Quit(); }
}
