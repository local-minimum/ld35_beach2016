using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CamButton : MonoBehaviour {

	[SerializeField] int width;
	[SerializeField] int height;

	public void Snap() {
		var btn = GetComponent<Button> ();
		btn.interactable = false;
		StartCoroutine (DoSnap ());
	}

	IEnumerator DoSnap() {
		yield return new WaitForEndOfFrame ();
		Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		tex.ReadPixels (new Rect ((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), 0, 0);
		tex.Apply ();
		var png = tex.EncodeToPNG ();
		int count = PlayerPrefs.GetInt("Beach.Photos", 0);
		count %= 10;
		File.WriteAllBytes (Application.dataPath + "/Sprites/Photos/beach_photo_" + count + ".png", png);
		count++;
		PlayerPrefs.SetInt ("Beach.Photos", count);
		DestroyObject (tex);
		SceneManager.LoadScene ("Menu");
	}
}
