using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhythemIcon : MonoBehaviour {

	AudioClip sound;
	RhythemChannel channel;
	public bool falling = false;
	float progress;
	Image image;

	void Awake () {
		image = GetComponent<Image> ();
	}

	void Update () {
		if (falling) {
			progress += channel.deltaProgress;
			progress = Mathf.Clamp01 (progress);
			transform.position = channel.GetPosition (progress);
			if (progress == 1f) {
				image.enabled = false;
				if (sound != null)
					channel.speaker.PlayOneShot (sound);
				falling = false;

			}
		}
	}

	public void SetSound(AudioClip clip) {
		sound = clip;
	}

	public void SetRhythemChannel(RhythemChannel channel) {
		this.channel = channel;
	}

	public void StartFall() {
		image.sprite = channel.iconImage;
		image.color = sound == null ? channel.offBeatColor : channel.beatColor;
		image.enabled = true;
		progress = Time.timeSinceLevelLoad;
		falling = true;
	}
}
