using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

	public float MoveHorisontal; // move speed for left and right 
	public float MoveVertical; // move speed for up and down 
	private float MoveVelocityH;
	private float MoveVelocityV;
	public KeyCode up,down,left,right;
	public bool isFacingLeft;
	private Animator player_anim;
	public bool isMoving, isDashing;

	// Use this for initialization
	void Start () {
		isFacingLeft = false;
		player_anim = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		MoveVelocityH = 0f; // prevents player from sliding 
		MoveVelocityV = 0f; // prevents player from sliding 
		player_anim.SetBool("isMoving", isMoving);
		player_anim.SetBool("isDashing", isDashing);

        

            if (isFacingLeft) {
			transform.localScale = new Vector2 (-1, 1);
		} 
		else {
			transform.localScale = new Vector2 (1, 1);
		}

		if (Input.GetKey(right)) // player moves right 
		{
			MoveVelocityH = MoveHorisontal; // sets MoveVelosityH to public MoveHorisontal
			isFacingLeft = false;
			isMoving = true;
		}

		if (Input.GetKey(left)) // player moves left
		{
			MoveVelocityH = -MoveHorisontal; // sets MoveVelosityH to negative MoveHorsital 
			isFacingLeft = true;
			isMoving = true;
		}

		if (Input.GetKey(up)) // moves player up 
		{
			MoveVelocityV = MoveVertical; // sets MoveVelocityV to MoveVertical
			isMoving = true;
		}

		if (Input.GetKey(down)) // moves player down 
		{
			MoveVelocityV = -MoveVertical; // sets MoveVelocityV to negative MoveVertical
			isMoving = true;
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(MoveVelocityH, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment  
		if (MoveVelocityH == 0 && MoveVelocityH == 0) {
			isMoving = false;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, MoveVelocityV); // sets velosity for veritacl movment
	}
}
