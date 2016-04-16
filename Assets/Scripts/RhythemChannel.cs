using UnityEngine;
using System.Collections.Generic;

public class RhythemChannel : MonoBehaviour {

	[SerializeField] Transform dropSource;

	[SerializeField] Transform failTarget;

	[SerializeField] Rhythem rhythem;

	[SerializeField] RhythemIcon template;

	public bool flipX;
	public AudioSource speaker;
	public Sprite iconImage;

	public Color32 beatColor;
	public Color32 offBeatColor;

	List<RhythemIcon> icons = new List<RhythemIcon>();
	int nextFree = 0;

	static float fallDuration = 1.5f;

	public Vector3 GetPosition(float progress) {
		return Vector3.Lerp(dropSource.position, failTarget.position, progress);
	}

	public float deltaProgress {
		get {
			return Time.deltaTime * rhythem.scaleFactor / fallDuration;
		}
	}

	void OnEnable() {
		rhythem.OnTrackEvent += Rhythem_OnTrackEvent;
	}

	void OnDisable() {
		rhythem.OnTrackEvent -= Rhythem_OnTrackEvent;
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
