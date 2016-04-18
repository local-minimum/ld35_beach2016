using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyPartProgressUI : MonoBehaviour {

	[SerializeField] Image[] progressImages;

	[SerializeField] Image idealIcon;
	[SerializeField] Image bodyPartIcon;

	[SerializeField] Sprite[] bodyPartIcons;
	[SerializeField] int[] xScales;
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

	void OnDestroy() {
		SetBodyPartSelector.OnBodyPartSelection -= SetBodyPartSelector_OnBodyPartSelection;
	}

	void SetBodyPartSelector_OnBodyPartSelection (int part, EventType eventType)
	{
		if (!body.HasBeenExercised (part)) {
			panelImage.color = hiddenColor;
		} else {
			bodyPartIcon.sprite = bodyPartIcons [part];
			var scale = Vector3.one;
			scale.x = xScales [part];
			bodyPartIcon.transform.localScale = scale;
			idealIcon.sprite = idealIcons [body.GetPathSelected(part) - 1];

			var curLvl = body.CurrentLevel (part);
	
			for (int i = 0; i < progressImages.Length; i++)
				progressImages [i].enabled = i < curLvl;
			panelImage.color = showingColor;
		}
	}
}
