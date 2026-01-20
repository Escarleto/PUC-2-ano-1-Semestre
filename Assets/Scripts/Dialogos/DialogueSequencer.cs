using UnityEngine;
using System;
using static DialogueHandler;

public class DialogueSequencer : MonoBehaviour
{
    private PlayerController Player;
    public DialogueLine[] DialogueLines;
    public static DialogueSequencer ActiveDialogue;
    private int CurrentText = 0;
    private DialogueHandler CurrentSpeaker;
    private bool onDialogue = false;
    public event Action OnDialogueEnded;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void StartDialogue()
    {
        ActiveDialogue = this;
        onDialogue = true;
        Player.CanMove = false;
        CurrentText = 0;
        CurrentSpeaker = DialogueLines[CurrentText].Speaker;
        PlayCurrentLine();
    }

    private void PlayCurrentLine()
    {
        CurrentSpeaker.CleanDialogueBox();
        DialogueLine Line = DialogueLines[CurrentText];
        Line.Speaker.PlayDialogue(Line.Line);
    }

    public void AdvanceDialogue()
    { 
        if (!onDialogue) return;

        CurrentSpeaker = DialogueLines[CurrentText].Speaker;

        if (CurrentSpeaker.isTyping)
            return;

        CurrentText++;

        if (CurrentText >= DialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        PlayCurrentLine();
    }

    private void EndDialogue()
    {
        onDialogue = false;
        ActiveDialogue = null;

        Player.CanMove = true;
        foreach (var Line in DialogueLines)
            Line.Speaker.CleanDialogueBox();
        OnDialogueEnded?.Invoke();
    }
}
