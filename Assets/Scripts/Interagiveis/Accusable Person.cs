using UnityEngine;

public class AccusablePerson : MonoBehaviour
{
    private enum State {HUMAN, DEAD}
    private State Is;

    void Start()
    {
        ChooseDead();
    }

    public void Shot()
    {
        switch (Is)
        {
            case State.HUMAN:
                Human();
                return;
            case State.DEAD:
                Dead();
                return;
            default:
                Debug.LogError("Estado desconhecido");
                return;
        }
    }
    
    private void ChooseDead()
    {
        float Chance = Random.Range(0f, 1f);
        if (Chance <= 0.75f)
        {
            Is = State.HUMAN;
            return;
        }
        else
        {
            if(Manager.Instance.TotalDeadPeople >= 10)
            {
                Is = State.HUMAN;
                return;
            }
            Is = State.DEAD;
            Manager.Instance.TotalDeadPeople += 1;
        }
    }

    private void Human()
    {
        Debug.Log("Interagiu com pessoa viva");
        Manager.Instance.SubtractSalario(35.75f);
    }

    private void Dead()
    {
        Manager.Instance.AddSalario(30.50f);
        Destroy(gameObject);
    }
}
