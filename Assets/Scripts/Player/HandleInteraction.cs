using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HandleInteraction : MonoBehaviour
{
    private Ray InteractRay;
    private InteractableBase Interactable;
    public float InteractReach = 1f;
    
    private void Update() //Aqui aplicamos os movimentos a cada frame do jogo
    {
        InteractRay = new Ray(transform.position, transform.forward); // Cria um raio a partir da posição da câmera na direção que ela está olhando
        
        if (!Physics.Raycast(InteractRay, out RaycastHit HitInfo, InteractReach, 3)) // Filtra os objetos não interagiveis
        {
            if (Interactable != null) Interactable.HideInteractionUI(); // Se não houver objeto interagível, esconde a UI de interação
            Interactable = null;
            return;
        }
        Interactable = HitInfo.collider.GetComponent<InteractableBase>(); // Pega o componente InteractableBase do objeto atingido pelo raio
        
        if (Interactable != null) Interactable.ShowInteractionUI(); // Mostra a UI de interação
    }

    public void TryInteract(InputAction.CallbackContext Context) // Aqui detectamos quando o jogador tenta interagir
    {
        if (!Context.performed) return;

        if (DialogueSequencer.ActiveDialogue != null)
        {
            DialogueSequencer.ActiveDialogue.AdvanceDialogue();
            return;
        }

        if (Interactable != null)
        {
            Interactable.Interact();
            Interactable.HideInteractionUI();
        }
    }
}
