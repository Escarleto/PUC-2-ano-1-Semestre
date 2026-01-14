using UnityEngine;

public class PannochkaDialogue : MonoBehaviour
{
    private DialogueHandler dialogueHandler;

    private void Start()  //Aqui inicializamos as variáveis quando o jogo inicia
    {
        dialogueHandler = GetComponent<DialogueHandler>();
    }

    public void ShortDialogue(string TextToSay)
    {
        dialogueHandler.PlayDialogue(TextToSay);
    }
}
