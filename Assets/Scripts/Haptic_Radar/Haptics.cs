using System.Collections;
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

    void Update()
    {
		if(grabPinchAction.GetStateDown(SteamVR_Input_Sources.LeftHand)){
			Pulse(2,0.2f,200f,0.1f,SteamVR_Input_Sources.LeftHand);
            _closest = _objectFinder.FindClosestTarget(_player, _tag);
            StartCoroutine(Radar(_closest, _player));
        }
        if (grabPinchAction.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Pulse(2, 0.2f, 75f, 0.1f, SteamVR_Input_Sources.LeftHand);
            StopCoroutine(Radar(_closest, _player));
        }
    }

	private void Pulse(int pulseAmount, float duration, float frequency, float amplitude, SteamVR_Input_Sources source){
		for(int i = 0; i < pulseAmount; i++){
			hapticAction.Execute(0,duration,frequency,amplitude,source);
		}
	}

    IEnumerator Radar(GameObject _obj, GameObject _player)
    {
        while (true)
        {
            _distToClosest = Vector3.Distance(_player.transform.position, _obj.transform.position);
            print("Dist to Closest: " + _distToClosest);
            Pulse(1, 0.2f, 100f, 1f - (_distToClosest / 10), SteamVR_Input_Sources.LeftHand);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
