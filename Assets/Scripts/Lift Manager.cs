using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private BoxCollider LiftTrigger;
    private bool IsPlayerOnLift = false;

    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        LiftTrigger = GetComponent<BoxCollider>();
        LiftTrigger.size = new Vector3(2.3f, 1.7f, 1.3f); // Define o comprimento do collider do elevador
    }

    private void Update() //Aqui aplicamos as verificações a cada frame do jogo
    {
        if (!IsPlayerOnLift) return; // Sai do método se o jogador não estiver no elevador

        // Lógica do elevador quando o jogador está dentro

    }

    private void OnTriggerEnter(Collider other) //Aqui detectamos quando o jogador entra na área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerOnLift = true; // Define o estado quando o jogador entra no elevador
            LiftTrigger.size = new Vector3(2.3f, 1.7f, 4f);
            Debug.Log("Player entrou no elevador.");
        }
    }

    private void OnTriggerExit(Collider other) //Aqui detectamos quando o jogador sai da área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayerOnLift = false; // Reseta o estado quando o jogador sai do elevador
            LiftTrigger.size = new Vector3(2.3f, 1.7f, 1.3f);
            Debug.Log("Player saiu do elevador.");
        }
    }
}
