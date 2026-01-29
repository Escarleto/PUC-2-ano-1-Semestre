using UnityEngine;
using DG.Tweening;

public class ManageButtons : MonoBehaviour
{
    private Animator Lift;
    [SerializeField] private CanvasGroup UI;
    [SerializeField] private CanvasGroup Main;
    [SerializeField] private CanvasGroup Controls;

    private void Start()
    {
        Lift = GetComponent<Animator>();
        Main.alpha = 1;
        Main.gameObject.SetActive(true);
        Controls.alpha = 0;
        Controls.interactable = false;
        Controls.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Lift.SetTrigger("Start");
        UI.DOFade(0f, 1.25f).SetEase(Ease.InOutSine);
        Main.interactable = false;
    }

    public void ChangeScene() { UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame"); }

    public void ControlsPanel(bool ToControls)
    {   
        if (ToControls == true)
        {
            Main.DOFade(0f, 1.25f).SetEase(Ease.InOutSine)
                .OnComplete(() => Main.gameObject.SetActive(false));
            Main.interactable = false;

            Controls.gameObject.SetActive(true);
            Controls.interactable = true;
            Controls.DOFade(1f, 1.25f).SetEase(Ease.InOutSine)
                 .OnComplete(() => Controls.interactable = true);
            return;
        }

        Controls.DOFade(0f, 1.25f).SetEase(Ease.InOutSine)
               .OnComplete(() => Controls.gameObject.SetActive(false));
        Main.gameObject.SetActive(true);
        Main.DOFade(1f, 1.25f).SetEase(Ease.InOutSine)
            .OnComplete(() => Main.interactable = true);
    }

    public void QuitGame() { Application.Quit(); }
}
