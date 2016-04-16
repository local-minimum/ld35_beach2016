using UnityEngine;
using System.Collections;

public delegate void ShiftEvent(int shape);

public class ShapeShiftingPart : MonoBehaviour {

	public event ShiftEvent OnShiftEvent;

	public int[] startShapes = new int[] {0};

	public Sprite[] shapeImages;

	public SpriteRenderer rend;

	#if UNITY_EDITOR
	public int editShape = 0;

	public float shiftChance = 2;
	#endif

	void Reset() {
		rend = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		Shift (startShapes [Random.Range (0, startShapes.Length)]);
	}

	void Shift(int newShape) {
		rend.sprite = shapeImages [newShape];
		if (OnShiftEvent != null)
			OnShiftEvent (newShape);
	}

	public void RandomShift() {
		Shift (Random.Range (0, shapeImages.Length));
	}

	void Update() {
		if (Random.value < Time.deltaTime * shiftChance)
			RandomShift ();
	}

	#if UNITY_EDITOR
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
