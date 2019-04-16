using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ColorFlashlight : MonoBehaviour
{
    public SteamVR_ActionSet _ActionSet;

    private int minColorNumber = 0;
    private int maxColorNumber = 4;
    private int colorNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        _ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void Update()
    {
        if (SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.Y))
        {
            ChangeColorNumber();
        }
    
    }

    public int GetColorNumber()
    {
        return colorNumber;
    }

    private void ChangeColorNumber()
    {
        colorNumber++;
        if(colorNumber > maxColorNumber)
        {
            colorNumber = minColorNumber;
        }
    }

}
