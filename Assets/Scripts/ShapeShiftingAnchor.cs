using UnityEngine;
using System.Collections;

public class ShapeShiftingAnchor : MonoBehaviour {

	public Transform anchored;

	public AnimationCurve easing;

	[Range(0.0001f, 3f)] public float duration = 1f;

	public int currentShape;

	public Vector3[] anchorLocalPositions;

	public ShapeShiftingPart controller;

	float shiftTime;

	Vector3 shiftOrigin;

	Vector3 shiftVector;

	bool shifting = false;

	void OnEnable() {
		controller.OnShiftEvent += HandleNewShape;
	}

	void OnDisable() {
		controller.OnShiftEvent -= HandleNewShape;
	}

	public void HandleNewShape (int shape)
	{
		currentShape = shape;
		transform.localPosition = anchorLocalPositions [currentShape];
		shiftOrigin = anchored.position;
		shiftVector = transform.position - shiftOrigin;
		shiftTime = Time.timeSinceLevelLoad;
		shifting = true;
	}

	void Update() {
		if (shifting) {
			float progress = Mathf.Clamp01 ((Time.timeSinceLevelLoad - shiftTime) / duration);
			if (progress == 1) {
				anchored.position = transform.position;
				shifting = false;
			} else {
				anchored.position = easing.Evaluate(progress) * shiftVector + shiftOrigin;
			}
		}
	}

	#if UNITY_EDITOR
	public void RecordPosition() {
		if (currentShape >= anchorLocalPositions.Length) {
			Vector3[] newArr = new Vector3[currentShape + 1];
			System.Array.Copy (anchorLocalPositions, newArr, anchorLocalPositions.Length);

		}
		anchorLocalPositions [currentShape] = transform.localPosition;

	}
	#endif
}
