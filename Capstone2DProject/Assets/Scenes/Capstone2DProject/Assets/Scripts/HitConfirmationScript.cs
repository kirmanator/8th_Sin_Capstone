using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitConfirmationScript : MonoBehaviour {

	void Start(){
		StartCoroutine(KillOnAnimEnd ());
	}

	private IEnumerator KillOnAnimEnd(){
		yield return new WaitForSeconds (GetComponent<Animation> ().clip.length);
		Destroy (gameObject);
	}
}
