using UnityEngine;
using System.Collections.Generic;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{
    private SphereCollider InteractionCollider;
    public bool isInteracting = false;
    public GameObject InteractUI;
    public enum CaronteState{INTRO, NOBINOCULARS, HASBINOCULARS, ONSHIFT, ENDSHIFT}
    public CaronteState CurrentState = CaronteState.INTRO;
    private Dictionary<string, DialogueSequencer> Dialogues;


    private void Start()  // Aqui inicializamos as variáveis quando o jogo inicia
    {
        InteractionCollider = GetComponent<SphereCollider>();

        Dialogues = new Dictionary<string, DialogueSequencer>();

        DialogueSequencer[] Sequences = GetComponentsInChildren<DialogueSequencer>(true);

        foreach (DialogueSequencer Sequence in Sequences)
        {
            Dialogues.Add(Sequence.name, Sequence);
            Sequence.OnDialogueEnded += OnDialogueFinished;
        }

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
                Dialogues["IntroDialogue"].StartDialogue();
                if (Camera.main.GetComponent<BinocularController>().HasBinoculars) CurrentState = CaronteState.HASBINOCULARS;
                else CurrentState = CaronteState.NOBINOCULARS;
                return;
            case CaronteState.NOBINOCULARS:
                if (Camera.main.GetComponent<BinocularController>().HasBinoculars)
                {
                    CurrentState = CaronteState.HASBINOCULARS;
                    return;
                }       
                Dialogues["NoBinocularsDialogue"].StartDialogue();
                return;
            case CaronteState.HASBINOCULARS:
                Dialogues["HasBinocularsDialogue"].StartDialogue();
                CurrentState = CaronteState.ONSHIFT;
                return;
            case CaronteState.ONSHIFT:
                Dialogues["ShiftDialogue"].StartDialogue();
                return;
            case CaronteState.ENDSHIFT:
                Dialogues["EndShiftDialogue"].StartDialogue();
                return;
            default:
                Debug.LogWarning("Caronte está em um estado inválido.");
                return;
        }
    }
    private void OnDialogueFinished()
    {
        isInteracting = false;
        InteractionCollider.enabled = true; // Reativa o collider de interação após o diálogo
        ShowInteractionUI();
    }

    public virtual void ShowInteractionUI() // Implementação do método ShowInteractionUI da interface
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

