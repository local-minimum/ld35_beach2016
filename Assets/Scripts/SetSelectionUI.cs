﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void AllowInteract(int part, Body body);

public class SetSelectionUI : MonoBehaviour {

	static public event AllowInteract OnAllowInteract;

	[SerializeField] Color32 activeColor;
	[SerializeField] Color32 hiddenColor;
	[SerializeField] Image panelImage;

	[SerializeField] Body body;

	[SerializeField] GameObject undoButton;
	[SerializeField] GameObject completeButton;
	[SerializeField] Text remaining;

	public void HideContents() {
		panelImage.color = hiddenColor;
		body.Play ();
	}
		
	public void UndoSelect() {
		var bodyPart = body.UndoSetSelection ();
		if (bodyPart >= 0) {
			if (OnAllowInteract != null)
				OnAllowInteract (bodyPart, body);
		}
	}

	void OnEnable() {		
		Body.OnBodyPartSetEvent += Body_OnBodyPartSetEvent;
	}

	void OnDisable() {
		Body.OnBodyPartSetEvent -= Body_OnBodyPartSetEvent;
	}

	public void Start() {
		Body_OnBodyPartSetEvent (body);
		body.ClearBodyParts ();
	}

	void Body_OnBodyPartSetEvent (Body body)
	{
		undoButton.SetActive (body.AnyParts);
		completeButton.SetActive (body.EnoughParts);
		panelImage.color = activeColor;
		remaining.text = body.EnoughParts ? "Pump 'em!" : "Select: " + (body.partsInSet - body.partsInCurrentSet.Count);
	}

	void Update() {
		if (!body.isPlaying) {
			panelImage.color = hiddenColor;
		}
	}
}
