using UnityEngine;
using UnityEngine.UI;

namespace CM.UI
{
	public class CM_UI_System_ScreenRotation : MonoBehaviour
	{
		private CM_UI_System _uiSystem;

		private Component[] _screens;
		private int _screenIndex = 0;

		private void Awake()
		{
			_uiSystem = GetComponent<CM_UI_System>();
		}

		private void Start()
		{
			_uiSystem.InitializeScreensEvent += OnInitializeScreens;
		}

		private void OnInitializeScreens(Component[] screens)
		{
			_screens = screens;
		}

		public CM_UI_Screen NextScreen()
		{
			_screenIndex = (_screenIndex < _screens.Length-1) ? ++_screenIndex : 0;
            return setScreen(_screenIndex);
		}

        public CM_UI_Screen setScreen(int screenIndex) {
            var currentScreen = _screens[screenIndex].GetComponent<CM_UI_Screen>();
            _uiSystem.SwitchScreens(currentScreen);
            return currentScreen;
        }

		public CM_UI_Screen PreviousScreen()
		{
			_screenIndex = (_screenIndex > 0) ? --_screenIndex : _screens.Length-1;
            return setScreen(_screenIndex);
        }
	}
}