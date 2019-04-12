using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Haptics : MonoBehaviour
{

    public SteamVR_Action_Vibration _hapticOutput;
	public SteamVR_Action_Boolean _buttonAction;

    private GameObject _closest;
    private float _distToClosest;

    [SerializeField]
    private string _tag;
    [SerializeField]
    private GameObject _controller;

    private FindObject _objectFinder;

    private void Start()
    {
        _objectFinder = gameObject.AddComponent<FindObject>();
        _buttonAction.AddOnChangeListener(ButtonTriggered, SteamVR_Input_Sources.LeftHand);
    }

    private void ButtonTriggered(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool isDown)
    {
        if (isDown)
        {
            _closest = _objectFinder.FindClosestTarget(_controller, _tag);
            StartCoroutine(Radar(_closest, _controller, SteamVR_Input_Sources.LeftHand));
        } else
        {
            StopAllCoroutines();
        }
    }

	IEnumerator Pulse(int pulseAmount, float duration, float frequency, float amplitude, SteamVR_Input_Sources source){
		for(int i = 0; i < pulseAmount; i++){
			_hapticOutput.Execute(0,duration,frequency,amplitude,source);
            print("pulse");
            yield return new WaitForSeconds(duration);
		}
        yield break;
	}

    IEnumerator Radar(GameObject _obj, GameObject _player, SteamVR_Input_Sources source)
    {
        while (!false)
        {
            _distToClosest = Vector3.Distance(_player.transform.position, _obj.transform.position);
            if(_distToClosest >= 0.1)
            {
                Pulse(1, 0.2f, Mathf.Clamp(Mathf.Round(320f / _distToClosest), 5, 300), Mathf.Clamp(1f - (_distToClosest / 10), 0f, 1f), source);
                yield return new WaitForSeconds(0.75f);
            } else
            {
                Pulse(2, 0.75f, 100f, 1f, source);
                yield break;
            }
        }
    }
}
