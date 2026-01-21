using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public int TotalDeadPeople = 0;
    private float Salario;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Salario = 50.50f;
    }

    public void AddSalario(float novoSalario)
    {
        Salario += novoSalario;
        Debug.Log("Salario Atual: " + Salario);
    }

    public void SubtractSalario(float valor)
    {
        Salario -= valor;
        Debug.Log("Salario Atual: " + Salario);
    }

    public void StartShift()
    {
        Debug.Log("Shift Started");
    }

    public void EndShift()
    {
        Debug.Log("Shift Ended");
    }
}
