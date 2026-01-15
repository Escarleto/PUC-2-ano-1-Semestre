using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private BoxCollider LiftTrigger;
    private Animator LiftAnimator;
    public enum Floor {SURFACE, HELL}
    private Floor CurrentFloor = Floor.SURFACE;


    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        LiftTrigger = GetComponent<BoxCollider>();
        LiftAnimator = GetComponent<Animator>();
        LiftTrigger.size = new Vector3(2.3f, 1.7f, 1.3f); // Define o comprimento do collider do elevador
    }

    private void ChangeFloor() //Aqui implementamos a lógica para mudar de andar
    {
        switch (CurrentFloor)
        {
            case Floor.SURFACE:
                LiftAnimator.SetTrigger("Descend");
                CurrentFloor = Floor.HELL;
                break;
            case Floor.HELL:
                LiftAnimator.SetTrigger("Ascend");
                CurrentFloor = Floor.SURFACE;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) //Aqui detectamos quando o jogador entra na área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeFloor();
            LiftTrigger.size = new Vector3(2.3f, 1.7f, 4f);
            Debug.Log("Player entrou no elevador.");
        }
    }

    private void OnTriggerExit(Collider other) //Aqui detectamos quando o jogador sai da área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LiftTrigger.size = new Vector3(2.3f, 1.7f, 1.3f);
        }
    }
}
