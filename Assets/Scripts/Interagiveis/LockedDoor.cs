using UnityEngine;

public class LockedDoor : MonoBehaviour, InteractableBase
{
    public GameObject InteractUI;
    public PannochkaDialogue pannochkaDialogue;
    private SphereCollider InteractableArea;
    public string TextToSay;

    private void Start()
    {
        InteractableArea = GetComponent<SphereCollider>();
        HideInteractionUI();
    }

    public virtual void Interact()
    {
        pannochkaDialogue.BarkDialogue(TextToSay); // Inicia o diálogo da Pannochka
        HideInteractionUI();
    }

    public virtual void ShowInteractionUI() // Implementação do método ShowInteractionUI da interface
    {
        if (InteractUI.activeSelf) return;// Se a UI já estiver ativa, não faz nada
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI() // Implementação do método HideInteractionUI da interface
    {
        if (!InteractUI.activeSelf) return; // Se a UI já estiver desativada, não faz nada
        InteractUI.SetActive(false);
    }
}
