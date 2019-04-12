using System;
using System.Collections.Generic;
using CM.UI;
using UnityEngine;
using Valve.VR;

public class ChangeInteractionState : MonoBehaviour
{
	private CM_UI_System_ScreenRotation _uiSystemScreenRotation;
    private Dictionary<string, InteractionStates> _interactionStates = new Dictionary<string, InteractionStates>(){
        { "DayNightUI", InteractionStates.DayNight},
        { "HiddenLayerUI", InteractionStates.HiddenLayer},
        { "MagnifierUI", InteractionStates.Magnifier},
        { "ScalerUI", InteractionStates.Scaler}
    };
		
	public SteamVR_ActionSet _ActionSet;

    [SerializeField] private InteractionMachine _interactionMachine;

	void Start(){
		_ActionSet.Activate(SteamVR_Input_Sources.Any,0,true);
	}

    private void Awake()
	{
		_uiSystemScreenRotation = GetComponent<CM_UI_System_ScreenRotation>();
    }

	void Update(){
		if(SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand)){
			var currentScreen = _uiSystemScreenRotation.NextScreen();
			var currentState = _interactionStates[currentScreen.name];
			//_interactionMachine.SetState(currentState);
		}
	}
}