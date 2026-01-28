using UnityEngine;
using DG.Tweening;

public class ManageButtons : MonoBehaviour
{
    private Animator Lift;
    [SerializeField ] private CanvasGroup UI;

    private void Start() { Lift = GetComponent<Animator>(); }

    public void StartGame()
    {
        Lift.SetTrigger("Start");
        UI.DOFade(0f, 1.25f).SetEase(Ease.InOutSine);
    }

    public void ChangeScene() { UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame"); }

    public void QuitGame() { Application.Quit(); }
}
