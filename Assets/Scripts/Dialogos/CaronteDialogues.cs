using System.Collections.Generic;
using UnityEngine;

public class CaronteDialogues : MonoBehaviour, InteractableBase
{    
    [SerializeField] private GameObject InteractUI;
    [SerializeField] private LiftManager Lift;
    public SphereCollider InteractionCollider;
    private Animator HandleAnimation;

    public enum CaronteStates { INTRO, NOBINOCULARS, HASBINOCULARS, ONSHIFT, ENDSHIFT }
    public CaronteStates CurrentState = CaronteStates.INTRO;

    private Dictionary<CaronteStates, DialogueSequencer> Dialogues;
    
    private bool isInteracting;

    private void Awake()
    {
        InteractionCollider = GetComponent<SphereCollider>();
        HandleAnimation = GetComponentInChildren<Animator>();

        CacheDialogues();
        HideInteractionUI();
    }

    private void CacheDialogues()
    {
        Dialogues = new Dictionary<CaronteStates, DialogueSequencer>();

        foreach (var Sequence in GetComponentsInChildren<DialogueSequencer>(true))
        {
            if (System.Enum.TryParse(Sequence.name.Replace("Dialogue", ""), out CaronteStates State))
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
            case CaronteStates.INTRO:
                Dialogues[CaronteStates.INTRO].StartDialogue();
                return;

            case CaronteStates.NOBINOCULARS:
                if (Camera.main.GetComponent<BinocularController>().HasBinoculars)
                {
                    CurrentState =CaronteStates.HASBINOCULARS;
                    StartState();
                    return;
                }
                Dialogues[CaronteStates.NOBINOCULARS].StartDialogue();
                return;

            case CaronteStates.HASBINOCULARS:
            case CaronteStates.ONSHIFT:
            case CaronteStates.ENDSHIFT:
                Dialogues[CurrentState].StartDialogue();
                return;
        }
    }

    private void OnDialogueFinished()
    {
        isInteracting = false;
        ShowInteractionUI();
        InteractionCollider.enabled = true;

        FinishState();
    }

    private void FinishState()
    {
        switch (CurrentState)
        {
            case CaronteStates.INTRO:
                CurrentState =Camera.main.GetComponent<BinocularController>().HasBinoculars
                    ? CaronteStates.HASBINOCULARS
                    : CaronteStates.NOBINOCULARS;
                return;

            case CaronteStates.HASBINOCULARS:
                HandleAnimation.SetTrigger("PunchIn");
                GetComponentInChildren<PunchIn>().InteractionCollider.enabled = true;
                InteractionCollider.enabled = false;
                HideInteractionUI();
                return;

            case CaronteStates.ENDSHIFT:
                Lift.OpenDoors();
                return;
        }
    }

    public void Punchin()
    {
        HandleAnimation.SetTrigger("PunchIn");
        Manager.Instance.StartShift();
        CurrentState =CaronteStates.ONSHIFT;
    }

    public virtual void ShowInteractionUI()
    {
        if (InteractUI.activeSelf || isInteracting) return;
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI()
    {
        if (!InteractUI.activeSelf) return;
        InteractUI.SetActive(false);
    }
}