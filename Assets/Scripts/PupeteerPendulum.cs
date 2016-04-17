using UnityEngine;
using System.Collections;

public class PupeteerPendulum : MonoBehaviour {

	[SerializeField] float pendelDuration;

	[SerializeField] Transform[] points = new Transform[2];

	[SerializeField] AnimationCurve easing;

	[SerializeField] Transform pendelum;

	float timeMoveStart = 0;


	void Update () {
		float progress = Mathf.Clamp01 ((Time.timeSinceLevelLoad - timeMoveStart) / pendelDuration);
		pendelum.position = Vector3.Lerp (points [0].position, points [1].position, easing.Evaluate (progress));
		if (progress == 1) {
			var pt = points [0];
			points [0] = points [1];
			points [1] = pt;
			timeMoveStart = Time.timeSinceLevelLoad;
		}
	}
}
