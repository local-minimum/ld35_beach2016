using UnityEngine;
using UnityEngine.UI;

public class RhythmSlotsUI : MonoBehaviour {

	[SerializeField] Body body;
	[SerializeField] Color32 activeColor;
	[SerializeField] Color32 hiddenColor;
	[SerializeField] Image img;

	void Update () {
		img.color = body.isExecising && body.isPlaying ? activeColor : hiddenColor;
	}
}
