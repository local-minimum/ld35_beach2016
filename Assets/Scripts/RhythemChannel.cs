using UnityEngine;
using System.Collections.Generic;

public class RhythemChannel : MonoBehaviour {

	[SerializeField] Transform dropSource;

	[SerializeField] Transform failTarget;

	[SerializeField] Rhythem rhythem;

	[SerializeField] RhythemIcon template;

	List<RhythemIcon> icons = new List<RhythemIcon>();

	static float fallDuration = 3f;

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
			return null;
		}
	}

}
