using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class PhotoLoader : MonoBehaviour {

	public static Texture2D photo;

	[SerializeField] StartButton btn;

	// Use this for initialization
	void Start () {
		var img = GetComponent<Image> ();

		if (photo) {
			img.sprite = Sprite.Create(photo, new Rect(0, 0, photo.width, photo.height), Vector2.one * 0.5f);
		} else {
			btn.Click ();
		}
	}
	
}
