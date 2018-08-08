using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class MenuSpriteAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animation> ().playAutomatically = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
