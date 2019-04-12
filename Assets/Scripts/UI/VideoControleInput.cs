using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VideoControleInput : MonoBehaviour
{
    public SteamVR_ActionSet _ActionSet;
    public SteamVR_Input_Sources Hand;
    private Video _video;

    [SerializeField] private bool touchPadButtonMode;

    void Start()
    {
        _ActionSet.Activate(Hand, 0, true);
        _video = GetComponent<Video>();
    }
    void Update()
    {
        if (touchPadButtonMode)
        {
            TouchpadButtonsControler();
        }
    }

    private void TouchpadButtonsControler()
    {
        

        if (SteamVR_Actions._default.Teleport.GetStateDown(Hand))
        {
            Vector2 trackpad = SteamVR_Actions._default.touchpadtouch.GetAxis(Hand);
            float _touchPadPositionX = trackpad.x;
            //float _touchPadPositionY = trackpad.y;

            print(_touchPadPositionX + ">>" + trackpad);
            if (_touchPadPositionX < -0.5f)
            {
                _video.Rewind();
                print("rewind");
            }
            else if (_touchPadPositionX > -0.5f && _touchPadPositionX < 0.5f)
            {
                _video.Pause();
                print("pause");
            }
            else if (_touchPadPositionX > 0.5f)
            {
                _video.FastForward();
                print("fastforward");
            }
        }
    }
}