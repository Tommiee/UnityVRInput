using UnityEngine;
using UnityEngine.Events;

namespace CM.UI
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CanvasGroup))]
	public class CM_UI_Screen : MonoBehaviour
	{
		[SerializeField] private UnityEvent openEvent;
        
		private Animator _animator;

		private void Start()
		{
			_animator = GetComponent<Animator>();
		}

		public void Close()
		{
			HandleAnimator("hide");
		}

		public void Open()
		{
			openEvent.Invoke();

			HandleAnimator("show");
		}

		private void HandleAnimator(string trigger)
		{
			if (_animator)
				_animator.SetTrigger(trigger);
		}
	}
}