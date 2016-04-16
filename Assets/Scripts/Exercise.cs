using UnityEngine;
using System.Collections.Generic;

public delegate void BeatEvent();

public class Exercise : MonoBehaviour {

	public event BeatEvent OnBeat;

	public Rhythem[] Tracks;
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
}
