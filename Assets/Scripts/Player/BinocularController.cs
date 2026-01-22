using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class BinocularController : MonoBehaviour
{   
    private Camera Cam;
    private CameraController CamController;

    public float OriginalFOV = 75f;
    private float CurrentFOV;
    private bool inBinoculars = false;
    public bool HasBinoculars = false;
    public GameObject Crosshair;
    public LayerMask InteractableLayer;

    private void Start() // Aqui inicializamos as variáveis quando o jogo inicia
    {
        CurrentFOV = OriginalFOV;
        Cam = GetComponent<Camera>(); // Pega a câmera principal
        CamController = GetComponent<CameraController>(); // Pega o componente CameraController
        Crosshair.SetActive(false); // Desativa a mira inicialmente
    }

    public void OnBinoculars(InputAction.CallbackContext Context) // Aqui detectamos quando o jogador ativa ou desativa os binóculos
    {
        if (Context.performed && HasBinoculars == true) inBinoculars = !inBinoculars; // Alterna o estado dos binóculos

        Crosshair.SetActive(inBinoculars); // Ativa ou desativa a mira com base no estado dos binóculos
        float targetFOV = inBinoculars ? OriginalFOV / 2f : OriginalFOV; // Define o campo de visão alvo com base no estado dos binóculos
        Cam.DOFieldOfView(targetFOV, 0.3f).SetEase(Ease.InOutSine); // Anima a transição do campo de visão usando DOTween
        CamController.AjustSenstivity(targetFOV); // Ajusta a sensibilidade com base no novo campo de visão
    }

    public void OnBinocularsZoom(InputAction.CallbackContext Context) // Aqui detectamos quando o jogador usa o zoom dos binóculos
    {
        if (!inBinoculars) return; // Sai do método se os binóculos não estiverem ativados

        float ZoomInput = Context.ReadValue<float>(); // Lê o valor do input de zoom

        CurrentFOV -= ZoomInput * 1f; // Ajusta o campo de visão atual com base no input
        CurrentFOV = Mathf.Clamp(CurrentFOV, 5f, OriginalFOV - 10f); // Limita o campo de visão entre o original e o máximo de zoom
        Cam.fieldOfView = CurrentFOV; // Aplica o campo de visão ajustado à câmera
        CamController.AjustSenstivity(CurrentFOV); // Ajusta a sensibilidade com base no nível de zoom
    }

    public void TryShoot(InputAction.CallbackContext Context)
    {
        if (!Context.performed || !inBinoculars) return;
        Ray ShootRange = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ShootRange, out RaycastHit HitInfo, 500f, InteractableLayer))
        {
            if (HitInfo.collider.TryGetComponent<AccusablePerson>(out AccusablePerson Accusable))
            {
                Accusable.Shot();
            }
        }
    }
}
