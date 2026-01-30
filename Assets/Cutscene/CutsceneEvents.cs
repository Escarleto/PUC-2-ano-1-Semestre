using UnityEngine;

public class CutsceneEvents : MonoBehaviour
{
    [SerializeField] private AudioSource LiftMusic;
    [SerializeField] private AudioSource LiftBoom;

    public void ChangeScene() { UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame"); }

    public void PlayMusic() 
    {
        LiftMusic.time = 10f;
        LiftMusic.Play(); 
    }

    public void PlayBoom() { LiftBoom.Play(); }
}
