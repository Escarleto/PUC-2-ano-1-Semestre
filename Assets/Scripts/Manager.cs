using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public int TotalDeadPeople = 0;
    private float Salario;

    public CaronteDialogues Caronte;

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

    public void ChangeSalario(float ChangeTo)
    {
        Salario += ChangeTo;
        Debug.Log("Salario Atual: " + Salario);
    }

    public void StartShift()
    {
        Debug.Log("Shift Started");
        Caronte.CurrentState = CaronteDialogues.CaronteState.ONSHIFT;
        StartCoroutine(ShiftDuration());
    }

    public void EndShift()
    {
        Debug.Log("Shift Ended");
        Caronte.CurrentState = CaronteDialogues.CaronteState.ENDSHIFT;
    }

    private IEnumerator ShiftDuration()
    {
        yield return new WaitForSeconds(5);
        EndShift();
    }
}
