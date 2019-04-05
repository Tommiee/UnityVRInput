using UnityEngine;
using Valve.VR;

// todo: rename
public class ChangeStateOnRotation : MonoBehaviour
{
	public RotationChecker rotationChecker;

    [SerializeField] private InteractionMachine _interactionMachine;

    public SteamVR_Action_Boolean grabGripAction;

	public SteamVR_ActionSet _ActionSet;

    private void Start()
	{
		rotationChecker.RotationEvent += OnRotation;

		_ActionSet.Activate(SteamVR_Input_Sources.Any,0,true);
    }
		
	void Update(){
		if(SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand)){
			_interactionMachine.StartApply();
		} else if(SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.LeftHand)) {
			_interactionMachine.EndApply();
		}
	}
		
    private void OnRotation(Vector3 rotation)
	{
        var amount = ((360 - rotation.z) / 360 + 0.5f) % 1;
        
        _interactionMachine.Apply(amount);
	}
}