﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Haptics : MonoBehaviour
{

	public SteamVR_Action_Vibration hapticAction;
	public SteamVR_Action_Boolean grabPinchAction;

    private GameObject _closest;
    private float _distToClosest;

    [SerializeField]
    private string _tag;
    [SerializeField]
    private GameObject _player;

    private FindObject _objectFinder;

    private void Start()
    {
        _objectFinder = gameObject.GetComponent<FindObject>();
    }


    //todo: make this run on event listeners instead of in update

    void Update()
    {
		if(grabPinchAction.GetStateDown(SteamVR_Input_Sources.LeftHand)){
			Pulse(2, 0.2f, 200f, 0.1f, SteamVR_Input_Sources.LeftHand);
            _closest = _objectFinder.FindClosestTarget(_player, _tag);
            StartCoroutine(Radar(_closest, _player));
        }
        if (grabPinchAction.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Pulse(2, 0.2f, 75f, 0.1f, SteamVR_Input_Sources.LeftHand);
            StopAllCoroutines();
        }
    }

	private void Pulse(int pulseAmount, float duration, float frequency, float amplitude, SteamVR_Input_Sources source){
		for(int i = 0; i < pulseAmount; i++){
			hapticAction.Execute(0,duration,frequency,amplitude,source);
		}
	}

    IEnumerator Radar(GameObject _obj, GameObject _player, bool _stop = false)
    {
        while (!_stop)
        {
            _distToClosest = Vector3.Distance(_player.transform.position, _obj.transform.position);
            print("Dist to Closest: " + _distToClosest);
            //todo: make amplitude inversely quadratic within clamp
            Pulse(1, 0.2f, Mathf.Round(320f/_distToClosest), Mathf.Clamp(1f - (_distToClosest / 10),0f,1f), SteamVR_Input_Sources.LeftHand);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
