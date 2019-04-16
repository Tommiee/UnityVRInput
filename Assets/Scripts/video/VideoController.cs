using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {
	private VideoPlayer _videoPlayer;

	[SerializeField]
	private VideoClip _video;
	private bool _rewind = false;
	private int frames = 0;

	void Start() {
		StartCoroutine(PlayVideo());
	}

	IEnumerator PlayVideo() {
		GameObject videoObject = GameObject.Find("VideoObject");
		_videoPlayer = videoObject.AddComponent<VideoPlayer>();
		_videoPlayer.playOnAwake = false;
		_videoPlayer.clip = _video;
		_videoPlayer.Prepare();
		_videoPlayer.SetDirectAudioVolume(0, 1);
		_videoPlayer.SetDirectAudioMute(0, false);

		while (!_videoPlayer.isPrepared) {
			Debug.Log("Preparing Video");
			yield return null;
		}

		_videoPlayer.Play();
	}

	private void Normalize() {
		_rewind = false;
		ResetPlayback();
	}

	public void ChangeTime(int seconds) {
		_videoPlayer.time = seconds;
	}

	public void Pause() {

		if (_videoPlayer.isPaused && !_rewind) {
			Play();
			return;
		}
		_videoPlayer.Pause();
		Normalize();
	}

	private void Play() {
		_videoPlayer.Play();
	}

	public void Rewind() {
		Normalize();
		_videoPlayer.Pause();
		_rewind = true;
	}

	private void ResetPlayback() {
		_videoPlayer.playbackSpeed = 1;
	}

	public void FastForward() {
		Normalize();
		Play();
		_videoPlayer.playbackSpeed = 2;
	}

	private void Update() {
		frames++;

		if (_rewind) {
			if (frames % 10 == 0) {
				_videoPlayer.frame -= 3;
			}
		}

		if (_videoPlayer.frame <= 0) {
			_rewind = false;
		}
	}
}