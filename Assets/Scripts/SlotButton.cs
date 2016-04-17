using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void PressEvent(ParticleSystem ps);

public class SlotButton : MonoBehaviour {

	public event PressEvent OnPressEvent;

	[SerializeField] KeyCode button;
	Image img;

	[SerializeField] Image progressImage;

	Text textField;

	[SerializeField] Color32 inactiveColor;

	[SerializeField] Color32 activeColor;

	[SerializeField] Color32 pressedColor;

	[SerializeField] Rhythem track;

	[SerializeField] RhythemChannel channel;

	[SerializeField, Range(0, 5)] float colorAttack = 0.3f;

	ParticleSystem ps;

	void Start () {
		ps = GetComponent<ParticleSystem> ();
		img = GetComponent<Image> ();
		textField = GetComponentInChildren<Text> ();
		var txt = button.ToString ();
		if (txt.StartsWith ("Alpha"))
			txt = txt.Substring (5);
		textField.text = txt;
	}
	
	void Update () {
		
		if (track.beating && !channel.autoPlay) {
			textField.color = activeColor;
			if (Input.GetKeyDown (button)) {
				img.color = pressedColor;
				if (OnPressEvent != null)
					OnPressEvent (ps);
			} else {
				img.color = Color32.Lerp (img.color, activeColor, Time.deltaTime * colorAttack);
			}
		} else {
			img.color = inactiveColor;
			textField.color = inactiveColor;
		}
	}

	void OnEnable() {
		Body.OnReps += Body_OnReps;
		Body.OnBodyPartSetEvent += Body_OnBodyPartSetEvent;
	}

	void OnDisable() {
		Body.OnReps -= Body_OnReps;
		Body.OnBodyPartSetEvent -= Body_OnBodyPartSetEvent;
	}

	void Body_OnBodyPartSetEvent (Body body)
	{
		if (!body.isExecising)
			progressImage.fillAmount = 0;
	}

	void Body_OnReps (Body body, int bodyPart, float progress)
	{
		if (body.exercise.GetBodyPartIndex (track) == bodyPart) {
			progressImage.fillAmount = progress;
		}
	}
}
