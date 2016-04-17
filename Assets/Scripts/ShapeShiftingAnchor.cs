using UnityEngine;
using System.Collections;

public class ShapeShiftingAnchor : MonoBehaviour {

	public HingeJoint2D hingeJoint;
	public SpringJoint2D springJoint;

	public AnimationCurve easing;

	[Range(0.0001f, 3f)] public float duration = 1f;

	public int currentShape;

	public Vector3[] anchorLocalPositions = new Vector3[0];

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
		shiftOrigin = transform.localPosition;
		if (currentShape < anchorLocalPositions.Length) {
			transform.localPosition = anchorLocalPositions [currentShape];
		} else if (anchorLocalPositions == null) {
			anchorLocalPositions = new Vector3[1] { transform.localPosition };
		} else {
			Vector3[] newArr = new Vector3[currentShape + 1];
			System.Array.Copy (anchorLocalPositions, newArr, anchorLocalPositions.Length);
			newArr [currentShape] = transform.localPosition;
			anchorLocalPositions = newArr;
		}

		shiftVector = transform.localPosition - shiftOrigin;
		shiftTime = Time.timeSinceLevelLoad;
		shifting = true;
	}

	void Update() {
		if (shifting) {
			float progress = Mathf.Clamp01 ((Time.timeSinceLevelLoad - shiftTime) / duration);
			if (progress == 1) {
				
				if (hingeJoint != null)
					hingeJoint.connectedAnchor = transform.localPosition;
				if (springJoint != null)
					springJoint.anchor = transform.localPosition;
				shifting = false;
			} else {				
				if (hingeJoint != null)
					hingeJoint.connectedAnchor = easing.Evaluate(progress) * shiftVector + shiftOrigin;
				if (springJoint != null)
					springJoint.anchor = easing.Evaluate(progress) * shiftVector + shiftOrigin;

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
