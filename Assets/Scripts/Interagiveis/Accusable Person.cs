using UnityEngine;

public class AccusablePerson : MonoBehaviour
{
    private GameObject ShootUI;
    private enum State {HUMAN, DEAD}
    private State Is;

    void Start()
    {
        ShootUI = transform.GetChild(0).gameObject;
        ShootUI.SetActive(false);
        if (gameObject.tag == "Dead") Is = State.DEAD;
        else if (gameObject.tag == "Human") Is = State.HUMAN;
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
