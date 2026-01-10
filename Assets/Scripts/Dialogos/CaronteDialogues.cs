using UnityEngine;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{
    private DialogueHandler dialogueHandler;
    private SphereCollider InteractionCollider;
    public GameObject InteractUI;

    private void Start()
    {
        dialogueHandler = GetComponentInChildren<DialogueHandler>();
        InteractionCollider = GetComponent<SphereCollider>();
        HideInteractionUI();
    }

    public virtual void Interact() // Implementação do método Interact da interface
    {
        dialogueHandler.PlayDialogue("Ai q dor nas costa to veio"); // Inicia o diálogo específico
        InteractionCollider.enabled = false; // Desativa o collider após a interação
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
