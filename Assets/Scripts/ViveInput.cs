using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{		
	public SteamVR_ActionSet _ActionSet;
	private Spawner _spawner;
	[SerializeField]private GameObject _obj;

	private void Start(){
		_ActionSet.Activate(SteamVR_Input_Sources.Any,0,true);
		_spawner = gameObject.GetComponent<Spawner>();
	}

	void Update(){
		if(SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any)){
			_spawner.Spawn(_obj,5);
		}

		if(SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any)){
			_spawner.Spawn(_obj,10);
		}

		if(SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any)){
			_spawner.PrintTotal();
		}
	}
}
