using System.Collections;
using UnityEngine;

public class PannochkaDialogue : MonoBehaviour
{
    private DialogueHandler dialogueHandler;
    private Coroutine currentDialogueRoutine;

    private void Start()  //Aqui inicializamos as variáveis quando o jogo inicia
    {
        dialogueHandler = GetComponent<DialogueHandler>();
    }

    public void BarkDialogue(string textToSay)
    {
        // Se já houver um diálogo rodando, cancela
        if (currentDialogueRoutine != null)
            StopCoroutine(currentDialogueRoutine);

        dialogueHandler.PlayDialogue(textToSay);
        currentDialogueRoutine = StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(3f);
        dialogueHandler.CleanDialogueBox();
        currentDialogueRoutine = null;
    }
}
