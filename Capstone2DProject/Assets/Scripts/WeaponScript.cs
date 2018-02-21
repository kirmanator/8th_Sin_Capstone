using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public bool isAttacking;
	public PlayerActions myPlayer;

	// Use this for initialization
	void Start () {
		isAttacking = false;
		myPlayer = GetComponentInParent<PlayerActions> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (myPlayer.attack)) {
			isAttacking = true;
			myPlayer.isAttacking = true;
		}

		if (Input.GetKeyUp (myPlayer.attack)) {
			isAttacking = false;
			myPlayer.isAttacking = false;
	}
	}
}
