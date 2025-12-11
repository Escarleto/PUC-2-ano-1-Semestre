using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Componentes
    private CharacterController Body;
    private SphereCollider InteractArea;

    //Variavéis e constantes
    const float Speed = 17f;
    public bool CanMove = true;
    private Vector2 H_Input;

    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        Body = GetComponent<CharacterController>();
        InteractArea = GetComponent<SphereCollider>();
    }
    
    private void Update() //Aqui aplicamos os movimentos a cada frame do jogo
    {
        Vector3 Movement = new Vector3(H_Input.x, 0f, H_Input.y) * Speed * Time.deltaTime; //Detecta os movimentos e aplica velocidade
        
        Body.Move(Movement); //Aplicamos todos os movimentos ao componente "CharacterController"
    }

    public void OnMove(InputAction.CallbackContext Context)//Aqui detectamos quando o jogador pressiona as teclas de andar
    {
        if (Context.performed && CanMove)
        {
            H_Input = Context.ReadValue<Vector2>();
            H_Input = H_Input.normalized; //Evita que a velocidade do player multiplique quando anda nas diagonais
        }
        else
        {
            H_Input = Vector2.zero; //Zera os inputs depois de não serem mais detectados
        }
    }

    private void OnTriggerEnter(Collider other) //Aqui detectamos colisões com o player
    {
        
    }
}
