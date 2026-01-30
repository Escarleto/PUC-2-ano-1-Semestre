using UnityEngine;

public class PegarCheque : MonoBehaviour, InteractableBase
{
    [SerializeField] private GameObject InteractionUI;
    [SerializeField] private DiaboDialogue Diabo;
    [SerializeField] private Cheque ChequeUI;

    public virtual void Interact()
    {
        ChequeUI.gameObject.SetActive(true);
        ChequeUI.OnCollected();
        Diabo.ResetInteractionDetection();
        Destroy(gameObject);
    }

    public virtual void ShowInteractionUI()
    {
        if (InteractionUI == null || InteractionUI.activeSelf == true) return;
        InteractionUI.SetActive(true);
    }

    public virtual void HideInteractionUI()
    {
        if (InteractionUI == null || InteractionUI.activeSelf == false) return;
        InteractionUI.SetActive(false);
    }
}
