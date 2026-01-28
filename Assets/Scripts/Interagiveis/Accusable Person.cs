using UnityEngine;

public class AccusablePerson : MonoBehaviour
{
    public enum State { HUMAN, DEAD }
    private State state;

    private VisualStateRandomizer Visuals;

    private void Start()
    {
        Visuals = GetComponent<VisualStateRandomizer>();

        state = Manager.Instance.IsDead(this)
            ? State.DEAD
            : State.HUMAN;

        Visuals.Apply(state);
    }

    public void Shot()
    {
        if (state == State.DEAD)
            Dead();
        else
            Human();
    }

    private void Human()
    {
        Manager.Instance.ChangeSalario(-35.75f);
    }

    private void Dead()
    {
        Manager.Instance.ChangeSalario(30.50f);
        Manager.Instance.RemoveDead(this);
        Destroy(gameObject);
    }
}