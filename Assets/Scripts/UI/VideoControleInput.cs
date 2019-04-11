using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VideoControleInput : MonoBehaviour
{
    public SteamVR_ActionSet _ActionSet;
    public SteamVR_Input_Sources Hand;

    [SerializeField] private bool touchPadButtonMode;

    void Start()
    {
        _ActionSet.Activate(Hand, 0, true);
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
        Vector2 trackpad = SteamVR_Actions._default.trackpad.GetAxis(Hand);
        float _touchPadPositionX = trackpad.x;
        //float _touchPadPositionY = trackpad.y;

        if (SteamVR_Actions._default.Teleport.GetStateDown(Hand))
        {
            //print(_touchPadPositionX + "<>" + _touchPadPositionY + ">>" + trackpad);
            if (_touchPadPositionX < -0.5f)
            {
                print("rewind");
            }
            else if (_touchPadPositionX > -0.5f && _touchPadPositionX < 0.5f)
            {
                print("pause");
            }
            else if (_touchPadPositionX > 0.5f)
            {
                print("fastforward");
            }
        }
    }
}