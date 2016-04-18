using UnityEngine;
using System.Collections.Generic;

public class AdSystem : MonoBehaviour {

	[SerializeField] AdsScript[] Popups;
	[SerializeField] Sprite[] Ads;
	[SerializeField] float probability;
	[SerializeField] GameProgress progress;
	[SerializeField, Range(0, 1)] float delay = 0.1f;
	[SerializeField] Body body;

	void Update () {
		if (body.isPlaying && Random.value < Time.deltaTime * probability * (1 - progress.progress)) {
			int adsToShow = Random.Range (Mathf.FloorToInt (Ads.Length * (1 - progress.progress) * 0.5f), Ads.Length);
			StartCoroutine (Splash (adsToShow));	
		} else if (!body.isPlaying) {
			for (int i = 0; i < Popups.Length; i++)
				Popups [i].CloseAd ();
		}
	}

	IEnumerator<WaitForSeconds> Splash(int adsToShow) {
		for (int i=0; i < Popups.Length; i++) {
			if (!Popups [i].isShowing) {
				Popups [i].ShowAd (Ads [Random.Range (0, Ads.Length)]);
				adsToShow--;
				if (adsToShow < 1)
					break;
				yield return new WaitForSeconds (delay);
			}
		}

	}
}
