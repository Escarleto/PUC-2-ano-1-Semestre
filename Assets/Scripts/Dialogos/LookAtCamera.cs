using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {

        Transform cameraTransform = Camera.main.transform;
        transform.LookAt(cameraTransform);
    }
}
