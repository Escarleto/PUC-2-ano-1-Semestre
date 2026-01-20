using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Componentes
    private CharacterController Body;
    public Transform Orientation;

    //Variavéis e constantes
    const float Speed = 10f;
    const float Gravity = -30f;
    public bool CanMove = true;
    private Vector2 H_Input;

    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        Body = GetComponent<CharacterController>();
    }

    private void Update() //Aqui aplicamos os movimentos a cada frame do jogo
    {
        if (!Body.isGrounded) //Detecta se o player não esta no chão
        {
            Body.Move(Vector3.up * Gravity * Time.deltaTime); //Aplica gravidade ao player para ele cair
            return; //Sai do método para evitar que o player ande no ar
        }

        Vector3 Movement = Orientation.forward * H_Input.y + Orientation.right * H_Input.x; //Calcula a direção do movimento baseado na orientação do player e nos inputs do jogador

        Body.Move(Movement * Speed * Time.deltaTime); //Aplicamos todos os movimentos ao componente "CharacterController"
    }

    public void OnMove(InputAction.CallbackContext Context)//Aqui detectamos quando o jogador pressiona as teclas de andar
    {  
        if (Context.performed && CanMove == true)
        {
            H_Input = Context.ReadValue<Vector2>();

            H_Input = H_Input.normalized; //Evita que a velocidade do player multiplique quando anda nas diagonais
        }
        else H_Input = Vector2.zero; //Zera os inputs depois de não serem mais detectados
    }
}
