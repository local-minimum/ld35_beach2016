using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CamButton : MonoBehaviour {

	[SerializeField, Range(0, 1f)] float width;
	[SerializeField, Range(0, 1f)] float height;
	[SerializeField] Body body;
	Button btn;
	Image img; 
	[SerializeField] Canvas hideCanvas;

	void Start() {
		btn = GetComponent<Button> ();
		btn.interactable = false;
		btn.enabled = false;
		img = GetComponentInChildren<Image> ();
		img.enabled = false;
	}
	void Update() {
		if (!body.isPlaying && !btn.enabled) {
			img.enabled = true;
			btn.enabled = true;
			btn.interactable = true;
		}
	}

	public void Snap() {
		btn.interactable = false;
		StartCoroutine (DoSnap ());
	}

	IEnumerator DoSnap() {
		hideCanvas.enabled = false;
		img.enabled = false;
		yield return new WaitForEndOfFrame ();
		int width = Mathf.FloorToInt(Screen.width * this.width);
		int height = Mathf.FloorToInt (Screen.height * this.height);

		Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		tex.ReadPixels (new Rect ((Screen.width - width) / 2, (Screen.height - height) / 2, width, height), 0, 0);
		tex.Apply ();
		var png = tex.EncodeToPNG ();
		int count = PlayerPrefs.GetInt("Beach.Photos", 0);
		count++;
		count %= 10;

		#if UNITY_WEBGL
		#else
			File.WriteAllBytes (Application.dataPath + "/Sprites/Photos/beach_photo_" + count + ".png", png);
		#endif

		PlayerPrefs.SetInt ("Beach.Photos", count);
		PhotoLoader.photo = tex;
		SceneManager.LoadScene ("PhotoAlbum");
	}
}
