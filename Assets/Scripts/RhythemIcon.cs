using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void HitEvent(Rhythem rhythem, RhythemIcon icon, int beatValue);

public class RhythemIcon : MonoBehaviour {

	public static event HitEvent OnHit;

	[HideInInspector] public AudioClip sound;
	RhythemChannel channel;
	public bool falling = false;
	[HideInInspector] public float progress;
	Image image;
	bool hit = false;
	public int beatValue = 1;

	void Awake () {
		image = GetComponent<Image> ();
	}

	public bool visibleFall {
		set {
			image.enabled = value;
		}
	}

	void Update () {
		if (falling) {
			progress += channel.deltaProgress;
			progress = Mathf.Clamp01 (progress);
			transform.position = channel.GetPosition (progress);

			if (!hit && channel.IsAutoHit(progress))
				Hit(channel.autoWorkout);

			if (progress == 1f) {
				image.enabled = false;
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

	public bool isABeat {
		get {
			return falling && !hit && sound != null;
		}
	}

	public void StartFall() {
		image.sprite = channel.iconImage;
		image.color = sound == null ? channel.offBeatColor : channel.beatColor;
		hit = sound == null;
		progress = 0;
		falling = true;
	}

	public void Hit() {
		Hit (true);
	}

	public void Hit(bool emit) {
		hit = true;
		if (OnHit != null && emit)
			OnHit (channel.rhythem, this, beatValue);
		if (sound != null)
			channel.speaker.PlayOneShot (sound);
	}
}
