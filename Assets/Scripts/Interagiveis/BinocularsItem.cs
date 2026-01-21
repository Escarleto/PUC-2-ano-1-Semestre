using UnityEngine;

public class BinocularsItem : MonoBehaviour, InteractableBase
{
    public GameObject InteractUI;
    public PannochkaDialogue pannochkaDialogue;

    private void Start()
    {
        HideInteractionUI();
    }

    public virtual void Interact()
    {
        Camera.main.GetComponent<BinocularController>().HasBinoculars = true; // Dá os binóculos ao jogador
        Camera.main.transform.Find("binóculos").gameObject.SetActive(true); // Ativa o objeto dos binóculos na câmera
        pannochkaDialogue.BarkDialogue("Encontrei");
        gameObject.SetActive(false);
        Destroy(gameObject); // Destroi o objeto dos binóculos ao interagir
    }

    public virtual void ShowInteractionUI() // Implementação do método ShowInteractionUI da interface
    {
        if (InteractUI.activeSelf) return;// Se a UI já estiver ativa, não faz nada
        InteractUI.SetActive(true);
    }

    public virtual void HideInteractionUI() // Implementação do método HideInteractionUI da interface
    {
        if (!InteractUI.activeSelf) return; // Se a UI já estiver desativada, não faz nada
        InteractUI.SetActive(false);
    }
}
