using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetBodyPartPath : MonoBehaviour {

	[SerializeField] Body body;

	int selectedBodyPart = -1;

	[SerializeField] Color32 activeColor;

	[SerializeField] Color32 hiddenColor;

	[SerializeField] Image img;

	public void SetPath(int path) {
		if (selectedBodyPart >= 0) {
			body.SetPath (selectedBodyPart, path);
			selectedBodyPart = -1;
			StartCoroutine (DelayHidden ());
		}
	}

	IEnumerator<WaitForSeconds> DelayHidden() {
		yield return new WaitForSeconds (0.5f);
		if (selectedBodyPart < 0)
			img.color = hiddenColor;
	}

	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
	}

	void OnDisable() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
	}

	void SetBodyPartSelector_OnBodyPartSelection (int part, EventType eventType)
	{
		if (eventType == EventType.Select && !body.HasBeenExercised(part)) {
			selectedBodyPart = part;
			img.color = activeColor;
		}
	}
}
