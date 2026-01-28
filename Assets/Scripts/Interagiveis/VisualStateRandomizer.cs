using UnityEngine;

public class VisualStateRandomizer : MonoBehaviour
{
    [SerializeField] private Renderer Visual;
    [SerializeField] private Material DeadClothMat;

    private static readonly Vector2 DeadSpecular = new(0.05f, 0.2f);
    private static readonly int ClothMatIndex = 0;
    private static readonly float maxTilt = 9f;

    private MaterialPropertyBlock mpb;

    private void Awake()
    {
        mpb = new MaterialPropertyBlock();
    }

    public void Apply(AccusablePerson.State state)
    {
        if (state != AccusablePerson.State.DEAD) return;

        float zTilt = Random.Range(-maxTilt, maxTilt);
        transform.localRotation *= Quaternion.Euler(0, 0, zTilt);

        bool changeCloth = Random.value < 0.5f;

        Visual.GetPropertyBlock(mpb);
        mpb.SetFloat("_Smoothness", Random.Range(DeadSpecular.x, DeadSpecular.y));
        Visual.SetPropertyBlock(mpb);

        var mats = Visual.sharedMaterials;

        if (!changeCloth) return;

        if (ClothMatIndex < 0 || ClothMatIndex >= mats.Length) return;

        mats[ClothMatIndex] = DeadClothMat;
        Visual.sharedMaterials = mats;
    }
}