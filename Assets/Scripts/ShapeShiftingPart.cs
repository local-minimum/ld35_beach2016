using UnityEngine;
using System.Collections;

public delegate void ShiftEvent(int shape);

public class ShapeShiftingPart : MonoBehaviour {

	public int bodyPartIndex;

	public event ShiftEvent OnShiftEvent;

	public int startShapes = 1;

	public Sprite[] shapeImages;

	public SpriteRenderer rend;

	#if UNITY_EDITOR
	public int editShape = 0;

	public float shiftChance = 2;

	void Update() {
		if (Random.value < Time.deltaTime * shiftChance)
			RandomShift ();
	}
	#endif

	void Reset() {
		rend = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		Shift (Random.Range (0, startShapes));
	}

	void Shift(int newShape) {
		rend.sprite = shapeImages [newShape];
		if (OnShiftEvent != null)
			OnShiftEvent (newShape);
	}

	public void RandomShift() {
		Shift (Random.Range (0, startShapes));
	}

	void OnEnable() {
		Body.OnReps += Body_OnReps;
	}

	void OnDisable() {
		Body.OnReps -= Body_OnReps;
	}

	void Body_OnReps (Body body, int bodyPart, float progress)
	{
		if (bodyPart == bodyPartIndex && progress == 1) {
			var path = body.GetPathSelected (bodyPartIndex);
			var lvl = body.CurrentLevel (bodyPartIndex);
			Debug.Log ("start " + startShapes + " lvl " + lvl + " path " + path + " : " + startShapes + (lvl - 1) * 4 + (path - 1));
			Shift (startShapes + (lvl - 1) * 4 + (path - 1));
		}
	}

	#if UNITY_EDITOR

	public void SetFromLocal() {
		rend = GetComponent<SpriteRenderer> ();
		shapeImages [editShape] = rend.sprite;
	}

	public void SetCurrent() {
		Shift (editShape);
		foreach (ShapeShiftingAnchor anchor in FindObjectsOfType<ShapeShiftingAnchor>()) {
			if (anchor.controller == this) {
				anchor.HandleNewShape (editShape);
			}
		}
	}

	public void RecordAnchors() {

		foreach (ShapeShiftingAnchor anchor in FindObjectsOfType<ShapeShiftingAnchor>()) {
			if (anchor.controller == this) {
				anchor.RecordPosition();
			}
		}

	}
	#endif
}
