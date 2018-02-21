using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFattyM : MonoBehaviour {
	
    public enum FattyState
    {
        Moving, Attacking, Damaged, Dead
    }

    public FattyState fattyState = FattyState.Moving;

    public bool IsMoving;
	public bool isFacingLeft;
	public LayerMask bgLayerMask;
	private Animator anim;
	public Sprite deathSprite;
    public Vector3 dropLocation;
    public Vector3 dropLocation2;
    //private SpriteRenderer sprr;

    //public bool TargitFound;
    //public Vector2 place;

    //public bool isFacingLeft;
    public bool attacked, isAttacking, damaged, isAlive, stunned, repented;

	public int HP;

	//create 2 dimensional Vector2 that saves different types of movements
	//use startPt as the current object's position
	//save the Vector2 points from 'createBezierCurve' in an array
	//put that array in the typesOfMovements
	//after calculating items in array, set endPt and controlPt to new positions (play around with it in editor)
	//set startTime
	//use Time.time - startTime
	//Repeat process
	//positions = new List<Vector2> ((int) (duration / Time.deltaTime) +30)

	[Tooltip("number of directions the object can moveTowards (initialized to 8)")]
	public int numMovements = 8;
	[Tooltip("distance the object covers in a single movement")]
	public float radius = 2.0f;
	[Tooltip("how long the object takes to complete a movement")]
	public float duration = 0.5f;
	[Tooltip("wait time between movements")]
	public float waitTime = 1.0f;
	[Tooltip("Time object is stunned")]
	public float stunTime;
	[Tooltip("wait time between spawning and moving")]
	public float spawnWait = 1.0f;
	[Tooltip("the lower the value, the closer you get to the line between startPt and endPt")]
	public float ctrlAngleAdjuster = 0.5f;
	[Tooltip("Should only be value between 0 and 1. The higher this value, the greater the height of the controlPt")]
	public float ctrlHeightAdjuster = 0.5f;
	public System.Random randMovement;
	private GameMaster GM;
	public GameObject food, coin;
	private GameObject spirit;
	[Tooltip("Is the object done spawning?.,")]
	public bool doneEntering;
	private AudioSource audio;
	public AudioClip damagedClip;
	public AudioClip moveClip;
    public Vector2 knockbackDerection;
    public float Isknockback;
    public float knockBackTime;
    public Vector2 knockbackDistans;
    public float knockBackLenth;
	public bool knockedBack;
	public float enterScale;

	private Coroutine moving;

    // Use this for initialization
    void Start () {

		HP = 2;
		spirit = transform.Find ("gluttonySpirit").gameObject;
		spirit.SetActive (false);
		audio = GetComponent<AudioSource> ();
		//sprr = this.GetComponent<SpriteRenderer> ();
		isAlive = true;
		anim = GetComponent<Animator> ();
		randMovement = new System.Random ();
		GM = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		StartCoroutine (SpawnWait(spawnWait));
		Debug.Log ("Interpolate Movement");
		//transform.localScale = new Vector2 (enterScale, enterScale);
		moving = StartCoroutine (InterpolateMovement (GM.typesOfMovements, duration, numMovements));

    }
	
    public void AnimatorSwitch(string initialState, string newState)
    {
        anim.SetBool(initialState, false);
        anim.SetBool(newState, true);
    }

    void Die()
    {
        anim.SetBool("isAlive", false);
        GetComponent<CircleCollider2D>().enabled = false;
    }

	void Update () {
		anim.SetBool("isMoving",IsMoving);
		anim.SetBool("isAttacking",isAttacking);
		anim.SetBool("damaged",damaged);
		anim.SetBool("isAlive",isAlive);
		anim.SetBool ("repented", repented);

        switch (fattyState)
        {
            case FattyState.Moving:
                //put movement code here

                //if attacking
                //AnimatorSwitch("isMoving", "isAttacking");

                //if damaged
                //StopCoroutine(moving)
                //AnimatorSwitch("isMoving", "damaged");

                //if dead
                //StopCoroutine(moving)
                //Die();
                break;

            case FattyState.Attacking:
                //put attack code here
                //after attack is done, do the following
                //AnimatorSwitch("isAttacking", "isMoving");
                break;

            case FattyState.Damaged:
                // put damaged code here
                // takeDamage(1);
                break;

            case FattyState.Dead:
                //put death code here
                break;

        }

        if (damaged) {
			Debug.Log ("damaged");
			//Isknockback -= Time.deltaTime;

		}
		if (HP <= 0) {
			isAlive = false;
		}
		if (isFacingLeft) {
			transform.localScale = new Vector3 (-1.5f, 1.5f, 1);
		} 
		else {
			transform.localScale = new Vector3 (1.5f, 1.5f, 1);
		}
//		if (repented) {
//            if (isAlive)
//            {
//                spirit.SetActive(true);
//                HP = 0;
//				GM.numEnemiesKilled++;
//                isAlive = false;
//            }
//            
//            repented = false;
//
//		}
		if (isAlive) {

			if (Isknockback > 0) {
				//StopCoroutine(InterpolateMovement(typesOfMovements, duration, numMovements));
				//typesOfMovements[movement].Clear();
				//transform.position = new Vector2 (knockbackDerection.x * knockBackLenth,knockbackDerection.y * knockBackLenth);
				StopCoroutine(moving);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockbackDerection.x * knockBackLenth, knockbackDerection.y * knockBackLenth);
				Isknockback = Isknockback - Time.deltaTime;
				damaged = true;
				Debug.Log ("goingback");
			}
			else if (Isknockback < 0) {
				Isknockback = 0;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				damaged = false;
				moving = StartCoroutine (InterpolateMovement (GM.typesOfMovements, duration, numMovements));
				Debug.Log ("backtonormal");
			}
		}
		else {
			StopCoroutine (moving);
		}
		
        
       // if (damaged == true && Isknockback <= 0)
       // {
         //   Isknockback = knockBackTime;
       // }
        //		if (transform.localScale.x >= 1.5f) {
        //			doneEntering = true;
        //		}
        //		if (!doneEntering) {
        //			transform.localScale = new Vector2 (Mathf.Lerp (transform.localScale.x, 1.5f,enterScale), Mathf.Lerp (transform.localScale.y, 1.5f,enterScale));
        //			enterScale += Time.deltaTime;
        //		}


    }
    public float takeDamage(int d)
    {
        HP -= d;


		if (HP <= 0) {
			if (isAlive) {
                
				GM.numEnemiesKilled++;
                //Instantiate (food, transform.position - new Vector3 (Random.Range(-2, 2), Random.Range(-2, 2), 0), transform.rotation);
                //Instantiate (coin, transform.position - new Vector3 (Random.Range(-2, 2), Random.Range(-2, 2), 0), transform.rotation);
                dropLocation = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
                dropLocation2 = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
                Instantiate(food, transform.position, transform.rotation);
                 
                Instantiate (coin, transform.position , transform.rotation);
                StopCoroutine(changeColor ());
				GetComponent<SpriteRenderer> ().color = GM.enemyDeathColor;
				isAlive = false;
                Random.Range(1,3);
				return 0f;
			}
		}
		else {
			StartCoroutine(changeColor ());
		}

        return 0f;
    }
	private IEnumerator InterpolateMovement(List<Vector2>[] typesOfMovements, float duration, int numMovements)
	{
		Vector2 curPos = transform.position;
		float startTime = Time.time;
		float elapsedTime;
		float endTime = startTime + duration;
		int index = 0;
		int movement = 0;
		IsMoving = true;

		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, radius, bgLayerMask);
		if (colls.Length > 0) {
			bool playerInRange = false;
			Vector2 targetPosition = Vector2.zero;
			for (int i = 0; i < colls.Length; i++) {
				Debug.Log ("coll tag: " + colls [i].gameObject.tag);
				if (colls [i].gameObject.CompareTag ("Player")) {
					Debug.Log ("PlayerinRange");
					playerInRange = true;
					targetPosition = colls [i].gameObject.transform.position;
					break;
				}
			}
			if (!(playerInRange)) {
				int[] bounds = checkBounds ();
				movement = bounds [randMovement.Next (0, bounds.Length)];
			} 
			else {
				Debug.Log ("IsAttacking");
				isAttacking = true;
				float interval = 360 / numMovements;
				for (int i = 0; i < numMovements; i++) {
					if (targetPosition.x < transform.position.x) {
						if (targetPosition.y < transform.position.y) {
							if (i * interval > 180 && i * interval < 270) {
								movement = i;
								break;
							}
						} 
						else {
							if (i * interval >= 90 && i * interval <= 180) {
								movement = i;
								break;
							}
						}
					} 
					else {
						if (targetPosition.y < transform.position.y) {
							if (i * interval > 270 && i * interval <= 0) {
								movement = i;
								break;
							}
						} 
						else {
							if (i * interval >= 0 && i * interval <= 90) {
								movement = i;
								break;
							}
						}
					}
				}

			}
		} 
		else {
			//pick a random direction
			movement = randMovement.Next (0, numMovements);
		}
		if ((movement >= 0 && movement < numMovements / 4) || (movement < numMovements && movement >= (numMovements / 4) * 3)) {
			isFacingLeft = false;
		} 
		else {
			isFacingLeft = true;
		}
		audio.PlayOneShot (moveClip, 0.15f);
		while ((Time.time < endTime) && (index < typesOfMovements[movement].Count)) {

			if (damaged) {
				Debug.Log ("knockback yo");
				StartCoroutine (wait ());

				//typesOfMovements [movement].Clear ();
				//transform.position = new Vector3 (transform.position.x + 3 * ((isFacingLeft) ? -1 : 1),
					//transform.position.y, transform.position.z);
				yield return null;
			}
			else {


				transform.position = typesOfMovements [movement] [index++] + curPos;
				yield return null;

			}

		}

		IsMoving = false;
		isAttacking = false;

		yield return new WaitForSeconds (waitTime);
		yield return new WaitForEndOfFrame ();
		if (isAlive) {
			StartCoroutine (InterpolateMovement (typesOfMovements, duration, numMovements));
		}

	}

	private IEnumerator SpawnWait(float seconds)
	{
		yield return new WaitForSeconds (seconds);
	}

//	void OnTriggerStay2D(Collider2D col)
//	{
//		if (col.tag == "Player") {
//			if (col.gameObject.GetComponent<WeaponScript> ().isAttacking) {
//					
//				if (HP > 0) {
//					damaged = true;
//                    knockbackDerection = gameObject.transform.position - col.transform.position;
//                    knockbackDerection = knockbackDerection.normalized;
//
//                    HP--;
//					audio.PlayOneShot (damagedClip);
//				}
//				else if (HP == 0) {
//					if (isAlive) {
//						col.gameObject.GetComponentInParent<PlayerActions> ().wrath += 0.5f;
//
//						Instantiate (food, transform.position, transform.rotation);
//						Instantiate (coin, transform.position, transform.rotation);
//						GM.numEnemiesKilled++;
//						isAlive = false;
//					}
//				}
//				col.gameObject.GetComponent<WeaponScript> ().isAttacking = false;
//			}
//
//		}
//
//		//damaged = false;
//	}

	void OnTriggerExit2D(Collider2D col)
	{
		if ((col.tag == "Weapon1") || (col.tag == "Weapon2"))
		{
			attacked = false;
            //IsMoving = true;
		}
	}

	private int[] checkBounds()
	{
		int[] temp = new int[numMovements];
		float angleInterval = 360 / numMovements;
		int index = 0;

		bool cantGoLeft = Physics2D.Linecast (transform.position, new Vector2(transform.position.x - radius, transform.position.y), bgLayerMask);
		bool cantGoRight = Physics2D.Linecast (transform.position,new Vector2(transform.position.x + radius, transform.position.y), bgLayerMask);
		bool cantGoUp = Physics2D.Linecast (transform.position, new Vector2(transform.position.x, transform.position.y + radius), bgLayerMask);
		bool cantGoDown = Physics2D.Linecast (transform.position,new Vector2(transform.position.x, transform.position.y - radius), bgLayerMask);

        {
                for (int i = 0; i < numMovements; i++)
                {
                    if (i * angleInterval < 90 || i * angleInterval > 270)
                    {
                        //Debug.Log ("Left index: " + index);
                        temp[index++] = i;
                    }
                }
            }

            if (cantGoDown)
            {
                for (int i = 0; i < numMovements; i++)
                {
                    if (i * angleInterval > 0 && i * angleInterval < 180)
                    {
                        //Debug.Log ("Down index: " + index);
                        temp[index++] = i;
                    }
                }
            }
            if (cantGoRight)
            {
                for (int i = 0; i < numMovements; i++)
                {
                    if (i * angleInterval > 90 && i * angleInterval < 270)
                    {
                        //Debug.Log ("Right index: " + index);
                        temp[index++] = i;
                    }
                }
            }
            if (cantGoUp)
            {
                for (int i = 0; i < numMovements; i++)
                {
                    if (i * angleInterval > 180 && i * angleInterval < 360)
                    {
                        //Debug.Log ("Up index: " + index);
                        temp[index++] = i;
                    }
                }
            }
        
        
		int[] result = new int[index + 1];
		for (int i = 0; i < result.Length; i++) {
			result [i] = temp [i];
			//Debug.Log("result: movement " + result[i] + " available");
		}
		return result;

	}

	private IEnumerator wait()
	{
		yield return new WaitForSeconds (1);
	}

	private IEnumerator changeColor()
	{
		SpriteRenderer rndr = GetComponent<SpriteRenderer> ();
		if (HP <= 0) {
			rndr.color = new Color (130, 130, 130);
			yield return null;
		} 
		else {
			damaged = true;
			rndr.color = new Color (255, 0, 0);
			yield return new WaitForSeconds(1);
			damaged = false;
			rndr.color = new Color (255, 255, 255);

		}
	}

	void OnDestroy()
	{
		
	}

    
}
