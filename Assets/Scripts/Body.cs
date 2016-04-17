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

	int[] exerciseMaxLevels = new int[5] {5, 5, 5, 5, 5};
	[SerializeField] int[] exerciseLevels = new int[5] {0, 0, 0, 0, 0};
	[SerializeField] int[] pathSelections = new int[5] {0, 0, 0, 0, 0};
	int[] repsRemaining = new int[5] {0, 0, 0, 0, 0};
	[SerializeField] int[] partsInSets = new int[] {1, 2, 3, 3, 4, 4, 5};
	[SerializeField] int[] setLengthInc = new int[] {0, 1, 1, 1, 2};

	[HideInInspector] public List<int> partsInCurrentSet = new List<int>();

	int _currentSet = 0;
	public bool isExecising = false;

	int partsInSet {
		get {
			int partsWithRemaining = 0;
			for (int i = 0; i < exerciseLevels.Length; i++) {
				if (exerciseLevels [i] < exerciseMaxLevels [i])
					partsWithRemaining++;
			}
			Debug.Log ("Remaing " + partsWithRemaining);
			return Mathf.Min (partsWithRemaining, partsInSets[Mathf.Min(_currentSet, partsInSets.Length - 1)]);
		}
	}

	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
		RhythemIcon.OnHit += HandleWorkout;
	}

	void OnDisalbe() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
		RhythemIcon.OnHit -= HandleWorkout;
	}

	void HandleWorkout (Rhythem rhythem, RhythemIcon icon)
	{
		int bodyPart = exercise.GetBodyPartIndex (rhythem);
		if (isExecising && !exercise.Channels [bodyPart].autoPlay) {
			Debug.Log ("Hit the beat");
			repsRemaining [bodyPart] = Mathf.Max (0, repsRemaining [bodyPart] - 1);

			if (repsRemaining [bodyPart] == 0) {
				exercise.Channels [bodyPart].autoPlay = true;
				exerciseLevels [bodyPart]++;
				exercise.Tracks [bodyPart].gameObject.SetActive (false);
			}
			
			if (OnReps != null)
				OnReps (this, bodyPart, 1 - (float)repsRemaining [bodyPart] / setLength);
		

			if (AllRepsDone) {
				StartCoroutine (DoCompleteSet ());
			}

		}
	}

	IEnumerator<WaitForSeconds> DoCompleteSet() {
		yield return new WaitForSeconds (nextSetSelectionDelay);
		Debug.Log ("Wait done");
		setLength += setLengthInc [Mathf.Min (_currentSet, setLengthInc.Length)];
		_currentSet++;
		if (HasMoreDepth) {
			
			ClearBodyParts ();
		} else {
			//TODO: DO beach!
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
				if (exerciseLevels [i] < exerciseMaxLevels [i])
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
		exercise.Play ();
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
}
