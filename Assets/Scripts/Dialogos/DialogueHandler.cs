using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueHandler : MonoBehaviour
{
    private TextMeshProUGUI TextComponent;
    private AudioSource Voice;
    public bool isTyping = false;
    private float TypingSpeed = 0.075f;

    [System.Serializable]
    public class DialogueLine
    {
        public DialogueHandler Speaker;
        [TextArea(2, 4)]
        public string Line;
    }

    private void Awake() // Aqui inicializamos as variáveis quando o jogo inicia
    {
        TextComponent = GetComponentInChildren<TextMeshProUGUI>();
        if (TextComponent == null) TextComponent = GetComponent<TextMeshProUGUI>();
        Voice = GetComponent<AudioSource>();
        CleanDialogueBox();
    }

    public void CleanDialogueBox() // Aqui limpamos a caixa de diálogo
    {
        TextComponent.text = "";
    }

    public void PlayDialogue(string TextToSay) // Aqui iniciamos o efeito de digitação
    {
        if (TextComponent == null || isTyping == true) return; // Se o componente de texto não estiver atribuído ou já estiver digitando, não faz nada

        StopAllCoroutines(); // Para qualquer efeito de digitação que esteja em andamento
        CleanDialogueBox(); // Limpa a caixa de diálogo antes de iniciar um novo efeito
        StartCoroutine(TypeText(TextToSay));
    }

    private IEnumerator TypeText(string TextToSay) // Aqui aplicamos o efeito de digitação
    {
        isTyping = true;
        
        foreach (char Letter in TextToSay.ToCharArray()) // Percorre cada letra do texto a ser dito
        {
            TextComponent.text += Letter; // Adiciona a letra atual ao componente de texto
            Voice.pitch = Random.Range(0.9f, 1f); //Adiciona variação de pitch para o som da voz
            Voice.Play(); // Toca o som da voz para cada letra digitada
            yield return new WaitForSeconds(TypingSpeed); //Espera um curto período antes de adicionar a próxima letra
        }

        isTyping = false;
    }
}
