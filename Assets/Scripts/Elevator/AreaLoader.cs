using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    public GameObject City;
    public GameObject Hell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            City.SetActive(!City.activeSelf);
            Hell.SetActive(!Hell.activeSelf);
        }
    }
}
