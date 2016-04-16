using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetBodyPartPath : MonoBehaviour {

	[SerializeField] Body body;

	int selectedBodyPart = -1;

	[SerializeField] Color32 activeColor;

	[SerializeField] Color32 hiddenColor;
	[SerializeField] Toggle[] toggles;

	[SerializeField] Image img;
	bool settingUp = false;

	public void SetPath(int path) {
		if (selectedBodyPart >= 0 && !settingUp) {
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
			int path = body.GetPathSelected (part) - 1;
			if (path < 0)
				path = Random.Range (0, toggles.Length);			
			settingUp = true;
			toggles [path].isOn = true;
			img.color = activeColor;
			settingUp = false;
		}
	}
}
