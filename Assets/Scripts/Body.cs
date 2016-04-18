using UnityEngine;
using System.Collections.Generic;

public delegate void BodyPartSetEvent(Body body);
public delegate void RepsEvent(Body body, int bodyPart, float progress);

public class Body : MonoBehaviour {

	[SerializeField] float nextSetSelectionDelay = 3f;

	public Exercise exercise;
	[SerializeField] int setLength = 15;

	public static event RepsEvent OnReps;
	public static event BodyPartSetEvent OnBodyPartSetEvent;

	[SerializeField] int[] exerciseMaxLevels = new int[5] {5, 5, 5, 5, 5};
	[SerializeField] int[] exerciseLevels = new int[5] {0, 0, 0, 0, 0};
	[SerializeField] int[] pathSelections = new int[5] {0, 0, 0, 0, 0};
	int[] repsRemaining = new int[5] {0, 0, 0, 0, 0};
	[SerializeField] int[] partsInSets = new int[] {1, 2, 3, 3, 4, 4, 5};
	int gameModeSets;
	[SerializeField] int[] setLengthInc = new int[] {0, 1, 1, 1, 2};

	[HideInInspector] public List<int> partsInCurrentSet = new List<int>();

	[SerializeField] AudioClip winSet;
	[SerializeField] AudioClip outroMusic;
	[SerializeField] AudioSource speaker;

	[SerializeField] Animator beachAnim;

	int _currentSet = 0;
	public bool isExecising = false;
	public bool isPlaying = true;

	public int partsInSet {
		get {
			int partsWithRemaining = 0;
			for (int i = 0; i < exerciseLevels.Length; i++) {
				if (exerciseLevels [i] < exerciseMaxLevels [pathSelections[i]])
					partsWithRemaining++;
			}
			Debug.Log ("Remaing " + partsWithRemaining);
			return Mathf.Min (partsWithRemaining, Mathf.Max(gameModeSets, partsInSets[Mathf.Min(_currentSet, partsInSets.Length - 1)]));
		}
	}

	void Awake() {
		gameModeSets = Mathf.FloorToInt (PlayerPrefs.GetFloat ("Game.StartSets", 1f));
	}

	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
		RhythemIcon.OnHit += HandleWorkout;
	}

	void OnDisalbe() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
		RhythemIcon.OnHit -= HandleWorkout;
	}

	void HandleWorkout (Rhythem rhythem, RhythemIcon icon, int beatValue)
	{
		int bodyPart = exercise.GetBodyPartIndex (rhythem);
		if (isExecising && !exercise.Channels [bodyPart].autoWorkout) {
			Debug.Log ("Hit the beat");
			repsRemaining [bodyPart] = Mathf.Max (0, repsRemaining [bodyPart] - beatValue);

			if (repsRemaining [bodyPart] == 0) {
				speaker.PlayOneShot (winSet, 0.3f);
				exercise.Channels [bodyPart].autoPlay = true;
				exercise.Channels [bodyPart].autoWorkout = true;
				exerciseLevels [bodyPart]++;
				//exercise.Tracks [bodyPart].beating = false;
			}
			
			if (OnReps != null)
				OnReps (this, bodyPart, 1 - (float)repsRemaining [bodyPart] / setLength);
		

			if (AllRepsDone) {
				StartCoroutine (DoCompleteSet ());
			}

		}
	}

	IEnumerator<WaitForSeconds> DoCompleteSet() {
		isExecising = false;

		yield return new WaitForSeconds (nextSetSelectionDelay);
		setLength += setLengthInc [Mathf.Min (_currentSet, setLengthInc.Length)];
		_currentSet++;
		if (HasMoreDepth) {			
			ClearBodyParts ();
		} else {
			GameOver ();
		}

	}

	bool AllRepsDone {
		get {
			for (int i = 0; i < repsRemaining.Length; i++) {
				if (repsRemaining [i] > 0)
					return false;
			}
			return true;
		}
	}

	bool HasMoreDepth {
		get {
			for (int i = 0; i < exerciseMaxLevels.Length; i++) {
				if (exerciseLevels [i] < exerciseMaxLevels [exerciseMaxLevels [pathSelections[i]]])
					return true;
			}
			return false;
		}
	}

	void SetBodyPartSelector_OnBodyPartSelection (int part, EventType eventType)
	{
		if (eventType == EventType.Select && !partsInCurrentSet.Contains (part) && exerciseLevels [part] > 0) {
			partsInCurrentSet.Add (part);
			if (OnBodyPartSetEvent != null)
				OnBodyPartSetEvent (this);
		}
	}

	public bool HasBeenExercised(int bodyPartIndex) {
		return exerciseLevels [bodyPartIndex] > 0;
	}

	public int CurrentLevel(int bodyPartIndex) {
		return exerciseLevels [bodyPartIndex];
	}

	public int GetPathSelected(int bodyPartIndex) {
		return pathSelections [bodyPartIndex];
	}

	public void SetPath(int bodyPartIndex, int path) {
		pathSelections [bodyPartIndex] = path;
		if (!partsInCurrentSet.Contains (bodyPartIndex)) {			
			partsInCurrentSet.Add (bodyPartIndex);
			if (OnBodyPartSetEvent != null)
				OnBodyPartSetEvent (this);
		}
	}

	public int UndoSetSelection() {
		if (partsInCurrentSet.Count == 0)
			return -1;
		var part = partsInCurrentSet [partsInCurrentSet.Count - 1];
		partsInCurrentSet.Remove (part);
		if (OnBodyPartSetEvent != null)
			OnBodyPartSetEvent (this);
		return part;
	}

	public void ClearBodyParts() {
		Debug.Log ("Clear");
		partsInCurrentSet.Clear ();
		isExecising = false;
		if (OnBodyPartSetEvent != null)
			OnBodyPartSetEvent (this);

	}

	public void Play() {
		Debug.Log ("Play");
		foreach (int part in partsInCurrentSet) {
			repsRemaining [part] = setLength;
		}
		isExecising = true;
		exercise.Play (this);
	}

	public bool EnoughParts {
		get {
			Debug.Log ("Parts in set " + partsInSet);
			Debug.Log ("Parts in current " + partsInCurrentSet.Count);
			return partsInCurrentSet.Count == partsInSet;
		}
	}

	public bool AnyParts {
		get {
			return partsInCurrentSet.Count > 0;
		}
	}

	public void GameOver() {
		if (!isPlaying)
			return;
		
		isPlaying = false;
		foreach (var c in exercise.Channels) {
			c.autoPlay = true;
			c.autoWorkout = true;
			c.speaker.mute = true;
		}

		beachAnim.SetTrigger ("Beach");

		speaker.clip = outroMusic;
		speaker.loop = true;
		speaker.Play();
		/*
		foreach (var t in exercise.Tracks) {
			t.beating = true;
		}*/


		Debug.Log ("Nothing more to improve");
		//TODO: DO beach!
	}
}
