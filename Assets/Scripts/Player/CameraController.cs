using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform Orientation;
    private BinocularController Binoculars;

    public Vector2 OriginalSense = new Vector2(6f, 4.5f);
    public Vector2 CurrentSense;
    private Vector2 LookInput;
    private float RotationX;
    private float RotationY;

    private void Start() // Aqui inicializamos as variáveis quando o jogo inicia
    {
        Binoculars = GetComponent<BinocularController>();
        CurrentSense = OriginalSense;
    }

    private void Update() //Aqui aplicamos os movimentos a cada frame do jogo
    {
        // Atualiza o campo de visão da câmera
        float mouseX = LookInput.x * CurrentSense.x * Time.deltaTime;
        float mouseY = LookInput.y * CurrentSense.y * Time.deltaTime;

        // Rotaciona a câmera e a orientação do jogador com base no input do mouse
        RotationY += mouseX;
        RotationX -= mouseY;
        RotationX = Mathf.Clamp(RotationX, -75f, 65f); // Limita a rotação vertical para evitar giros completos

        // Aplica as rotações calculadas
        Orientation.rotation = Quaternion.Euler(0f, RotationY, 0f);
        transform.rotation = Quaternion.Euler(RotationX, RotationY, 0f);
    }

    public void OnLook(InputAction.CallbackContext Context) // Aqui detectamos quando o jogador move o mouse ou o analógico direito
    {
        LookInput = Context.ReadValue<Vector2>(); // Lê o valor do input de olhar
    }

    public void AjustSenstivity(float FOV) // Ajusta a sensibilidade com base no campo de visão atual
    {
        float fovRatio = FOV / Binoculars.OriginalFOV; // Calcula a proporção do campo de visão atual em relação ao original
        CurrentSense = OriginalSense * fovRatio; // Ajusta a sensibilidade proporcionalmente
    }
}
