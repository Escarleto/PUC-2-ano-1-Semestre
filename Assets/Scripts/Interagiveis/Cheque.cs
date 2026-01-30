using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Cheque : MonoBehaviour
{
    [SerializeField] PannochkaDialogue Pannochka;
    [SerializeField] TextMeshProUGUI Money;
    [SerializeField] LiftManager Lift;

    private void Start()
    {
       gameObject.SetActive(false); 
    }

    public void OnCollected()
    {
        float Salario = Manager.Instance.Salario;
        string Reaction = " ";
        Camera.main.GetComponentInParent<PlayerController>().CanMove = false;
        Money.text = " ";
        Money.text = Salario.ToString();

        if (Salario <= 0f) Reaction = "...";

        else if (Salario > 0f && Salario <= 35f) Reaction = "Não pode continuar assim..";

        else if (Salario > 35f && Salario <= 57f) Reaction = "Estou bem por agora..";

        else if (Salario > 57f && Salario <= 100f) Reaction = "Ufa..";

        else if (Salario > 100f && Salario <= 150f) Reaction = "E não é que o Diabo compensa..";

        else Reaction = "UAU!";

        Pannochka.BarkDialogue(Reaction);
    }

    public void OnClosed()
    {
        Lift.OpenDoors();
        Camera.main.GetComponentInParent<PlayerController>().CanMove = true;
        gameObject.SetActive(false);
    }

}
