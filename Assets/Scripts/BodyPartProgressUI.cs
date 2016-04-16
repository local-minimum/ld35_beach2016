using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyPartProgressUI : MonoBehaviour {

	[SerializeField] Image[] progressImages;

	[SerializeField] Image idealIcon;

	[SerializeField] Sprite[] selectedIdeals;

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
			idealIcon.sprite = selectedIdeals [part];
			var curLvl = body.CurrentLevel (part);
	
			for (int i = 0; i < progressImages.Length; i++)
				progressImages [i].enabled = i < curLvl;
			panelImage.color = showingColor;
		}
	}
}
