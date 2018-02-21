	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {

	public KeyCode attack, attack2;
	public KeyCode repent, repent2;
	public bool enemyContact = false;
	public float repentRadius;
	private float knockbackX,knockbackY;
	private Vector2 knockback;
	public float thrust;
	public float greed,gluttony,sloth,wrath;
	public float HP, maxHP, baseHP = 5.0f;
	public float resistGreed, resistGluttony, resistSloth, resistWrath;
	private SpriteRenderer sprr;
	private GameObject heavenLight;
	public LayerMask repentMask;
	public float walkItOffTime, origWalkItOffTime;
	public bool walkItOff;
    public GameObject SinIndector;
	public GameObject mainCam;

	private Color32 origColor;
	//private Animator anim;
	public bool damaged, isAttacking, isMoving, repenting, canRepent, isAlive;
	private Animator player_anim;
	private GameMaster gm;

    //John's code
    public GameObject CoinCountText;
	public GameObject HPBar;
	public float repentCooldown, orig_repentCooldown;
	private GameObject cooldownBar;
    public Vector2 knockbackDerection;

	public AudioSource audio;
	public AudioClip attackClip;
	private CameraPan cpan;

	private GameObject hitConfirm;
    public Droped_pickup dPickup;
    public bool canReviveSelf;
    public PlayerActions otherPlayer;
    public bool healInRang;
   
	// Use this for initialization
	void Start () {

        
        dPickup = FindObjectOfType<Droped_pickup>();
		mainCam = GameObject.Find ("Main Camera");
		cpan = mainCam.GetComponent<CameraPan>();
		gm = GameObject.Find ("GameMaster").GetComponent<GameMaster> ();
		hitConfirm = transform.Find ("hitConfirmation1").gameObject;
		//hitConfirm.GetComponent<SpriteRenderer> ().enabled = false;
		cooldownBar = transform.Find ("repentCooldown").gameObject;
		orig_repentCooldown = repentCooldown;
		audio = GetComponent<AudioSource> ();
		maxHP = baseHP + 4;

		HP = baseHP;
		origColor = GetComponent<SpriteRenderer> ().color;
		resistGluttony = 0.25f;
		sprr = this.GetComponent<SpriteRenderer> ();
		player_anim = this.GetComponent<Animator> ();
		heavenLight = transform.Find ("tempSpotlight").gameObject;
		heavenLight.SetActive (false);
		canRepent = true;
		isAlive = true;

		//anim = this.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		player_anim.SetBool ("damaged", damaged);
		player_anim.SetBool ("isAttacking", isAttacking);
		player_anim.SetBool ("alive", isAlive);
		cooldownBar.transform.localScale = new Vector2 (cooldownBar.transform.localScale.x, repentCooldown / (orig_repentCooldown/2));
		//Player gluttony timer
        
		if (hitConfirm.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > 1) {
			hitConfirm.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (walkItOffTime > 0) {

			if (GetComponent<Control_Suport>().isMoving) {
				walkItOffTime -= Time.deltaTime;
			}
		} 
		else if(walkItOffTime < 0){
			if (walkItOff) {
				walkItOff = false;
				walkItOffTime = 0;
				if (HP > baseHP) {
					HP = baseHP;
				}
			}
		}

		//Player health
		if (HP >= 0) {
			HPBar.GetComponent<Image> ().sprite = gm.healthSprites [(int)HP];
		}
		if (HP > 0) {
			if (walkItOff) {

				GetComponent<Control_Suport> ().MoveHorisontal = GetComponent<Control_Suport> ().slowHorizontal;
				GetComponent<Control_Suport> ().MoveVertical = GetComponent<Control_Suport> ().slowVertical;
                SinIndector.SetActive(true);
			} 
			else {
				GetComponent<Control_Suport> ().MoveHorisontal = GetComponent<Control_Suport> ().orig_horiz_m;
				GetComponent<Control_Suport> ().MoveVertical = GetComponent<Control_Suport> ().orig_vert_m;
                SinIndector.SetActive(false);
            }
		}

        if(otherPlayer.isAlive == false && heavenLight.activeInHierarchy && healInRang)
        {
            otherPlayer.HP = otherPlayer.baseHP;
            otherPlayer.isAlive = true;
            gm.numPlayersAlive++;
        }
		if (HP < 0) {
			HP = 0;
			if (isAlive) {
				gm.numPlayersAlive--;
				isAlive = false;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
                

			}
            
		} 
		else {
			GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}
		if (Input.GetKeyDown (attack) || Input.GetKeyDown(attack2)) {
			if (!(isAttacking)) {
				isAttacking = true;
				audio.PlayOneShot (attackClip, 0.5f);
				StartCoroutine (FinishAttacking ());
			}
            
		}

		// Repent code
//		if (Input.GetKeyDown (repent) || Input.GetKeyDown(repent2)) {
//			if (canRepent) {
//				repentCooldown = orig_repentCooldown;
//				repenting = true;
//				StartCoroutine (Repent ());
//				canRepent = false;
//			}
//		}
//		if (repenting) {
//			DrawRepent ();
//		}
//		if (repentCooldown > 0) {
//			repentCooldown -= Time.deltaTime;
//			canRepent = false;
//		} else {
//			canRepent = true;
//		}

		if (gm.numPlayersReady == 2) {
			cpan.cameraPan = true;
			gm.numPlayersReady = 0;
			gm.player1Ready = false;
			gm.player2Ready = false;
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag.Equals("Enemy"))
        {
            if (col.gameObject.GetComponent<LittleFattyM>().isAlive)
            {
                if (!(isAttacking))
               {
                col.gameObject.GetComponent<LittleFattyM>().AnimatorSwitch("isMoving","isAttacking");
                if (GetComponent<Control_Suport>().Isknockback <= 0)
                {
                    HP--;
                }
                knockbackDerection = gameObject.transform.position - col.transform.position;
                knockbackDerection = knockbackDerection.normalized;

                enemyContact = true;
                TakeDamage();
                col.gameObject.GetComponent<LittleFattyM>().AnimatorSwitch("isAttacking", "isMoving");
                }

            }

        }

        if(col.name == otherPlayer.name )
        {
           
        }
        /*if (col.tag == "hitBox" && isAttacking)
        {

            if (col.gameObject.GetComponentInParent<LittleFattyM>())
            {

                LittleFattyM enemy = col.gameObject.GetComponentInParent<LittleFattyM>();
                float dmgTaken = enemy.takeDamage(1);
                wrath += dmgTaken;
                ConfirmHit();

                if (dmgTaken == 0)
                {
                    enemy.damaged = true;
                    enemy.knockbackDerection = col.gameObject.transform.position - transform.position;
                    enemy.knockbackDerection = enemy.knockbackDerection.normalized;
                    if (enemy.damaged == true && enemy.Isknockback <= 0)
                    {
                        enemy.Isknockback = enemy.knockBackTime;
                    }
                    enemy.damaged = false;
                    audio.PlayOneShot(enemy.damagedClip);
                }
                else
                {
                    gm.numEnemiesKilled++;
                }
                //isAttacking = false;
            }
        } */
       /* if (col.tag.Equals("Coin")) {
            Destroy(col.gameObject);
            greed += (0.5f - resistGreed);

            //John's code
            P2coinage.p2coins(1);
        }*/
        /* else if (col.tag.Equals("Cake")) {
             if (HP < maxHP) {
                 if (HP == baseHP) {
                     walkItOffTime = origWalkItOffTime;
                     walkItOff = true;
                 }
                 Destroy (col.gameObject);
                 gluttony += 0.5f; // subtract resistGluttony when implemented
                 HP += 1f;
             }
         }*/
        if (col.tag.Equals ("nextWave")) {
			if (name.Equals ("Player1")) {
				if (!(gm.player1Ready)) {
					gm.numPlayersReady++;
					gm.player1Ready = true;
				}
			}
			if (name.Equals ("Player2")) {
				if (!(gm.player2Ready)) {
					gm.numPlayersReady++;
					gm.player2Ready = true;
				}
			}

		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		enemyContact = false;

        if (col.tag == "Player")
        {
            healInRang = false;
        }
    }
		
	private IEnumerator Wait(int seconds)
	{
		Debug.Log ("Waiting...");
		yield return new WaitForSeconds (seconds);
		Debug.Log ("Done waiting");
	}

	void TakeDamage()
	{
		damaged = true;
		sprr.color = new Color32(255,0,0,255);
		StartCoroutine (Recover());
	}

	private IEnumerator Recover()
	{
		yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds (0.5f);
		damaged = false;
		sprr.color = origColor;

	}

	public void ConfirmHit()
	{
		hitConfirm.GetComponent<Animator> ().Play ("HitConfirmation", -1, 0f);
		Debug.Log ("Confirm hit");
	}

	public IEnumerator Repent()
	{
		yield return new WaitForEndOfFrame ();
		heavenLight.SetActive (true);
		GetComponent<Control_Suport> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, repentRadius, repentMask);
		for (int i = 0; i < colls.Length; i++) {
			if (colls [i].gameObject.tag.Equals ("Enemy")) {
				//colls [i].gameObject.GetComponent<LittleFattyM> ().repented = true;
				//wrath -= 0.5f;

			} 
			else if (colls [i].gameObject.CompareTag ("Player")) {
				if (colls [i].gameObject.GetComponent<PlayerActions> ().isAlive == false) {
					colls [i].gameObject.GetComponent<PlayerActions> ().HP = colls [i].gameObject.GetComponent<PlayerActions> ().baseHP;
					colls [i].gameObject.GetComponent<PlayerActions> ().isAlive = true;
				}
			}
		}
		yield return new WaitForSeconds(1);
		heavenLight.SetActive (false);
		yield return new WaitForSeconds(1);
		GetComponent<Control_Suport> ().enabled = true;
		repenting = false;
	}
	public IEnumerator FinishAttacking()
	{
		yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds (0.1f);
		isAttacking = false;

	}

	private IEnumerator wait(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
	}

	void OnDestroy()
	{
		
	}
//	private IEnumerator HideWeapon(float waitTime)
//	{
//		transform.GetChild (0).gameObject.SetActive (false);
//		yield return new WaitForSeconds (waitTime);
//		transform.GetChild (0).gameObject.SetActive (true);
//	}

	void DrawRepent()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, repentRadius);
	}
}
