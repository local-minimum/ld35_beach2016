using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EventType {Hover, Select, Clear};

public delegate void BodyPartSelectorEvent(int part, EventType eventType);

public class SetBodyPartSelector : MonoBehaviour {

	public static event BodyPartSelectorEvent OnBodyPartSelection;

	[SerializeField] Button btn;
	[SerializeField] int bodyPartIndex;

	bool allowClick = true;

	void Reset () {
		btn = GetComponent<Button> ();
	}
	
	public void Click () {
		if (!allowClick)
			return;
		
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

	void OnEnable() {
		SetSelectionUI.OnAllowInteract += HandeAllowInteract;
		Body.OnBodyPartSetEvent += Body_OnBodyPartSetEvent;
	}
		
	void OnDisable() {
		SetSelectionUI.OnAllowInteract -= HandeAllowInteract;
		Body.OnBodyPartSetEvent -= Body_OnBodyPartSetEvent;
	}

	void Body_OnBodyPartSetEvent (Body body)
	{	
		btn.interactable = !body.partsInCurrentSet.Contains (bodyPartIndex);
		allowClick = !body.EnoughParts;
	}

	void HandeAllowInteract (int part)
	{
		if (part == bodyPartIndex) {
			btn.interactable = true;
		}
	}
}
