using System.Collections;
using UnityEngine;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{
    private DialogueHandler dialogueHandler;
    private bool isInteracting = false;
    public GameObject InteractUI;
    public DialogueSequencer FirstSequence;

    private void Start()  // Aqui inicializamos as variáveis quando o jogo inicia
    {
        dialogueHandler = GetComponentInChildren<DialogueHandler>();
        HideInteractionUI();
    }

    public virtual void Interact() // Implementação do método Interact da interface
    {
        if (isInteracting) return; // Se já estiver interagindo, não faz nada
        isInteracting = true;
        FirstSequence.StartDialogue(); // Inicia a primeira sequência de diálogo
        HideInteractionUI();
    }

    public virtual void ShowInteractionUI() // Implementação do método ShowInteractionUI da interface
    {
        if (InteractUI.activeSelf || isInteracting == true) return;// Se a UI já estiver ativa, não faz nada
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI() // Implementação do método HideInteractionUI da interface
    {
        if (!InteractUI.activeSelf) return; // Se a UI já estiver desativada, não faz nada
        InteractUI.SetActive(false);
    }
}

