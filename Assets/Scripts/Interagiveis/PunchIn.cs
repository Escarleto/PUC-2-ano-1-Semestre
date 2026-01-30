using UnityEngine;

public class PunchIn : MonoBehaviour, InteractableBase
{
    private CaronteDialogues Caronte;
    public SphereCollider InteractionCollider;
    [SerializeField] private GameObject InteractionUI;

    private void Start()
    {
        Caronte = GetComponentInParent<CaronteDialogues>();
        InteractionCollider = GetComponent<SphereCollider>();
        InteractionCollider.enabled = false;
        HideInteractionUI();
    }

    public virtual void Interact()
    {
        Caronte.Punchin();
        Caronte.InteractionCollider.enabled = true;
        Destroy(InteractionUI);
        GetComponent<PunchIn>().enabled = false;
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
