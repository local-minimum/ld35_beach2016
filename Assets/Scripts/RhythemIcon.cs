using UnityEngine;
using System.Collections;

public class RhythemIcon : MonoBehaviour {

	AudioClip sound;
	RhythemChannel channel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSound(AudioClip clip) {
		sound = clip;
	}

	public void SetRhythemChannel(RhythemChannel channel) {
		this.channel = channel;
	}

	public void StartFall() {

	}
}
