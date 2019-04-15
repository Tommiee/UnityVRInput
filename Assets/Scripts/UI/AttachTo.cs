using UnityEngine;
using UnityEngine.Animations;

public class AttachTo : MonoBehaviour
{
	public Transform target;

	private void Awake()
	{
		transform.SetParent(target);
		transform.position = transform.parent.position;
	}
}