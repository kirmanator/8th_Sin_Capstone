using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFattyM : GroundEnemy {

    public bool isMoving;

	private Animator anim;
	public Sprite deathSprite;
	private static Color damagedColor, deadColor, origColor;

    //public bool TargitFound;
    //public Vector2 place;

	//public bool isFacingLeft;
	public bool isAttacking, damaged, repented;

	//create 2 dimensional Vector2 that saves different types of movements
	//use startPt as the current object's position
	//save the Vector2 points from 'createBezierCurve' in an array
	//put that array in the typesOfMovements
	//after calculating items in array, set endPt and controlPt to new positions (play around with it in editor)
	//set startTime
	//use Time.time - startTime
	//Repeat process
	//positions = new List<Vector2> ((int) (duration / Time.deltaTime) +30)

//	private GameObject spirit;
	[Tooltip("Is the object done spawning?")]
	public bool doneEntering;

	private SpriteRenderer rndr;

    protected override void Start () {

//		spirit = transform.Find ("gluttonySpirit").gameObject;
//		spirit.SetActive (false);
		base.Start();

		rndr = GetComponent<SpriteRenderer> ();

		//isAlive = true;
		anim = GetComponent<Animator> ();

		damagedColor = new Color (1f, 0, 0);
		deadColor = new Color (130f/255f, 130f/255f, 130f/255f);
		origColor = new Color (1f, 1f, 1f);
    }
	
//    public void AnimatorSwitch(string initialState, string newState)
//    {
//        anim.SetBool(initialState, false);
//        anim.SetBool(newState, true);
//    }

    void Die()
    {
        anim.SetBool("isAlive", false);
        GetComponent<CircleCollider2D>().enabled = false;
		// GetComponent<Rigidbody2D> ().isKinematic = true;
    }

	protected override void Update () {

		base.Update ();
		anim.SetBool("isMoving",isMoving);
		anim.SetBool("isAttacking",isAttacking);
		anim.SetBool("damaged",damaged);
		anim.SetBool("isAlive",isAlive);
		anim.SetBool ("repented", repented);
		UpdateAnimator ();
		UpdateFaceDirection ();
		//Debug.Log ("Little fatty update");
		if (!isAlive) {
			return;
		}

    }

	private void UpdateAnimator(){
		//switch (gEnemy.EnemyState) {
		switch(EState){
		case EnemyState.idle:
			isMoving = false;
			isAttacking = false;
			damaged = false;
			break;
		case EnemyState.moving:
			isMoving = true;
			isAttacking = false;
			break;
		case EnemyState.attacking:
			isMoving = false;
			isAttacking = true;
			//Debug.Log ("Child isAttacking is true");
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
			transform.localScale = new Vector3 (-6f, 6f, 1);
		} 

		else {
			transform.localScale = new Vector3 (6f, 6f, 1);
		}
	}

	public override void TakeDamage(int d)
    {
		if (canBeHit) {
			health -= d;

			if (health <= 0) {
				//gEnemy.EnemyState = EnemyState.dead;
				if (isAlive) {
					EState = EnemyState.dead;
					GameManager.NumEnemiesKilled++;
					Instantiate (GameManager.Food, transform.position - new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0), transform.rotation);
					Instantiate (GameManager.Coin, transform.position - new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0), transform.rotation);
					isAlive = false;
					StartCoroutine (ChangeColor ());
				}

			}
			else {
				EState = EnemyState.damaged;
				canBeHit = false;
				StartCoroutine (ChangeColor ());
			}
		}
    }

	#region Old Code
//	private IEnumerator InterpolateMovement(List<Vector2>[] typesOfMovements, float duration, int numMovements)
//	{
//		Vector2 curPos = transform.position;
//		float startTime = Time.time;
//		float elapsedTime;
//		float endTime = startTime + duration;
//		int index = 0;
//		int movement = 0;
//
//		if(HasSurroundings()) {
//			
//			if (!PlayerInRange()) {
//				int[] bounds = checkBounds ();
//				movement = bounds [randMovement.Next (0, bounds.Length)];
//			} 
//			else {
//
//				isAttacking = true;
//				movement = MakeNextMovement (GetPlayerPosition (GetSurroundings ()), numMovements);
//			}
//		} 
//		else {
//			//pick a random direction
//			IsMoving = true;
//			movement = randMovement.Next (0, numMovements);
//		}
//		SetDirection (movement);
//
//		audio.PlayOneShot (moveClip, 0.15f);
//		while ((Time.time < endTime) && (index < typesOfMovements[movement].Count)) {
//
//			if (damaged) {
//				Debug.Log ("knockback yo");
//				StartCoroutine (wait ());
//
//				yield return null;
//			}
//			else {
//				transform.position = typesOfMovements [movement] [index++] + curPos;
//				yield return null;
//			}
//		}
//
//		IsMoving = false;
//		isAttacking = false;
//
//		yield return new WaitForSeconds (waitTime);
//		yield return new WaitForEndOfFrame ();
//		if (isAlive) {
//			StartCoroutine (InterpolateMovement (typesOfMovements, duration, numMovements));
//		}
//
//	}
//

//	private Collider2D[] GetSurroundings()
//	{
//		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, (radius*2), bgLayerMask);
//		return colls;
//	}
//

//	private bool HasSurroundings()
//	{
//		return (GetSurroundings().Length > 0);
//	}
//

//	private Vector2 GetPlayerPosition(Collider2D[] colls)
//	{
//		Vector2 targetPosition = new Vector2(-1,-1);
//		if (HasSurroundings()) {
//			for (int i = 0; i < colls.Length; i++) {
//				
//				if (colls [i].gameObject.CompareTag ("Player")) {
//					targetPosition = colls [i].gameObject.transform.position;
//					return targetPosition;
//				}
//			}
//		}
//		return targetPosition;
//	}
//

//	private bool PlayerInRange()
//	{
//		return (GetPlayerPosition (GetSurroundings ()) != (Vector2.one * -1));
//	}
//

//			if (Isknockback > 0) {
//				//StopCoroutine(InterpolateMovement(typesOfMovements, duration, numMovements));
//				//typesOfMovements[movement].Clear();
//				//transform.position = new Vector2 (knockbackDerection.x * knockBackLenth,knockbackDerection.y * knockBackLenth);
//				StopCoroutine(moving);
//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockbackDerection.x * knockBackLenth, knockbackDerection.y * knockBackLenth);
//				Isknockback = Isknockback - Time.deltaTime;
//				damaged = true;
//				Debug.Log ("goingback");
//			}
//			else if (Isknockback < 0) {
//				Isknockback = 0;
//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
//				damaged = false;
//				moving = StartCoroutine (InterpolateMovement (GameManager.Instance.typesOfMovements, duration, numMovements));
//				Debug.Log ("backtonormal");
//			}

//	private int MakeNextMovement(Vector2 target, int numMove)
//	{
//		float interval = 360 / numMove;
//		for (int i = 0; i < numMove; i++) {
//			if (target.x < transform.position.x) {
//				if (target.y < transform.position.y) {
//					if (i * interval > 180 && i * interval < 270) {
//						return i;
//					}
//				} 
//				else {
//					if (i * interval >= 90 && i * interval <= 180) {
//						return i;
//					}
//				}
//			} 
//			else {
//				if (target.y < transform.position.y) {
//					if (i * interval >= 270 && i * interval <= 0) {
//						return i;
//					}
//				} 
//				else {
//					if (i * interval >= 0 && i * interval <= 90) {
//						return i;
//					}
//				}
//			}
//		}
//		return randMovement.Next (0, numMove);
//	}

//	private void SetDirection(int m)
//	{
//		if ((m >= 0 && m < numMovements / 4) || (m < numMovements && m >= (numMovements / 4) * 3)) {
//			isFacingLeft = false;
//		} 
//		else {
//			isFacingLeft = true;
//		}
//	}

//	private IEnumerator SpawnWait(float seconds)
//	{
//		yield return new WaitForSeconds (seconds);
//	}

//	void OnTriggerExit2D(Collider2D col)
//	{
//		if ((col.tag == "Weapon1") || (col.tag == "Weapon2"))
//		{
//			attacked = false;
//            //IsMoving = true;
//		}
//	}

//	private int[] checkBounds()
//	{
//		int[] temp = new int[numMovements];
//		float angleInterval = 360 / numMovements;
//		int index = 0;
//
//		bool cantGoLeft = Physics2D.Linecast (transform.position, new Vector2(transform.position.x - (radius * 2), transform.position.y), bgLayerMask);
//		bool cantGoRight = Physics2D.Linecast (transform.position,new Vector2(transform.position.x + (radius * 2), transform.position.y), bgLayerMask);
//		bool cantGoUp = Physics2D.Linecast (transform.position, new Vector2(transform.position.x, transform.position.y + (radius * 2)), bgLayerMask);
//		bool cantGoDown = Physics2D.Linecast (transform.position, new Vector2(transform.position.x, transform.position.y - (radius * 2)), bgLayerMask);
//
//		if (cantGoLeft) {
//			for (int i = 0; i < numMovements; i++) 
//			{
//				if (i * angleInterval < 90 || i * angleInterval > 270) {
//					temp [index++] = i;
//				}
//			}
//		}
//
//        if (cantGoDown){
//			for (int i = 0; i < numMovements; i++)
//	            {
//	                if (i * angleInterval > 0 && i * angleInterval < 180){
//	                    temp[index++] = i;
//	                }
//	            }
//	        }
//		if (cantGoRight){
//		for (int i = 0; i < numMovements; i++)
//            {
//                if (i * angleInterval > 90 && i * angleInterval < 270){
//                    temp[index++] = i;
//                }
//            }
//        }
//        if (cantGoUp){
//		for (int i = 0; i < numMovements; i++)
//			{
//                if (i * angleInterval > 180 && i * angleInterval < 360){
//                    temp[index++] = i;
//                }
//            }
//        }
//        
//        
//		int[] result = new int[index + 1];
//		for (int i = 0; i < result.Length; i++) {
//			result [i] = temp [i];
//		}
//		return result;
//
//	}
	#endregion

	private IEnumerator wait()
	{
		yield return new WaitForSeconds (1);
	}

	private IEnumerator ChangeColor()
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

	public void SetCanBeHit(bool b){
		canBeHit = b;
	}

//	public override void TakeDamage(int amount){
//		base.TakeDamage (amount);
//		health -= amount;
//	}
}
