using UnityEngine;
using System.Collections;

public class PuppeteerPulser : MonoBehaviour {

	[SerializeField] Rhythem rythm;

	[SerializeField] Vector3 localScalePulse;

	float position = 0;

	[SerializeField] float decay = 3f;

	[SerializeField, Range(0, 1)] float hitPower = 0.7f;


	void OnEnable() {
		RhythemIcon.OnHit += RhythemIcon_OnHit;
	}

	void OnDisable() {
		RhythemIcon.OnHit -= RhythemIcon_OnHit;
	}

	void RhythemIcon_OnHit (Rhythem rhythem, RhythemIcon icon)
	{
		if (rythm == rhythem) {
			position = Mathf.Clamp01 (position + hitPower);
		}
	}

	void Update() {
		transform.localScale = Vector3.Lerp (Vector3.one, localScalePulse, position);
		position = Mathf.Clamp01 (position - decay * Time.deltaTime);
	}
}
