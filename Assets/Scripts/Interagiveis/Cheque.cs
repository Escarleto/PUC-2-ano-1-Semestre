using UnityEngine;

public class Cheque : MonoBehaviour
{
    [SerializeField] PannochkaDialogue Pannochka;
    [SerializeField] LiftManager Lift;
    private bool InputBlocker = false;

    private void Start()
    {
       gameObject.SetActive(false); 
    }

    public void OnCollected()
    {
        Pannochka.BarkDialogue("Finalmente");
        Camera.main.GetComponentInParent<PlayerController>().CanMove = false;
        InputBlocker = true;
    }

    public void OnClosed()
    {
        if (InputBlocker == true)
        {
            InputBlocker = false;
            return;
        }

        Lift.OpenDoors();
        Camera.main.GetComponentInParent<PlayerController>().CanMove = true;
        gameObject.SetActive(false);
    }

}
