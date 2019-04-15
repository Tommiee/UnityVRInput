using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class RotationChecker : MonoBehaviour
{
	[Dropdown("_rotationTypes")]
	public string rotationType;	

	private string[] _rotationTypes = new string[] { "This", "Parent", "Transform" };

	[ShowIf("RotationTypeIsTransform")]
	public Transform target;

	public delegate void RotationHandler(Vector3 rotation);
	public event RotationHandler RotationEvent;

	public UnityEvent onRotation;

	private Quaternion _rotationPrevious;

    public bool IsApplying { get; set; } = false;

	private void Start()
	{
		if (RotationTypeIsThis())
			target = transform;

		if (RotationTypeIsParent())
			target = transform.parent;
	}

	private void Update()
	{
        if (!IsApplying) {
            return;
        }
		if (target.transform.rotation != _rotationPrevious)
		{
			RotationEvent?.Invoke(target.transform.localEulerAngles);
			onRotation?.Invoke();
		}

		_rotationPrevious = target.transform.rotation;
	}

	private bool RotationTypeIsThis()
	{
		if (rotationType == "This")
			return true;

		return false;
	}

	private bool RotationTypeIsParent()
	{
		if (rotationType == "Parent")
			return true;

		return false;
	}

	private bool RotationTypeIsTransform()
	{
		if (rotationType == "Transform")
			return true;

		return false;
	}
}