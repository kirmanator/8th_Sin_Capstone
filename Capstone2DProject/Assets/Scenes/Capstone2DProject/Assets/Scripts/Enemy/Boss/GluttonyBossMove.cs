using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyBossMove : MonoBehaviour {

	[SerializeField] private Transform startPos, endPos;
	[SerializeField] private float moveSpeed;
	private bool canMove;
	private bool playAnim;
	private Animator anim;

	// Use this for initialization
	void Start () {
		transform.position = startPos.position;
		anim = GetComponent<Animator> ();
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (new Vector2(transform.position.x, transform.position.y), endPos.position) <= 0.5f) {
			//Debug.Log ("I'm too close");
			canMove = false;
			anim.Play ("TrailerAnim");
			return;
		}
		if (canMove) {
			transform.position = Vector2.MoveTowards (new Vector2(transform.position.x, transform.position.y), endPos.position, moveSpeed * Time.deltaTime);
			//Debug.Log ("I'm moving");
		}
	}
}
