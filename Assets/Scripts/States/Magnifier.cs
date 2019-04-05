using UnityEngine;

public class Magnifier : State<InteractionStates>
{
    public override InteractionStates Id => InteractionStates.Magnifier;

    [SerializeField] private Material _targetMaterial;

    public Material TargetMaterial => _targetMaterial;

    public override void Apply(float amount) {
        var magnification = 1f + amount * 2;
        TargetMaterial.SetFloat("_Magnification", magnification);
    }
}