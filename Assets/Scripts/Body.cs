using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	int[] exerciseLevels = new int[5] {0, 0, 0, 0, 0};
	int[] pathSelections = new int[5] {0, 1, 2, 3, 4};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
	}
}
