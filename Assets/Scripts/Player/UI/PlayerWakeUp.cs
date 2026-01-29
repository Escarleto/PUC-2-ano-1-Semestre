using DG.Tweening;
using UnityEngine;

public class PlayerWakeUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup FadeImage;
    [SerializeField] private PlayerController Player;
    private float WakeDelay = 1f;
    private AudioSource ElevatorBoom;

    
    private void Start()
    {
        if (Player != null) Player.CanMove = false;
        ElevatorBoom = GetComponent<AudioSource>();
        FadeImage.alpha = 1f;

        if (ElevatorBoom != null) ElevatorBoom.Play();

        Invoke(nameof(FadeIn), WakeDelay);
    }

    private void FadeIn()
    {
        FadeImage
            .DOFade(0f, 2f)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                if (Player != null)
                    Player.CanMove = true;
            });
    }
}
