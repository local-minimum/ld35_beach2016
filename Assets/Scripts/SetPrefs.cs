using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrefs : MonoBehaviour {
	[SerializeField] string prefKey;

	[SerializeField] string[] names;

	[SerializeField] float[] values;

	Text txt;
	int index = 0;

	string CurrentName {
		get {
			if (!PlayerPrefs.HasKey (prefKey))
				PlayerPrefs.SetFloat (prefKey, values [0]);
			
			float val = PlayerPrefs.GetFloat(prefKey);
			for (int i = 0; i < names.Length; i++) {
				if (val == values [i]) {
					index = i;
					return names [i];
				}
			}
			index = 0;
			return names [0];
		}
	}

	void Start() {
		txt = GetComponentInChildren<Text> ();
		txt.text = CurrentName;
	}

	public void Click() {
		index++;
		index %= names.Length;
		txt.text = names [index];
		PlayerPrefs.SetFloat (prefKey, values [index]);
	}
}
