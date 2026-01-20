using UnityEngine;

public class ManageLightIntensity : MonoBehaviour
{
    private Light LightSource;
    private GameObject Player;
    private float InitialIntensity;

    private void Start()
    {
        LightSource = GetComponent<Light>();
        Player = GameObject.FindWithTag("Player");
        InitialIntensity = LightSource.intensity;
    }

    private void Update() 
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        // Normaliza a distância entre 0 e 1
        float t = Mathf.InverseLerp(4f, 18f, Distance);

        // Inverte (perto = menos luz, longe = mais luz)
        LightSource.intensity = Mathf.Lerp(0f, InitialIntensity, t);
    }
}
