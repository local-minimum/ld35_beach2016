using UnityEngine;
using System.Collections.Generic;

public delegate void BeatEvent();

public class Exercise : MonoBehaviour {

	public event BeatEvent OnBeat;

	public Rhythem[] Tracks;
	public RhythemChannel[] Channels;

	public AudioClip[] Instruments;

	[SerializeField, Range(1, 8)] int notesPerBeat = 8;
	[SerializeField, Range(90, 200)] float beatsPerMinute = 100;

	void Start () {
		StartCoroutine (Beater ());
	}

	IEnumerator<WaitForSeconds> Beater() {
		while (true) {
			if (OnBeat != null)
				OnBeat ();

			yield return new WaitForSeconds (60f / (beatsPerMinute * notesPerBeat));
		}
	}

	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
		Body.OnBodyPartSetEvent += Body_OnBodyPartSetEvent;
	}
		
	void OnDisable() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
		Body.OnBodyPartSetEvent -= Body_OnBodyPartSetEvent;
	}

	void SetBodyPartSelector_OnBodyPartSelection (int part, EventType eventType)
	{
		if (eventType == EventType.Select) {
			Tracks [part].gameObject.SetActive (true);
		}
	}

	void Body_OnBodyPartSetEvent (Body body)
	{
		
		for (int i = 0; i < Tracks.Length; i++) {
			Tracks [i].gameObject.SetActive (body.partsInCurrentSet.Contains (i));
			Channels [i].autoPlay = !body.isExecising;
		}
	}

	public void Play() {
		for (int i = 0; i < Tracks.Length; i++) {
			if (Tracks [i].gameObject.activeSelf) {
				Channels [i].autoPlay = false;
			}
		}

	}

	public int GetBodyPartIndex(Rhythem rythm) {
		for (int i = 0; i < Tracks.Length; i++) {
			if (Tracks [i] == rythm)
				return i;
		}
		return -1;
	}

}
