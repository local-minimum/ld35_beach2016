using UnityEngine;
using System.Collections;

public class MenuChar : MonoBehaviour {

	[SerializeField] Animator anim;

	[SerializeField, Range(0, 1)] float pWobble;
	[SerializeField, Range(0, 1)] float pSquat;
	[SerializeField, Range(0, 1)] float pFlex;


	void Update () {
		if (Random.value < pWobble * Time.deltaTime) {
			anim.SetTrigger ("Wobble");
		} else if (Random.value < pSquat * Time.deltaTime) {
			anim.SetTrigger ("Squat");
		} else if (Random.value < pFlex * Time.deltaTime) {
			anim.SetTrigger ("Flex");
		}
	}
}
