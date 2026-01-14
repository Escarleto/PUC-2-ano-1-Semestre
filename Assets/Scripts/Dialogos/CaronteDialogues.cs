using UnityEngine;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{
    private DialogueHandler dialogueHandler;
    private SphereCollider InteractionCollider;
    public bool isInteracting = false;
    public GameObject InteractUI;
    public enum CaronteState{INTRO, ONSHIFT, ENDSHIFT}
    public CaronteState CurrentState = CaronteState.INTRO;
    private DialogueSequencer IntroDialogue;
    private DialogueSequencer ShiftDialogue;
    private DialogueSequencer EndShiftDialogue;
 

    private void Start()  // Aqui inicializamos as vari�veis quando o jogo inicia
    {
        InteractionCollider = GetComponent<SphereCollider>();
        IntroDialogue = transform.Find("IntroDialogue").GetComponent<DialogueSequencer>();
        ShiftDialogue = transform.Find("ShiftDialogue").GetComponent<DialogueSequencer>();
        EndShiftDialogue = transform.Find("EndShiftDialogue").GetComponent<DialogueSequencer>();

        IntroDialogue.OnDialogueEnded += OnDialogueFinished;
        ShiftDialogue.OnDialogueEnded += OnDialogueFinished;
        EndShiftDialogue.OnDialogueEnded += OnDialogueFinished;

        dialogueHandler = GetComponentInChildren<DialogueHandler>();
        HideInteractionUI();
    }

    public virtual void Interact() // Implementa��o do m�todo Interact da interface
    {
        if (isInteracting) return; // Se j� estiver interagindo, n�o faz nada
        CheckState();
        isInteracting = true; // Inicia a primeira sequ�ncia de di�logo
        InteractionCollider.enabled = false; // Desativa o collider de intera����o durante o di�logo
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
    private void OnDialogueFinished()
    {
        isInteracting = false;
        InteractionCollider.enabled = true; // Reativa o collider de intera����o ap�s o di�logo
        ShowInteractionUI();
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

