using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMeter : MonoBehaviour {

	private GameObject p1,p2;
	public float maxSin = 1.0f;
	public float height;
	public int direction = 1;
	// Use this for initialization
	void Start () {
		p1 = GameObject.FindGameObjectWithTag ("Player1");
		p2 = GameObject.FindGameObjectWithTag ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == 1) {
			maxSin = Mathf.Max (p1.GetComponent<PlayerActions> ().greed, p1.GetComponent<PlayerActions> ().gluttony, p1.GetComponent<PlayerActions> ().sloth,
				p1.GetComponent<PlayerActions> ().wrath);
		} 
		else if (direction == -1){
			maxSin = Mathf.Max (p2.GetComponent<PlayerActions> ().greed, p2.GetComponent<PlayerActions> ().gluttony,
				p2.GetComponent<PlayerActions> ().sloth, p2.GetComponent<PlayerActions> ().wrath);
		}
		height = maxSin;
		transform.localScale = new Vector3 (transform.localScale.x, height * direction / 2, transform.localScale.z);
	}
}
