using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	int[] exerciseLevels = new int[5] {0, 0, 0, 0, 0};

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
}
