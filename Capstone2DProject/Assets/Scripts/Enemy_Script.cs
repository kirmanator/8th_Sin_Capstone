using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {

	public int HP;
	private int moveTowards;
	public float velocity;
	public float default_vel = 2.5f;
	public Vector2 final_pos;
	private GameObject p1;
	private GameObject p2;
	public bool wasAttacked = false;
	private Animator anim;
	public bool hasAnim = false;
	// Use this for initialization
	void Start () {
		p1 = GameObject.FindGameObjectWithTag ("Player1");
		p2 = GameObject.FindGameObjectWithTag ("Player2");
		HP = 3;
		comparePlayerDist ();
		if (GetComponent<Animator> ()) 
		{
			anim = GetComponent<Animator> ();
			hasAnim = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (moveTowards == 1) {
			final_pos = p1.transform.position;
		} 
		else if(moveTowards == 2){
			final_pos = p2.transform.position;
		}

		transform.position = Vector2.MoveTowards (transform.position, final_pos, velocity * Time.deltaTime);
		if (wasAttacked) 
		{
			if(hasAnim)
			anim.enabled = true;
		} 
		else 
		{
			if(hasAnim)
			anim.enabled = false;
		}
		if (HP <= 0) 
		{
			Destroy (this.gameObject);
			if (moveTowards == 1) {
				p1.GetComponent<PlayerActions> ().wrath += 0.5f;
			} 
			else if (moveTowards == 2) {
				p2.GetComponent<PlayerActions> ().wrath += 0.5f;
			}
		}

	}

	void comparePlayerDist()
	{
		
		if (Mathf.Abs (Vector2.Distance ((transform.position), (p1.transform.position))) > Mathf.Abs (Vector2.Distance ((transform.position), (p2.transform.position)))) {
			moveTowards = 1;
		} else {
			moveTowards = 2;
		}
	}
}
