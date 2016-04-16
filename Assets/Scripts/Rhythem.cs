using UnityEngine;
using System.Collections;

public delegate void TrackEvent(AudioClip clip);

public class Rhythem : MonoBehaviour {

	public event TrackEvent OnTrackEvent;
	[SerializeField] int[] Track;

	[SerializeField] public int scaleFactor = 1;

	Exercise exercise;

	int beat;

	public int Instrument {
		get {
			return Track [beat];
		}
	}

	void Awake() {
		exercise = GetComponentInParent<Exercise> ();

	}

	void OnEnable() {
		exercise.OnBeat += Exercise_OnBeat;
	}

	void OnDisable() {
		exercise.OnBeat -= Exercise_OnBeat;
	}

	void Exercise_OnBeat ()
	{
		beat = (beat + 1) % Track.Length;
		int instrument = Instrument;

	}
	
}
