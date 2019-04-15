using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RayGun : MonoBehaviour
{
    [SerializeField] private List<Material> _targetMaterials;

    public List<Material> TargetMaterials => _targetMaterials;

    void Update()
    {
        if (TargetMaterials.Count == 0)
        {
            return;
        }

        foreach (var targetMaterial in TargetMaterials)
        {
            targetMaterial.SetVector("_RayPosition", transform.position);
            targetMaterial.SetVector("_RayDirection", transform.forward);
        }
        
    }

    void OnDisable()
    {
        foreach (var targetMaterial in TargetMaterials)
        {
            targetMaterial.SetFloat("_HiddenLayerEnabled", 0f);
        }
    }

    void OnEnable()
    {
        foreach (var targetMaterial in TargetMaterials)
        {
            targetMaterial.SetFloat("_HiddenLayerEnabled", 1f);
        }
    }

    
}