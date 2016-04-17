using UnityEngine;
using System.Collections;

public delegate void TrackEvent(AudioClip clip);

public class Rhythem : MonoBehaviour {

	public event TrackEvent OnTrackEvent;
	[SerializeField] int[] Track;

	[SerializeField] public int scaleFactor = 1;

	[SerializeField] public int beatValue = 1;

	Exercise exercise;

	public bool beating = true;

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
		if (Track.Length > 0) {
			beat = (beat + 1) % Track.Length;
			if (OnTrackEvent != null && beating)
				OnTrackEvent (exercise.Instruments [Instrument]);
		}
	}
	
}
