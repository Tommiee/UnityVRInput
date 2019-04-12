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

	IEnumerator Pulse(int _pulseAmount, float _duration, float _frequency, float _amplitude, SteamVR_Input_Sources _source)
    {
		for(int i = 0; i < _pulseAmount; i++)
        {
			_hapticOutput.Execute(0, _duration, _frequency, _amplitude,_source);
            yield return new WaitForSeconds(_duration + 0.1f);
		}
        yield break;
	}

    IEnumerator Radar(GameObject _obj, GameObject _player, SteamVR_Input_Sources _source)
    {
        while (!false)
        {
            _distToClosest = Vector3.Distance(_player.transform.position, _obj.transform.position);
            if(_distToClosest >= 0.1)
            {
                float _freq = Mathf.Clamp(Mathf.Round(320f / _distToClosest), 1, 320);
                float _amp = Mathf.Clamp(1f - (_distToClosest / 10), 0f, 1f);
                StartCoroutine(Pulse(1, 0.2f, _freq, _amp, _source));
                yield return new WaitForSeconds(0.75f);
            } else
            {
                StartCoroutine(Pulse(2, 0.5f, 50f, 1f, _source));
                yield break;
            }
        }
    }
}
