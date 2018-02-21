using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class DestroyAfterAnim : MonoBehaviour {

	public float length;

	// Use this for initialization
	void Start () {
		length = GetComponent<Animation> ().clip.length;
		StartCoroutine (KillOnAnimEnd ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private IEnumerator KillOnAnimEnd()
	{
		yield return new WaitForSeconds (length);
		Destroy (this.gameObject);
	}
}
