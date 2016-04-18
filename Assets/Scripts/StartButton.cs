using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	[SerializeField] string sceneName;

	public void Click () {
		SceneManager.LoadScene (sceneName);
	}
}
