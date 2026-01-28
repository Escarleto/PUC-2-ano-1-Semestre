using UnityEngine;

public class LiftManager : MonoBehaviour
{
    private BoxCollider LiftTrigger;
    public BoxCollider DoorCollision;
    private Animator LiftAnimator;
    private AudioSource ElevatorMusic;
    [SerializeField] private bool OpenDoorOnStart;
    public enum Floor {SURFACE, HELL}
    private Floor CurrentFloor = Floor.SURFACE;

    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        LiftTrigger = GetComponent<BoxCollider>();
        LiftAnimator = GetComponent<Animator>();
        ElevatorMusic = GetComponent<AudioSource>();
        LiftTrigger.size = new Vector3(3.11f, 1f, 0.95f); // Define o comprimento do collider do elevador
        if (OpenDoorOnStart) OpenDoors();
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

    public void PlayElevatorMusic() { ElevatorMusic.Play(); }

    public void OpenDoors() //Aqui abrimos as portas do elevador
    {
        LiftAnimator.SetTrigger("OpenGate");
        LiftAnimator.GetBehaviour<DisableCollision>().DoorCollision = DoorCollision;
    }

    private void CloseDoors() //Aqui fechamos as portas do elevador
    {
        LiftAnimator.SetTrigger("CloseGate");
        LiftAnimator.GetBehaviour<EnableCollision>().DoorCollision = DoorCollision;
    }

    private void OnTriggerEnter(Collider other) //Aqui detectamos quando o jogador entra na área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LiftTrigger.size = new Vector3(3.11f, 1f, 5f);
            CloseDoors();
            ChangeFloor();
        }
    }

    private void OnTriggerExit(Collider other) //Aqui detectamos quando o jogador sai da área do elevador
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LiftTrigger.size = new Vector3(3.11f, 1f, 0.95f);
            CloseDoors();
        }
    }
}
