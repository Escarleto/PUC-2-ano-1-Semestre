using UnityEngine;

public class AscendingEffect : MonoBehaviour
{
    private Material Mat;

    private void Start()
    {
        Mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Mat.mainTextureOffset -= new Vector2(0f, Time.deltaTime * 0.75f);
    }
}
