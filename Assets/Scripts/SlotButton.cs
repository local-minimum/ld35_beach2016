using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void PressEvent();

public class SlotButton : MonoBehaviour {

	public event PressEvent OnPressEvent;

	[SerializeField] KeyCode button;
	Image img;

	Text textField;

	[SerializeField] Color32 inactiveColor;

	[SerializeField] Color32 activeColor;

	[SerializeField] Color32 pressedColor;

	[SerializeField] Rhythem track;

	[SerializeField] RhythemChannel channel;

	[SerializeField, Range(0, 5)] float colorAttack = 0.3f;

	void Start () {
		img = GetComponent<Image> ();
		textField = GetComponentInChildren<Text> ();
		textField.text = button.ToString ();
	}
	
	void Update () {
		
		if (track.enabled && !channel.autoPlay) {
			textField.color = activeColor;
			if (Input.GetKeyDown (button)) {
				img.color = pressedColor;
				if (OnPressEvent != null)
					OnPressEvent ();
			} else {
				img.color = Color32.Lerp (img.color, activeColor, Time.deltaTime * colorAttack);
			}
		} else {
			img.color = inactiveColor;
			textField.color = inactiveColor;
		}
	}
}
