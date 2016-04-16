using UnityEngine;
using System.Collections.Generic;

public delegate void BodyPartSetEvent(Body body);

public class Body : MonoBehaviour {

	[SerializeField] Exercise exercise;

	public static event BodyPartSetEvent OnBodyPartSetEvent;

	int[] exerciseLevels = new int[5] {0, 0, 0, 0, 0};
	int[] pathSelections = new int[5] {0, 1, 2, 3, 4};

	[HideInInspector] public List<int> partsInCurrentSet = new List<int>();

	int partsInSet = 2;

	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
	}

	void OnDisalbe() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
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
		partsInCurrentSet.Clear ();
		if (OnBodyPartSetEvent != null)
			OnBodyPartSetEvent (this);

	}

	public void Play() {

		exercise.Play ();
	}

	public bool EnoughParts {
		get {
			return partsInCurrentSet.Count == partsInSet;
		}
	}

	public bool AnyParts {
		get {
			return partsInCurrentSet.Count > 0;
		}
	}
}
