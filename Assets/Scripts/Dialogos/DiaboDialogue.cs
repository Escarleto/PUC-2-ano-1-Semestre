using System.Collections.Generic;
using UnityEngine;

public class DiaboDialogue : MonoBehaviour, InteractableBase
{
    [SerializeField] private GameObject InteractionUI;
    [SerializeField] private GameObject Check;
    private SphereCollider InteractionCollider;
    private Animator HandleAnimation;

    public enum DiaboStates { INTRO, POSTCHECK };
    private DiaboStates CurrentState = DiaboStates.INTRO;

    private Dictionary<DiaboStates, DialogueSequencer> Dialogues;
    
    private bool isInteracting;

    private void Awake()
    {
        InteractionCollider = GetComponent<SphereCollider>();
        HandleAnimation = GetComponentInChildren<Animator>();
        Check.SetActive(false);

        CacheDialogues();
        HideInteractionUI();
    }

    private void CacheDialogues()
    {
        Dialogues = new Dictionary<DiaboStates, DialogueSequencer>();

        foreach (var Sequence in GetComponentsInChildren<DialogueSequencer>(true))
        {
            if (System.Enum.TryParse(Sequence.name.Replace("Dialogue", ""), out DiaboStates State))
            {
                Dialogues[State] = Sequence;
                Sequence.OnDialogueEnded += OnDialogueFinished;
            }
        }
    }

    public virtual void Interact()
    {
        if (isInteracting) return;

        isInteracting = true;
        InteractionCollider.enabled = false;
        HideInteractionUI();

        StartState();
    }

    private void StartState()
    {
        switch (CurrentState)
        {
            case DiaboStates.INTRO:
                Dialogues[DiaboStates.INTRO].StartDialogue();
                return;
            case DiaboStates.POSTCHECK:
                Dialogues[CurrentState].StartDialogue();
                return;
        }
    }

    private void OnDialogueFinished()
    {
        isInteracting = false;

        FinishState();
    }

    private void FinishState()
    {
        switch (CurrentState)
        {
            case DiaboStates.INTRO:
                HandleAnimation.SetTrigger("SnapFingers");
                return;

            case DiaboStates.POSTCHECK:
                ResetInteractionDetection();
                return;
        }
    }

    public void OnFingersSnapped()
    {
        Check.SetActive(true);
        CurrentState = DiaboStates.POSTCHECK;
    }

    public void ResetInteractionDetection()
    {
        InteractionCollider.enabled = true;
        ShowInteractionUI();
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
