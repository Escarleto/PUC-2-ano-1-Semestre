using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Variavéis
    private Vector2 H_Move;
    private float V_Move = 0f;
    private float Gravity = -9.81f;
    public float Speed = 5f;


    void Start() //Roda unica vez quando o jogo inicia
    {

    }
    
    void Update() //Roda a cada frame do jogo
    {
        //Movimentação Horizontal
        H_Move.x = Input.GetAxisRaw("Horizontal");
        H_Move.y = Input.GetAxisRaw("Vertical");

        //Aplica gravidade
        if (!OnGround())
        {
            V_Move += Gravity * Time.deltaTime;
            Gravity += Gravity * Time.deltaTime;
        }
        else
        {
            V_Move = 0f;
            if (Gravity != -9.81f)
            {
                Gravity = -9.81f;
            }
        }

        //Aplica os movimentos
        transform.Translate(new Vector3(H_Move.x, V_Move, H_Move.y) * Speed * Time.deltaTime);
    }

    private bool OnGround() //Detecta se o jogador está no chão
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
