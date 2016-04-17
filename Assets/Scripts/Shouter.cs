using UnityEngine;
using System.Collections;

public class Shouter : MonoBehaviour {

	[SerializeField] Body body;

	[SerializeField, Range(0, 1)] float chance = 0.1f;

	[SerializeField] AudioClip[] shouts;

	[SerializeField] AudioSource speaker;

	[SerializeField] float volume = 1.4f;

	void Update () {
		if (body.isExecising && Random.value < Time.deltaTime * chance) {
			speaker.PlayOneShot (shouts [Random.Range (0, shouts.Length)], volume);
		}
	}
}
