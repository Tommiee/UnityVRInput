using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

[RequireComponent(typeof(Slider))]
public class ChangeSliderOnRotation : MonoBehaviour
{
	public RotationChecker rotationChecker;

	public enum EQuaternion { X, Y, Z };
	public EQuaternion rotationAxis = EQuaternion.Z;

    

    private Slider _slider;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
    }

    

    private void Start()
	{
		rotationChecker.RotationEvent += OnRotation;
	}

	private void OnRotation(Vector3 rotation)
	{
        
		if (rotationAxis == EQuaternion.X)
			_slider.value = rotation.x;

		if (rotationAxis == EQuaternion.Y)
			_slider.value = rotation.y;

        if (rotationAxis == EQuaternion.Z)
            _slider.value = ((360 - rotation.z) / 360 + 0.5f)%1*2-1;

    }
}