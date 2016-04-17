using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour {

	[SerializeField] Image img;

	[SerializeField, Range(0, 1)] float selectingSetFactor = .1f;

	float progress = 1;

	[SerializeField, Range(10, 60 * 5)] float duration = 60f;

	[SerializeField] Body body;

	void Update () {
		progress -= body.isExecising ? Time.deltaTime / duration : Time.deltaTime / duration * selectingSetFactor;
		progress = Mathf.Max (0, progress);
		img.fillAmount = progress;
		if (progress == 0) {
			body.GameOver ();
		}
	}
}
