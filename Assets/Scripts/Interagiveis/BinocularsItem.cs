using UnityEngine;

public class BinocularsItem : MonoBehaviour, InteractableBase
{
    public GameObject InteractUI;

    private void Start()
    {
        HideInteractionUI();
    }

    public virtual void Interact()
    {
        gameObject.SetActive(false); // Desativa os binóculos na cena
        Camera.main.GetComponent<BinocularController>().HasBinoculars = true; // Dá os binóculos ao jogador
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
