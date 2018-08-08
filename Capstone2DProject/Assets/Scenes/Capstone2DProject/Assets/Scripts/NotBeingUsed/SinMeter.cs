using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMeter : MonoBehaviour {

	[SerializeField]private GameObject p1,p2;
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
			maxSin = Mathf.Max (p1.GetComponent<PlayerActions> ().Greed, p1.GetComponent<PlayerActions> ().Gluttony, p1.GetComponent<PlayerActions> ().Sloth,
				p1.GetComponent<PlayerActions> ().Wrath);
		} 
		else if (direction == -1){
			maxSin = Mathf.Max (p2.GetComponent<PlayerActions> ().Greed, p2.GetComponent<PlayerActions> ().Gluttony,
				p2.GetComponent<PlayerActions> ().Sloth, p2.GetComponent<PlayerActions> ().Wrath);
		}
		height = maxSin;
		transform.localScale = new Vector3 (transform.localScale.x, height * direction / 2, transform.localScale.z);
	}
}
