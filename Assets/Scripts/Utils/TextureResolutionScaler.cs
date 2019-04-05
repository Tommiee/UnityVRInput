using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TextureResolutionScaler : MonoBehaviour
{

    [SerializeField] private float eyeTextureResolutionScale;

    void Start()
    {
        XRSettings.eyeTextureResolutionScale = eyeTextureResolutionScale;
    }

    
}
