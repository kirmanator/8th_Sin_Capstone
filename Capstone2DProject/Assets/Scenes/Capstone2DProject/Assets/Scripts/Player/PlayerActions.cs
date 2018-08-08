	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {

	[SerializeField] private float greed,gluttony,sloth,wrath;
	[SerializeField] private int HP, maxHP, baseHP;
	[SerializeField] private float resistGreed, resistGluttony, resistSloth, resistWrath;

	[SerializeField] private string otherPlayerName;
	private Color32 origColor;
	[SerializeField] private float indicatorAlpha;
	[SerializeField] private float indicatorFadeRate;

	[SerializeField] private bool damaged, isAttacking, isMoving, repenting, canRepent, isAlive, isFat;

	[SerializeField] private bool canReviveSelf;

	private SpriteRenderer sprr;

	public AudioSource audio;
	private Animator player_anim;
	private new Rigidbody2D rigidbody;

	[SerializeField] private PlayerActions otherPlayer;
	private PlayerMovement player_move;
	private PlayerAttack player_attack;

	[SerializeField] private GameObject HPBar;
	//private GameObject cooldownBar;
	[SerializeField] private GameObject SinIndicator;
	private GameObject heavenLight;
	private Revive reviveRadius;
   
	public bool IsAlive{get{return isAlive;}}
	public float Greed{ get { return greed; } }
	public float Gluttony{ get { return gluttony; } }
	public float Sloth{ get { return sloth; } }
	public float Wrath{ get { return wrath; } }
	public int Health{ get { return HP; } set { HP = value; } }
	public int BaseHealth{ get { return baseHP; } }

	void Start () {
		#region Not Implemented Code
//		public float repentCooldown, orig_repentCooldown;
//		public LayerMask repentMask;
//		hitConfirm = transform.Find ("hitConfirmation1").gameObject;
//		hitConfirm.GetComponent<SpriteRenderer> ().enabled = false;
//		cooldownBar = transform.Find ("repentCooldown").gameObject;
//		orig_repentCooldown = repentCooldown;
		#endregion

		heavenLight = transform.Find ("tempSpotlight").gameObject;
		reviveRadius = transform.Find ("ReviveRadius").gameObject.GetComponent<Revive> ();
		SinIndicator = transform.Find ("Sin_icon").gameObject;
		SinIndicator.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		player_move = gameObject.GetComponent<PlayerMovement> ();
		player_anim = gameObject.GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody2D>();
		player_attack = transform.Find ("attackCollider").GetComponent<PlayerAttack> ();
		audio = GetComponent<AudioSource> ();
		sprr = GetComponent<SpriteRenderer> ();

		isAlive = true;

		maxHP = baseHP + 4;
		HP = baseHP;

		origColor = GetComponent<SpriteRenderer> ().color;
//		resistGluttony = 0.25f;
		if (indicatorFadeRate == 0f) {
			indicatorFadeRate = 2f;
		}
		indicatorFadeRate = Time.deltaTime / indicatorFadeRate;
		indicatorAlpha = indicatorFadeRate;

		heavenLight.SetActive (false);
		canRepent = true;

	}
	
	// Update is called once per frame
	void Update () {
		
		player_anim.SetBool ("isDamaged", player_move.KnockedBack);
		player_anim.SetBool ("isAttacking", player_attack.IsAttacking);
		player_anim.SetBool ("alive", isAlive);
		player_anim.SetBool ("isMoving", player_move.IsMoving ());
		player_anim.SetFloat ("isFat", player_move.WalkItOff == false ? 0f : 1f);

		if (!isAlive) {
			return;
		}

		UpdateHPBar();
		UpdateIsAlive ();

		if(otherPlayer.isAlive == false && heavenLight.activeInHierarchy && reviveRadius.PlayerInRange)
        {
            otherPlayer.HP = otherPlayer.baseHP;
            otherPlayer.isAlive = true;
			GameManager.NumPlayersAlive++;
        }
		if (player_move.WalkItOff) {
			if (indicatorAlpha < 1f) {
				indicatorAlpha += indicatorFadeRate;
				SinIndicator.GetComponent<SpriteRenderer> ().color += new Color (0f, 0f, 0f, indicatorAlpha);
			}
		}
		else {
			if (indicatorAlpha > 0f) {
				indicatorAlpha -= indicatorFadeRate;
				SinIndicator.GetComponent<SpriteRenderer> ().color += new Color (0f, 0f, 0f, indicatorAlpha);
			}
		}
	}

	private void GetInput(){

	}

	private void UpdateHPBar(){
		HPBar.GetComponent<Image> ().sprite = GameManager.HealthSprites [(int)HP];
	}

	private void UpdateIsAlive(){
		if (HP <= 0) {
			if (isAlive) {
				HP = 0;
				GameManager.NumPlayersAlive--;
				isAlive = false;
				rigidbody.velocity = new Vector2 (0, 0);
				rigidbody.bodyType = RigidbodyType2D.Static;
				return;
			}

		} 
		else {
			rigidbody.bodyType = RigidbodyType2D.Dynamic;
		}
	}

	public void SetIndicator(int direction){
		indicatorAlpha = (direction == 0 ? 1f - indicatorFadeRate : (direction == 1 ? indicatorFadeRate : 0f));
	}

	public void ActivateWeapon(){
		player_attack.ActivateWeapon ();
	}
	public void DeactivateWeapon(){
		player_attack.DeactivateWeapon ();
	}

	#region Old Code
//	void OnTriggerEnter2D(Collider2D col)
//	{
//        if (col.tag.Equals("Enemy"))
//        {
//            if (col.gameObject.GetComponent<LittleFattyM>().isAlive)
//            {
//                if (!(isAttacking))
//               {
//                col.gameObject.GetComponent<LittleFattyM>().AnimatorSwitch("isMoving","isAttacking");
//                if (GetComponent<Control_Suport>().Isknockback <= 0)
//                {
//                    HP--;
//                }
//                knockbackDerection = gameObject.transform.position - col.transform.position;
//                knockbackDerection = knockbackDerection.normalized;
//
//                enemyContact = true;
//                TakeDamage();
//                col.gameObject.GetComponent<LittleFattyM>().AnimatorSwitch("isAttacking", "isMoving");
//                }
//
//            }
//
//        }
//		#region Move to HealthRadius object
//        if(col.name == otherPlayerName)
//        {
//           // healInRang = true;
//        }
//		#endregion
//        /*if (col.tag == "hitBox" && isAttacking)
//        {
//
//            if (col.gameObject.GetComponentInParent<LittleFattyM>())
//            {
//
//                LittleFattyM enemy = col.gameObject.GetComponentInParent<LittleFattyM>();
//                float dmgTaken = enemy.takeDamage(1);
//                wrath += dmgTaken;
//                ConfirmHit();
//
//                if (dmgTaken == 0)
//                {
//                    enemy.damaged = true;
//                    enemy.knockbackDerection = col.gameObject.transform.position - transform.position;
//                    enemy.knockbackDerection = enemy.knockbackDerection.normalized;
//                    if (enemy.damaged == true && enemy.Isknockback <= 0)
//                    {
//                        enemy.Isknockback = enemy.knockBackTime;
//                    }
//                    enemy.damaged = false;
//                    audio.PlayOneShot(enemy.damagedClip);
//                }
//                else
//                {
//                    gm.numEnemiesKilled++;
//                }
//                //isAttacking = false;
//            }
//        } */
//       /* if (col.tag.Equals("Coin")) {
//            Destroy(col.gameObject);
//            greed += (0.5f - resistGreed);
//
//            //John's code
//            P2coinage.p2coins(1);
//        }*/
//        /* else if (col.tag.Equals("Cake")) {
//             if (HP < maxHP) {
//                 if (HP == baseHP) {
//                     walkItOffTime = origWalkItOffTime;
//                     walkItOff = true;
//                 }
//                 Destroy (col.gameObject);
//                 gluttony += 0.5f; // subtract resistGluttony when implemented
//                 HP += 1f;
//             }
//         }*/
//        if (col.tag.Equals ("nextWave")) {
//			if (name.Equals ("Player1")) {
//		if (!(GameManager.Instance.player1Ready)) {
//			GameManager.Instance.numPlayersReady++;
//			GameManager.Instance.player1Ready = true;
//				}
//			}
//			if (name.Equals ("Player2")) {
//		if (!(GameManager.Instance.player2Ready)) {
//			GameManager.Instance.numPlayersReady++;
//			GameManager.Instance.player2Ready = true;
//				}
//			}
//
//		}
//	}
//
//	void OnTriggerExit2D(Collider2D col)
//	{
//
//        if (col.name == otherPlayerName)
//        {
//           // healInRang = false;
//        }
//    }
	#endregion
		
	private IEnumerator Wait(int seconds)
	{
		Debug.Log ("Waiting...");
		yield return new WaitForSeconds (seconds);
		Debug.Log ("Done waiting");
	}

	public void TakeDamage(int amount)
	{
		HP = Mathf.Max(0, HP - amount);
		damaged = true;
		sprr.color = new Color(1f,0f,0f);
		StartCoroutine (Recover());
	}

	public void AddHealth(int amount){
		HP = Mathf.Min(maxHP, HP + amount);
		if (HP > baseHP) {
			player_move.StartWalkitOff();
		}
	}

	private IEnumerator Recover()
	{
		yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds (0.5f);
		damaged = false;
		sprr.color = origColor;

	}
	#region Not Implemented Code
//		cooldownBar.transform.localScale = new Vector2 (cooldownBar.transform.localScale.x, repentCooldown / (orig_repentCooldown/2));
//		Player gluttony timer

//		if (hitConfirm.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > 1) {
//			hitConfirm.GetComponent<SpriteRenderer> ().enabled = false;
//		}
//	public void ConfirmHit()
//	{
//		hitConfirm.GetComponent<Animator> ().Play ("HitConfirmation", -1, 0f);
//		Debug.Log ("Confirm hit");
//	}

//	public IEnumerator Repent()
//	{
//		yield return new WaitForEndOfFrame ();
//		heavenLight.SetActive (true);
//		GetComponent<Control_Suport> ().enabled = false;
//		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
//		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, repentRadius, repentMask);
//		for (int i = 0; i < colls.Length; i++) {
//			if (colls [i].gameObject.tag.Equals ("Enemy")) {
//				//colls [i].gameObject.GetComponent<LittleFattyM> ().repented = true;
//				//wrath -= 0.5f;
//
//			} 
//			else if (colls [i].gameObject.CompareTag ("Player")) {
//				if (colls [i].gameObject.GetComponent<PlayerActions> ().isAlive == false) {
//					colls [i].gameObject.GetComponent<PlayerActions> ().HP = colls [i].gameObject.GetComponent<PlayerActions> ().baseHP;
//					colls [i].gameObject.GetComponent<PlayerActions> ().isAlive = true;
//				}
//			}
//		}
//		yield return new WaitForSeconds(1);
//		heavenLight.SetActive (false);
//		yield return new WaitForSeconds(1);
//		GetComponent<Control_Suport> ().enabled = true;
//		repenting = false;
//	}
	#endregion

//	public IEnumerator FinishAttacking()
//	{
//		yield return new WaitForEndOfFrame ();
//		yield return new WaitForSeconds (0.1f);
//		isAttacking = false;
//
//	}

	private IEnumerator wait(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
	}
}
