using UnityEngine;

public class LockRotation : MonoBehaviour
{
	public RotationChecker rotationChecker;

	public bool lockX = false;
	public bool lockY = false;
	public bool lockZ = false;

	private void Start()
	{
		rotationChecker.RotationEvent += OnRotation;
		OnRotation(new Vector3());
	}

	private void OnRotation(Vector3 rotation)
	{
        Vector3 newRotation = rotation;

        /*newRotation.x = lockX ? -transform.eulerAngles.x : 0;
        newRotation.y = lockY ? -transform.eulerAngles.y : 0;
        newRotation.z = lockZ ? -transform.eulerAngles.z : 0;*/

		transform.eulerAngles = newRotation;
	}
}