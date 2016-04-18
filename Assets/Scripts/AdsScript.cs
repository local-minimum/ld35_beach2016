using UnityEngine;
using UnityEngine.UI;

public class AdsScript : MonoBehaviour {

	Image img;

	[SerializeField, Range(0, 0.5f)] float xOffset;
	[SerializeField, Range(0, 0.5f)] float yNegOff;
	[SerializeField, Range(0, 0.5f)] float yPosOff;
	[SerializeField] float offsetF = 200f;

	void Start () {
		img = GetComponent<Image> ();
		img.enabled = false;
	}
	
	public bool isShowing {
		get {
			return img.enabled;
		}			
	}

	public void ShowAd(Sprite ad) {
		img.sprite = ad;
		img.SetNativeSize ();
		transform.localPosition = new Vector2 (Random.Range (-xOffset, xOffset) * offsetF, Random.Range (-yNegOff, yPosOff) * offsetF);
		img.enabled = true;
	}

	public void CloseAd() {
		//Debug.Log ("Close Ad");
		img.enabled = false;
	}
}
