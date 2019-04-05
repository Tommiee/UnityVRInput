using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CM.UI;

public class KeyboardChangeState : MonoBehaviour
{
	public InteractionMachine _interactionMachine;
	private CM_UI_System_ScreenRotation _uiSystemScreenRotation;
	private Dictionary<string, InteractionStates> _interactionStates = new Dictionary<string, InteractionStates>(){
		{ "DayNightUI", InteractionStates.DayNight},
		{ "HiddenLayerUI", InteractionStates.HiddenLayer},
		{ "MagnifierUI", InteractionStates.Magnifier},
		{ "ScalerUI", InteractionStates.Scaler}
	};

	void Awake(){
		_uiSystemScreenRotation = GetComponent<CM_UI_System_ScreenRotation>();
	}

    void Update(){
		bool _keyOne = Input.GetKeyDown(KeyCode.Alpha1);
		bool _keyTwo = Input.GetKeyDown(KeyCode.Alpha2);

		if(_keyOne){
			PreviousState();
		}
		if(_keyTwo){
			NextState();
		}
    }

	//Switches to the next state in order in the state machine
	public void NextState(){
		var currentScreen = _uiSystemScreenRotation.NextScreen();
		var currentState = _interactionStates[currentScreen.name];
		_interactionMachine.SetState(currentState);
	}

	//Switches to the previous state in order in the state machine
	public void PreviousState(){
		var previousScreen = _uiSystemScreenRotation.PreviousScreen();
		var previousState = _interactionStates[previousScreen.name];
		_interactionMachine.SetState(previousState);
	}
}
