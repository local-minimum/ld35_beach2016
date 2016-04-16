using UnityEngine;
using System.Collections.Generic;

public class RhythemChannel : MonoBehaviour {

	[SerializeField] Transform dropSource;

	[SerializeField] Transform failTarget;
	[SerializeField] Transform hitTarget;
	public Rhythem rhythem;

	[SerializeField] SlotButton button;

	[SerializeField] RhythemIcon template;

	public bool flipX;
	public AudioSource speaker;
	public Sprite iconImage;
	public bool autoPlay;
	public Color32 beatColor;
	public Color32 offBeatColor;

	List<RhythemIcon> icons = new List<RhythemIcon>();
	int nextFree = 0;
	float hitProgress = 1;

	[SerializeField] float hitTolerance = 0.05f;

	static float fallDuration = 1.5f;

	public Vector3 GetPosition(float progress) {
		return Vector3.Lerp(dropSource.position, failTarget.position, progress);
	}

	public float deltaProgress {
		get {
			return Time.deltaTime * rhythem.scaleFactor / fallDuration;
		}
	}

	public bool IsAutoHit(float progress) {
		return autoPlay && Mathf.Abs (progress - hitProgress) < hitTolerance;
	}

	void Start() {
		hitProgress = (hitTarget.position - dropSource.position).magnitude / (failTarget.position - dropSource.position).magnitude;
	}

	void OnEnable() {
		rhythem.OnTrackEvent += Rhythem_OnTrackEvent;
		button.OnPressEvent += Button_OnPressEvent;
	}
		
	void OnDisable() {
		rhythem.OnTrackEvent -= Rhythem_OnTrackEvent;
		button.OnPressEvent -= Button_OnPressEvent;
	}

	void Button_OnPressEvent ()
	{
		for (int i = 0, l = icons.Count; i < l; i++) {
			if (icons [i].isABeat && Mathf.Abs (icons [i].progress - hitProgress) < hitTolerance) {
				icons [i].Hit ();
				return;
			}
		}

		//TODO: Missed.. punsh?
	}

	void Rhythem_OnTrackEvent (AudioClip clip)
	{
		var icon = FreeIcon;
		icon.SetSound (clip);
		icon.SetRhythemChannel (this);
		icon.StartFall ();
			
	}

	RhythemIcon FreeIcon {
		get {
			if (nextFree < icons.Count && !icons [nextFree].falling) {
				nextFree++;
				return icons [nextFree - 1];
			} else if (icons.Count > 0 && !icons [0].falling) {
				nextFree = 1;
				return icons [0];
			} else {
				var icon = SpawnIcon ();
				icons.Add (icon);
				nextFree = icons.Count;
				return icon;
			}
		}
	}

	RhythemIcon SpawnIcon() {
		var icon = Instantiate (template);
		icon.transform.SetParent (transform);
		var v = Vector3.one;
		if (flipX)
			v.x = -1;
		icon.transform.localScale = v;
		return icon;
	}

}
