using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour {

	[SerializeField] private PlayerActions otherPlayer;
	private bool playerInRange;

	public bool PlayerInRange{ get { return playerInRange; } }
	void Start () {
		GetComponent<Collider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name == otherPlayer.gameObject.name) {
			playerInRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.name == otherPlayer.gameObject.name) {
			playerInRange = false;
		}
	}
}
