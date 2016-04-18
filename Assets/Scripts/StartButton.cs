using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	[SerializeField] string sceneName;

	public void Click () {
		SceneManager.LoadScene (sceneName);
	}

	public void ShareTwitter() {
		string base_url = "http://twitter.com/intent/tweet";
		string lang = "en";
		string text = "@Local_Minimum I'm ready for Beach 2016 #fitness #pumpin #LDJAM";

		Application.OpenURL (base_url + "?text=" + WWW.EscapeURL (text) +
			"&amp;lang=" + WWW.EscapeURL (lang));

	}

}
