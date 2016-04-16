using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SlotButton : MonoBehaviour {

	[SerializeField] KeyCode button;
	Image img;

	Text textField;

	[SerializeField] Color32 inactiveColor;

	[SerializeField] Color32 activeColor;

	[SerializeField] Color32 pressedColor;

	[SerializeField] Rhythem track;

	[SerializeField, Range(0, 5)] float colorAttack = 0.3f;

	void Start () {
		img = GetComponent<Image> ();
		textField = GetComponentInChildren<Text> ();
		textField.text = button.ToString ();
	}
	
	void Update () {
		if (track.enabled) {
			textField.color = activeColor;
			if (Input.GetKeyDown (button)) {
				img.color = pressedColor;
			} else {
				img.color = Color32.Lerp (img.color, activeColor, Time.deltaTime * colorAttack);
			}
		} else {
			img.color = inactiveColor;
			textField.color = inactiveColor;
		}
	}
}
