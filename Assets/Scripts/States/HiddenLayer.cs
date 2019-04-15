using UnityEngine;

public class HiddenLayer : State<InteractionStates>
{
    public override InteractionStates Id => InteractionStates.HiddenLayer;

    [SerializeField] private RayGun _ray;

    public RayGun Ray => _ray;

    public override void Enter()
    {
        Ray.enabled = true;
    }

    public override void Leave()
    {
        Ray.enabled = false;
    }

    public override void Init()
    {
        Ray.enabled = false;
    }

    public override void Apply(float amount) {
        
	}
}