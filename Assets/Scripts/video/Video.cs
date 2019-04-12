using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour {
	private VideoPlayer videoPlayer;

	[SerializeField]
	private VideoClip _video;
	private bool _rewind = false;
	public string _videoState;

	void Start() {
		StartCoroutine(PlayVideo());
	}

	IEnumerator PlayVideo() {
		GameObject videoObject = GameObject.Find("VideoObject");
		videoPlayer = videoObject.AddComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.clip = _video;
		videoPlayer.Prepare();
		videoPlayer.SetDirectAudioMute(0, true);



		while (!videoPlayer.isPrepared) {
			Debug.Log("Preparing Video");
			yield return null;
		}

		videoPlayer.Play();

		while (videoPlayer.isPlaying) {
			Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
			yield return null;
		}
	}


	public void Normalize() {
		PlayNormal();
		_rewind = false;
	}

	public void ChangeTime(int seconds) {
		videoPlayer.time = seconds;
	}

	public void Pause() {
		Normalize();
		videoPlayer.Pause();

		if (videoPlayer.isPaused) {
			Play();
		}
	}

	public void Play() {
		videoPlayer.Play();
	}

	public void Rewind() {
		Normalize();
		if (_rewind) {
			videoPlayer.Play();
			_rewind = false;
		}

		_rewind = true;
		videoPlayer.Pause();

	}

	public void PlayNormal() {
		videoPlayer.playbackSpeed = 1;
	}

	public void FastForward() {
		Normalize();
		videoPlayer.playbackSpeed = 2;
	}

	private void Update() {
		if (_rewind) {
			videoPlayer.frame--;
		}

		if (videoPlayer.frame <= 0) {
			_rewind = false;
		}


	}
}