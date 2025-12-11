using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //Componentes
    private PlayerController Player;
    private Camera Cam;

    //Variavéis e Constantes
    public float Sensitivity = 100f;
    public float Limit = 90f;
    private Vector2 LookInput;

    private void Start() //Aqui inicializamos as variáveis quando o jogo inicia
    {
        Player = GetComponentInParent<PlayerController>(); //Pega o componente do Player no pai deste objeto
        Cam = GetComponent<Camera>(); //Pega o componente "Camera" anexado a este objeto
    }

    public void OnLook(InputAction.CallbackContext Context) //Detecta quando o mouse move
    {
        if (Context.performed)
        {
            LookInput = Context.ReadValue<Vector2>(); //Adiciona os inputs a uma variavel
        }
    }

    private void Update() //Aqui aplicamos os movimentos a cada frame do jogo
    {
        Vector2 CamRotation = new Vector2(LookInput.x, LookInput.y) * Sensitivity * Time.deltaTime;
        CamRotation.y = Mathf.Clamp(CamRotation.y, -Limit, Limit); //Limita até onde o player pode virar a camera verticalmente

        transform.localRotation = Quaternion.Euler(CamRotation.y, 0f, 0f); //Roda a camera verticalmente no proprio eixo
        Player.transform.Rotate(Vector3.up * CamRotation.x); //Roda a camera e todo o Player horizontalmente
    }
}
