using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public int TotalDeadPeople = 0;
    private float Salario;
    public float ShiftTime = 300f; // Duração do turno em segundos (5 minutos)

    public CaronteDialogues Caronte;
    public TimerVisual ClockUI;

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
        ClockUI.MoveTimer(267f, 169f);
        ClockUI.OnShift = true;
        StartCoroutine(ShiftDuration());
    }

    public void EndShift()
    {
        Debug.Log("Shift Ended");
        ClockUI.OnShift = false;
        ClockUI.MoveTimer(169f, 267f);
        Caronte.CurrentState = CaronteDialogues.CaronteState.ENDSHIFT;
    }

    private IEnumerator ShiftDuration()
    {
        yield return new WaitForSeconds(ShiftTime);
        EndShift();
    }
}
