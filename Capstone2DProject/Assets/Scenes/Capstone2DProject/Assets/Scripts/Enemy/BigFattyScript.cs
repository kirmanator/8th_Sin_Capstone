using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFattyScript : GroundEnemy {

	[SerializeField] private bool moving, attacking, damaged;

	private Animator anim;
	private SpriteRenderer rndr;

	private static Color damagedColor, deadColor, origColor;

	//Setters/Getters

	protected override void Start () {
		base.Start ();
		isAlive = true;

		anim = GetComponent<Animator> ();
		rndr = GetComponent<SpriteRenderer> ();
		//radius = GameMaster.GM.radiusBF;

		damagedColor = new Color (1f, 0, 0);
		deadColor = new Color (130f/255f, 130f/255f, 130f/255f);
		origColor = new Color (1f, 1f, 1f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		anim.SetBool ("isMoving", moving);
		anim.SetBool ("damaged", damaged);
		anim.SetBool ("isAttacking", attacking);
		anim.SetBool ("isAlive", isAlive);
		//Debug.Log ("Is Alive: " + isAlive);

		UpdateAnimator ();
		UpdateFaceDirection ();
	}

	private void UpdateAnimator(){
		switch (EState) {
		case EnemyState.idle:
			moving = false;
			attacking = false;
			break;
		case EnemyState.moving:
			moving = true;
			attacking = false;
			break;
		case EnemyState.attacking:
			moving = false;
			attacking = true;
			break;
		case EnemyState.damaged:
			damaged = true;
			break;
		case EnemyState.dead:
			damaged = false;
			isAlive = false;
			break;
		}
	}

	private void UpdateFaceDirection(){
		if (isFacingLeft) {
			transform.localScale = new Vector3 (-3f, 3f, 1);
		} 

		else {
			transform.localScale = new Vector3 (3f, 3f, 1);
		}
	}

	public override void TakeDamage(int d)
	{
		if (canBeHit) {
			health -= d;

			if (health <= 0) {
				//gEnemy.EnemyState = EnemyState.dead;
				EState = EnemyState.dead;
				GameManager.NumEnemiesKilled++;
				Instantiate (GameManager.Food, transform.position - new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0), transform.rotation);
				Instantiate (GameManager.Coin, transform.position - new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0), transform.rotation);
				isAlive = false;
				StartCoroutine (changeColor ());
			}
			else {
				EState = EnemyState.damaged;
				StartCoroutine (changeColor ());
				canBeHit = false;
			}
		}
	}

	private IEnumerator changeColor()
	{
		if (isAlive == false) {
			rndr.color = deadColor;
			yield return null;
		} 
		else {
			rndr.color = damagedColor;
			yield return new WaitForSeconds(TotalKnockbackTime);
			rndr.color = origColor;
			SetCanBeHit (true);
		}
	}

	void SetCanBeHit(bool b){
		canBeHit = b;
	}

	// Method to determine random direction
	// Method to check for surroundings
	// Method to check if player is in range
	// Method to move
	// Method to fire bullet
	// Method to take damage
	// Method to die


}
