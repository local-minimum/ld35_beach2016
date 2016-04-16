using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyPartProgressUI : MonoBehaviour {

	[SerializeField] Image[] progressImages;

	[SerializeField] Image idealIcon;
	[SerializeField] Image bodyPartIcon;

	[SerializeField] Sprite[] bodyPartIcons;

	[SerializeField] Sprite[] idealIcons;

	[SerializeField] Body body;

	[SerializeField] Color32 showingColor;

	[SerializeField] Color32 hiddenColor;

	[SerializeField] Image panelImage;


	void OnEnable() {
		SetBodyPartSelector.OnBodyPartSelection += SetBodyPartSelector_OnBodyPartSelection;
	}

	void OnDisalbe() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
	}

	void SetBodyPartSelector_OnBodyPartSelection (int part, EventType eventType)
	{
		if (!body.HasBeenExercised (part)) {
			panelImage.color = hiddenColor;
		} else {
			bodyPartIcon.sprite = bodyPartIcons [part];
			idealIcon.sprite = idealIcons [body.GetPathSelected(part)];

			var curLvl = body.CurrentLevel (part);
	
			for (int i = 0; i < progressImages.Length; i++)
				progressImages [i].enabled = i < curLvl;
			panelImage.color = showingColor;
		}
	}
}
