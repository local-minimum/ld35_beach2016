using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class AutoTagger : MonoBehaviour {

	public string[] tags;

	public string[] alwaysTag;

	public int totalTags = 7;

	void Start () {
		var txt = GetComponent<Text> ();
		bool[] taggedAlways = new bool[alwaysTag.Length];
		bool[] otherTags = new bool[tags.Length];

		while (totalTags > 0) {
			var needed = taggedAlways.Sum (v => v ? 0 : 1); 
			string tag = "";
			if (Random.value < needed / (float)totalTags) {
				int pos = Random.Range (0, needed);

				for (int i = 0; i < alwaysTag.Length; i++) {
					if (!taggedAlways [i]) {
						pos--;
						if (pos < 0) {
							taggedAlways [i] = true;
							tag = alwaysTag [i];
							break;
						}
					}
				}
			} else {
				var untagged = otherTags.Sum (v => v ? 0 : 1);
				var pos = Random.Range (0, untagged);
				for (int i = 0; i < tags.Length; i++) {
					if (!otherTags [i]) {
						pos--;
						if (pos < 0) {
							otherTags [i] = true;
							tag = tags [i];
							break;
						}
					}
				}
			}

			if (tag != "") {
				txt.text += tag + " ";
				totalTags--;
			} else {
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
