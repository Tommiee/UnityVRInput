using UnityEngine;

[ExecuteInEditMode]
public class ViewportDisplacement : MonoBehaviour
{
    [SerializeField] private Material _effectMaterial;

    public Material EffectMaterial
    {
        get { return _effectMaterial; }
        set { _effectMaterial = value; }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (EffectMaterial == null)
        {
            return;
        }

        Graphics.Blit(src, dst, EffectMaterial);
    }
}