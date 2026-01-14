using System.Collections;
using UnityEngine;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{
    private DialogueHandler dialogueHandler;
    public bool isInteracting = false;
    public GameObject InteractUI;
    public enum CaronteState{INTRO, ONSHIFT, ENDSHIFT}
    public CaronteState CurrentState = CaronteState.INTRO;
    private DialogueSequencer IntroDialogue;
    private DialogueSequencer ShiftDialogue;
    private DialogueSequencer EndShiftDialogue;
 

    private void Start()  // Aqui inicializamos as vari�veis quando o jogo inicia
    {
        IntroDialogue = transform.Find("IntroDialogue").GetComponent<DialogueSequencer>();
        ShiftDialogue = transform.Find("ShiftDialogue").GetComponent<DialogueSequencer>();
        EndShiftDialogue = transform.Find("EndShiftDialogue").GetComponent<DialogueSequencer>();
        dialogueHandler = GetComponentInChildren<DialogueHandler>();
        HideInteractionUI();
    }

    public virtual void Interact() // Implementa��o do m�todo Interact da interface
    {
        if (isInteracting) return; // Se j� estiver interagindo, n�o faz nada
        CheckState();
        isInteracting = true; // Inicia a primeira sequ�ncia de di�logo
        HideInteractionUI();
    }

    private void CheckState()
    {
        switch (CurrentState)
        {
            case CaronteState.INTRO:
                IntroDialogue.StartDialogue();
                CurrentState = CaronteState.ONSHIFT;
                break;
            case CaronteState.ONSHIFT:
                ShiftDialogue.StartDialogue();
                break;
            case CaronteState.ENDSHIFT:
                EndShiftDialogue.StartDialogue();
                break;
        }
    }

    public virtual void ShowInteractionUI() // Implementa��o do m�todo ShowInteractionUI da interface
    {
        if (InteractUI.activeSelf || isInteracting == true) return;// Se a UI j� estiver ativa, n�o faz nada
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI() // Implementa��o do m�todo HideInteractionUI da interface
    {
        if (!InteractUI.activeSelf) return; // Se a UI j� estiver desativada, n�o faz nada
        InteractUI.SetActive(false);
    }
}

