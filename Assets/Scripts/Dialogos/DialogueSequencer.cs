using UnityEngine;
using UnityEngine.InputSystem;
using static DialogueHandler;

public class DialogueSequencer : MonoBehaviour
{
    public DialogueLine[] dialogueLines;
    public static DialogueSequencer ActiveDialogue;
    private int CurrentText = 0;
    private DialogueHandler CurrentSpeaker;
    private bool dialogueActive = false;

    public void StartDialogue()
    {
        ActiveDialogue = this;
        dialogueActive = true;
        CurrentText = 0;
        CurrentSpeaker = dialogueLines[CurrentText].Speaker;
        PlayCurrentLine();
    }

    void PlayCurrentLine()
    {
        DialogueLine Line = dialogueLines[CurrentText];
        Line.Speaker.PlayDialogue(Line.Line);
    }

    public void AdvanceDialogue()
    { 
        if (!dialogueActive) return;
        CurrentSpeaker.CleanDialogueBox();
        CurrentSpeaker = dialogueLines[CurrentText].Speaker;

        if (CurrentSpeaker.isTyping)
            return;

        CurrentText++;

        if (CurrentText >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        PlayCurrentLine();
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        ActiveDialogue = null;

        foreach (var Line in dialogueLines)
            Line.Speaker.CleanDialogueBox();
    }
}
