using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamMaterial : MonoBehaviour
{
        
    public int FPS { get; set; } = 30;
    public bool RespectAspectRatio { get; set; } = false;
    public int SelectedCameraIndex {
        get { return _selectedCameraIndex; }
        set { _selectedCameraIndex = value;  }
    }
    public int Width {
        get { return _width; }
        set { _width = value; }
    }
    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }

    private WebCamTexture _webcamTexture;

    [SerializeField]
    private int _selectedCameraIndex;

    [SerializeField]
    private int _width = 1280;

    [SerializeField]
    private int _height = 720;

    void Start()
    {
        SetupTexture();
        if (RespectAspectRatio) {
            SetAspectRatio();
        }
        Play();
    }

    void SetupTexture()
    {
        _webcamTexture = new WebCamTexture(WebCamTexture.devices[SelectedCameraIndex].name, Width, Height, FPS);
        var renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = _webcamTexture;
    }

    void SetAspectRatio()
    {
        var aspectRatio = Width / (float) Height;
        transform.localScale = new Vector3(transform.localScale.x * aspectRatio, transform.localScale.y,
            transform.localScale.z);
    }

    void Play()
    {
        _webcamTexture.Play();
    }
}