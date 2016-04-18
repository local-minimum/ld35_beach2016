using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPhotoAlbum : MonoBehaviour {

	[SerializeField] Button btn;

	void Start () {
		btn.interactable = PhotoLoader.photo != null;
	}
	
	public void Click () {
		SceneManager.LoadScene ("PhotoAlbum");
	}
}
