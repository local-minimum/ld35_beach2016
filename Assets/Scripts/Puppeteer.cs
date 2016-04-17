using UnityEngine;
using System.Collections;

public class Puppeteer : MonoBehaviour {

	[SerializeField] Rhythem rhythem;

	[SerializeField, Range(0, 1)] float hitPower = 0.7f;

	[SerializeField] Transform[] path;

	[SerializeField] Transform stringAnchor;

	float position = 0;

	[SerializeField] float decay = 3f;

	void Update () {
		position = Mathf.Clamp01 (position - decay * Time.deltaTime);		
		stringAnchor.position = anchorPos;

	}

	Vector3 anchorPos {
		get {
			float scaled = position * (path.Length - 1);
			int idx = Mathf.FloorToInt(scaled);
			if (idx == path.Length)
				idx--;
			return Vector3.Lerp (path [idx].position, path [idx + 1].position, scaled - idx);
		}
	}

	void OnEnable() {
		RhythemIcon.OnHit += RhythemIcon_OnHit;
	}

	void OnDisable() {
		RhythemIcon.OnHit -= RhythemIcon_OnHit;
	}

	void RhythemIcon_OnHit (Rhythem rhythem, RhythemIcon icon, int beatValue)
	{
		if (rhythem == this.rhythem) {
			position = Mathf.Clamp01 (position + hitPower);
		}
	}
}
