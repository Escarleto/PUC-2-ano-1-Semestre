using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    void LateUpdate()
    {
        transform.position = Target.transform.position + new Vector3(5.7f, 6.5f, -6.4f);
    }
}
