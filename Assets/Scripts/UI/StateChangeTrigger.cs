using UnityEngine;
using Valve.VR;

public class StateChangeTrigger : MonoBehaviour
{
	public RotationChecker rotationChecker;


	public SteamVR_ActionSet _ActionSet;

    private void Start()
	{
		_ActionSet.Activate(SteamVR_Input_Sources.Any,0,true);
    }

	private void Update(){
		if(SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand)){
			rotationChecker.IsApplying = true;
		}
	}
}