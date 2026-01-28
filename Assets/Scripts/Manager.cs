using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private HashSet<int> deadIDs = new HashSet<int>();
    
    public float Salario;
    public float ShiftTime = 150f; // Duração do turno em segundos (2m 30s)

    [SerializeField] private CaronteDialogues Caronte;
    [SerializeField] private TimerVisual ClockUI;
    [SerializeField] private KidsBehaviour Kids;
    private PlayerController Player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Player = Camera.main.GetComponentInParent<PlayerController>();
        Salario = 50.50f;

        var people = FindObjectsByType<AccusablePerson>(FindObjectsSortMode.None);
        ChooseDeadPeople(new List<AccusablePerson>(people)); 
    }

    public void ChangeSalario(float ChangeTo)
    {
        Salario += ChangeTo;
    }

    public void StartShift()
    {
        ClockUI.MoveTimer(267f, 169f);
        ClockUI.OnShift = true;
        Caronte.CurrentState = CaronteDialogues.CaronteState.ONSHIFT;
        Kids.StartCoroutine(Kids.KidsCycle());
        StartCoroutine(ShiftDuration());
    }

    public void EndShift()
    {
        ClockUI.OnShift = false;
        ClockUI.MoveTimer(169f, 267f);
        Kids.StopCycle();
        Caronte.CurrentState = CaronteDialogues.CaronteState.ENDSHIFT;
    }

    private IEnumerator ShiftDuration()
    {
        yield return new WaitForSeconds(ShiftTime);
        EndShift();
    }

    public void ChooseDeadPeople(List<AccusablePerson> people)
    {
        deadIDs.Clear();

        while (deadIDs.Count < 15)
        {
            int index = Random.Range(0, people.Count);
            deadIDs.Add(people[index].GetInstanceID());
        }
    }

    public bool IsDead(AccusablePerson person)
    {
        return deadIDs.Contains(person.GetInstanceID());
    }

    public void RemoveDead(AccusablePerson person)
    {
        deadIDs.Remove(person.GetInstanceID());
    }
}
