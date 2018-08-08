using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Vector2 moveDirection;
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float lifeTime;
	[SerializeField] private int attackPoints;
	private float life;
	private bool isMoving;

	private Rigidbody2D rb2d;
	[SerializeField] private AnimationClip animClip;
	private Animator anim;
	private GroundEnemy parentEnemy;

	public bool IsMoving{ get { return isMoving; } }
	public int AttackPoints{ get { return attackPoints; } }
	public GroundEnemy ParentEnemy{ set { parentEnemy = value; } }
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
		rb2d = GetComponent<Rigidbody2D> ();
		life = lifeTime;
		isMoving = true;
		moveDirection = new Vector2(transform.position.x - parentEnemy.PlayerPos.x, 
			transform.position.y - parentEnemy.PlayerPos.y);
		moveDirection = moveDirection.normalized;
		moveDirection *= -1;
		//Debug.Log ("my move direction: " + moveDirection);
	}
	
	// Update is called once per frame
	void Update () {
		if (life < 0) {
			life = 0;
			isMoving = false;
			anim.enabled = true;
		}
		else {
			life -= Time.deltaTime;
		}
		if (isMoving) {
			rb2d.velocity = new Vector2 (moveDirection.x * speed, moveDirection.y * speed);
			transform.Rotate (Vector3.forward * rotateSpeed * Time.deltaTime);
		}
		else {
			rb2d.velocity = Vector2.zero;
			anim.Play ("Gluttony2bullet");
			return;
		}
	}

	public void Disappear(){
		if (parentEnemy != null) {
			//Debug.Log ("I have a dad!");
			parentEnemy.GetComponent<BigFattyScript> ().Fired = false;
		}
		Destroy (gameObject);
	}
}
