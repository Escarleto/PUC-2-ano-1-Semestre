using UnityEngine;

public class AccusablePerson : MonoBehaviour
{
    private enum State { HUMAN, DEAD }
    private State Is;

    private void Start() { ChooseDead(); }

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
        Manager.Instance.ChangeSalario(-35.75f);
    }

    private void Dead()
    {
        Manager.Instance.ChangeSalario(30.50f);
        Destroy(gameObject);
    }
}
