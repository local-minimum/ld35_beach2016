using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EventType {Hover, Select, Clear};

public delegate void BodyPartSelectorEvent(int part, EventType eventType);

public class SetBodyPartSelector : MonoBehaviour {

	public static event BodyPartSelectorEvent OnBodyPartSelection;

	[SerializeField] Button btn;
	[SerializeField] int bodyPartIndex;

	void Reset () {
		btn = GetComponent<Button> ();
	}
	
	public void Click () {
		btn.interactable = false;
		if (OnBodyPartSelection != null)
			OnBodyPartSelection (bodyPartIndex, EventType.Select);
	}

	public void Hover() {
		if (OnBodyPartSelection != null)
			OnBodyPartSelection (bodyPartIndex, EventType.Hover);
	}

	public void UnHover() {
		if (OnBodyPartSelection != null)
			OnBodyPartSelection (bodyPartIndex, EventType.Clear);
	}
}
