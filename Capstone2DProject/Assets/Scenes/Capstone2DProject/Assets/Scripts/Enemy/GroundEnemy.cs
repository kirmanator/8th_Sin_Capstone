using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is lovely~
/// </summary>
public enum EnemyState{
	idle, moving, attacking, damaged, dead
}

public class GroundEnemy : Enemy {
	[Header("Current state")] 
	public EnemyState EState = EnemyState.idle;

//	[Tooltip("number of directions the object can moveTowards (initialized to 8)")]
//	public static int numMovements;

//	[Tooltip("Time object is stunned")]
//	[SerializeField] private static float stunTime;

	[Space(20)]
	#region Static variables
	[Header("Movement")]
	[Tooltip("distance the object covers in a single movement")]
	[SerializeField] private float radius;

	[Tooltip("distance the object covers in an attack sequence")]
	[SerializeField] private float attackRadius;

	[Tooltip("how long the object takes to complete a movement")]
	[SerializeField] private float duration;

	[Tooltip("wait time between each movement")]
	[SerializeField] private float waitTime;

	[Tooltip("wait time between spawning and moving")]
	[SerializeField] private float spawnWait;
	#endregion

	[Space(20)]
	[Header("Knockback")]

	[SerializeField] private bool knockedBack;
	[SerializeField] private Vector2 knockbackDirection;
	[SerializeField] private float knockbackTime;
	[SerializeField] private static float totalKnockbackTime;
	[SerializeField] private static float knockbackSpeed;

	protected Vector2 playerPos;
	private Vector2 startPos;
	private Vector2 finalPos;

	private float lerpEndTime;
	private float lerpTimeInterval;
	private float lerpElapsedTime;

	private bool canFire;
	protected bool fired;
	private float fireWaitTime;

	#region Initialized Components
	private AudioSource audio;
	#endregion

	//Setters/Getters
	#region Setters/Getters
	public EnemyState EnemyState{ get { return EState; } set { EState = value; } }
	public float TotalKnockbackTime{ get { return totalKnockbackTime; } }
	public Vector2 PlayerPos{ get { return playerPos; } }
	public bool Fired{ get{ return fired; } set{ fired = value; } }
	public bool IsAlive{ get{ return isAlive; } }
	public int AttackPoints{ get { return attackPoints; } }
	#endregion

	protected virtual void Start(){
		
		if (this is LittleFattyM) {
			radius = 12f;
			attackRadius = 18f;
			duration = 0.5f;
			waitTime = 0.4f;

		}
		else if (this is BigFattyScript) {
			radius = 24f;
			attackRadius = 60f;
			duration = 1.3f;
			waitTime = 1.2f;
		}
		spawnWait = 1f;
		knockbackSpeed = 0.5f;
		totalKnockbackTime = 1f;

		audio = GetComponent<AudioSource> ();
		StartCoroutine (SpawnWait (spawnWait));

		StartCoroutine(SetNewState (waitTime));
	}

	protected override void Update(){
		UpdateState ();
		if (this.audio.enabled == false) {
			this.enabled = false;
			EState = EnemyState.dead;
		}
	}
	#region State-based behavior
	private IEnumerator SetNewState(float waitTime){
		
		yield return new WaitForSeconds (waitTime);
		if (EState != EnemyState.damaged) {
			if (IsPlayerInRange ()) {
				//Debug.Log ("Player is in range");
				SetupMove ();
				audio.PlayOneShot (AudioManager.LFAttack, 1.5f);
				if (this is BigFattyScript && !(fired)) {
					canFire = true;
				}
				EState = EnemyState.attacking;
				//Debug.Log ("Estate is now attacking");
			}
			else {
				SetupMove ();
				audio.PlayOneShot (AudioManager.LFMove);
				EState = EnemyState.moving;
			}
			while (EState != EnemyState.idle) {
				yield return null;
			}
			if (EState != EnemyState.dead) {
				//Debug.Log ("Estate is not dead");
				StartCoroutine (SetNewState (waitTime));
			}
		}
	}

	private void UpdateState(){
		switch (EState) {
		case EnemyState.idle:
			//Debug.Log ("I am idle");
			playerPos = new Vector2 (-50, -50);
			break;
		case EnemyState.moving:
			//Debug.Log ("I am moving");
			Move (finalPos);
			break;
		case EnemyState.attacking:
			if (this is LittleFattyM) {
				//Debug.Log ("Update State is attacking");
				//Debug.Log ("I am attacking");
				Move (playerPos);
				//Debug.Log("Moving to player position");
			}
			else if (this is BigFattyScript) {
				Fire ();
			}
			break;
		case EnemyState.damaged:
			//Debug.Log ("I am damaged");
			knockedBack = true;
			Knockback ();
			break;
		case EnemyState.dead:
			//Debug.Log ("I am dead");
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			this.audio.enabled = false;
			return;
		}
	}
	#endregion
	#region Movement
	// Set the movement based on current time for object movement
	private void SetupMove(){
		lerpEndTime = Time.time + duration;
		lerpTimeInterval = Time.deltaTime / duration;
		lerpElapsedTime = lerpTimeInterval;
		startPos = transform.position;
		finalPos = MakeNewPosition ();
	}

	// Pick random vector2 position within the object's radius
	private Vector2 MakeNewPosition(){
		
		float randomX = transform.position.x + Random.Range (-1 * radius, radius);
		float randomY = transform.position.y + Random.Range (-1 * radius, radius);
		Vector2 newPos = new Vector2 (randomX, randomY);

		//If the new position is outside of the bounds, create new position

		RaycastHit2D objectHit;

		objectHit = Physics2D.Linecast (transform.position, newPos, GameManager.BGLayerMask);
		if (objectHit.collider != null) {
			newPos = objectHit.point + objectHit.normal;
		}


		// Rather than check if the OBJECT is null, we check if the COLLIDER is null, since RaycastHit2D cannot hold the value 'null' (being a struct)
		/*while ((objectHit = Physics2D.Linecast (transform.position, newPos, GameManager.Instance.BGLayerMask)).collider != null) {
			
			Debug.Log ("Linectasting");
			randomX = transform.position.x + Random.Range ((-1 * radius), radius);
			randomY = transform.position.y + Random.Range ((-1 * radius), radius);
			newPos = new Vector2 (randomX, randomY);

		}*/
		SetDirectionTowards (newPos);
		return (newPos);
	}
	
	// OverlapCircleAll to check if player is within the range
	private bool IsPlayerInRange(){

		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, attackRadius, GameManager.PlayerMask);
		if (hits.Length != 0) {
			//grab the first player it finds
			//Debug.Log(hits[0].GetComponent<Collider2D>().name);
			foreach (Collider2D col in hits) {
				if (col.GetComponent<PlayerActions> ().IsAlive) {
					playerPos = hits [0].transform.position;
					//Debug.Log ("Player position: " + playerPos);
					SetDirectionTowards (playerPos);
					return true;
				}
			}
		}
		return false;
	}

	private void Move(Vector2 final){

		//Debug.Log ("Entered Move");
		//We only need this while statement so that we know when to switch back to idle
		if(Time.time < lerpEndTime){
			transform.position = Vector2.Lerp (startPos, final, lerpElapsedTime);
			lerpElapsedTime += lerpTimeInterval;
			//Debug.Log ("Moving");
		}
		else {
			//Debug.Log ("Stopped moving");
		EState = EnemyState.idle;
		StartCoroutine (SetNewState (waitTime));
		}
	}

	private void SetDirectionTowards(Vector2 pos){
		if (pos.x < transform.position.x) {
			isFacingLeft = true;
		}
		else if(pos.x >= transform.position.x){
			isFacingLeft = false;
		}
	}
	#endregion
	#region Knockback
	public void SetupKnockback(Transform player){
		knockbackDirection = transform.position - player.position;
		knockbackDirection = knockbackDirection.normalized;
		knockbackTime = totalKnockbackTime;

	}

	private void Knockback(){
		if (knockedBack) {
			if (knockbackTime < 0) {
				knockbackTime = 0;
				knockedBack = false;
				EState = EnemyState.idle;
				StartCoroutine (SetNewState (waitTime));
			}
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockbackDirection.x * knockbackSpeed, knockbackDirection.y * knockbackSpeed);
			knockbackTime -= Time.deltaTime;
		}
	}
	#endregion

	//Should only trigger once
	public void Fire(){
		if (canFire) {
			GameObject ball = Instantiate (GameManager.SpitBall, transform.position, Quaternion.identity);
			ball.GetComponent<Projectile> ().ParentEnemy = this;
			EState = EnemyState.idle;
			//Debug.Log ("Fired");
			StartCoroutine(SetNewState (waitTime));
			fired = true;
			canFire = false;
			return;
		}
	}
	private IEnumerator SpawnWait(float waitTime){
		yield return new WaitForSeconds (waitTime);
	}

	public override void TakeDamage(int amount){
		audio.PlayOneShot (AudioManager.LFDamaged, 0.5f);
	}

}
