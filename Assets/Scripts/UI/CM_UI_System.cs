using UnityEngine;
using UnityEngine.UI;

namespace CM.UI
{
	public class CM_UI_System : MonoBehaviour
	{
		public CM_UI_Screen startScreen;

		public delegate void InitializeScreensHandler(Component[] uiScreens);
		public event InitializeScreensHandler InitializeScreensEvent;

		[Header("Fader Properties")]
		public Image fader;
		public float fadeInDuration = 1f;
		public float fadeOutDuration = 1f;

		private Component[] _screens;


		private CM_UI_Screen _currentScreen;
		public CM_UI_Screen CurrentScreen
		{
			get
			{
				return _currentScreen;
			}
		}

		private void Awake()
		{
			_screens = GetComponentsInChildren<CM_UI_Screen>(true);
		}

		private void Start()
		{
			_currentScreen = startScreen;
			_currentScreen.gameObject.SetActive(true);
			_currentScreen.Open();
			//InitializeScreens();

			if (fader)
				fader.gameObject.SetActive(true);

			FadeIn();

			InitializeScreensEvent?.Invoke(_screens);
		}

		public void SwitchScreens(CM_UI_Screen screen)
		{
			if (screen)
			{
				screen.gameObject.SetActive(true);
				_currentScreen.Close();

				_currentScreen.gameObject.SetActive(false);
				_currentScreen = screen;
				_currentScreen.Open();
			}
		}

		public void TurnOffCurrentScreen()
		{
			_currentScreen.Close();
		}

		public void FadeIn()
		{
			if (fader)
			{
				fader.CrossFadeAlpha(0f, fadeInDuration, false);
			}
		}

		public void FadeOut()
		{
			if (fader)
			{
				fader.CrossFadeAlpha(0f, fadeOutDuration, false);
			}
		}

		private void InitializeScreens()
		{
			foreach (var screen in _screens)
			{
				screen.gameObject.SetActive(true);
			}
		}

		public Component[] GetScreens()
		{
			return _screens;
		}
	}
}