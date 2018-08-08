using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	//variable depending on player
	[SerializeField] private string attack;
	[SerializeField] private string repent;
	//[SerializeField] private GameObject weapon;

	[SerializeField] private bool isAttacking;

	private PlayerActions player_actions;
	private PlayerMovement player_movement;
	private Collider2D collider;
    private float atckTimmer;

	[SerializeField] private TatoralCameraPan tutorialCam;

	public bool IsAttacking{ get { return isAttacking; } }

	void Start(){
		player_actions = transform.parent.gameObject.GetComponent<PlayerActions> ();
		player_movement = transform.parent.gameObject.GetComponent<PlayerMovement> ();
		collider = GetComponent<Collider2D> ();
		collider.enabled = false;
		//weapon = transform.parent.Find ("chickenLeg").gameObject;
	}

	void Update(){
		if (CanAttack ()) {
			GetInput ();
		}

        if(atckTimmer > 0)
        {
            atckTimmer = atckTimmer - Time.deltaTime;
        }
	}

	private void GetInput(){
		if (Input.GetButtonDown (attack) && atckTimmer <= 0) {
			ActivateWeapon ();
			player_actions.audio.PlayOneShot (AudioManager.PlayerAttack, 0.5f);
            atckTimmer = 0.50f;
		}
	}

	private bool CanAttack(){
		return(!(player_movement.KnockedBack) && !(player_movement.IsDashing) && !(isAttacking));
	}

	//Called as trigger in WeaponAttack animation
	public void ActivateWeapon(){
		isAttacking = true;
		collider.enabled = true;
		//Debug.Log ("Weapon Activated");
        //StartCoroutine(DeactivateFailSafe());
	}

	//Called as trigger in WeaponAttack animation
	public void DeactivateWeapon(){
		isAttacking = false;
		collider.enabled = false;
		//Debug.Log("Weapon Deactivated"	);
	}

	private void ConfirmHit(){
		Instantiate (GameManager.HitConfirm, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D col){

		//Debug.Log ("######" + col.tag);
		if (col.CompareTag ("Practice")) {
//			Debug.Log ("Contact with dummy");
//				Debug.Log ("Hitting dummy");
				col.gameObject.GetComponent<Animator> ().Play ("dummyhit", -1, 0f);
		}
		else if (col.CompareTag ("Enemy")) {
			//Debug.Log ("Enemy is hit");
			if (isAttacking) {
				if (col.GetComponent<Enemy> ()) {
					if (col.GetComponent<Enemy> ().canBeHit) {
						ConfirmHit ();
						col.GetComponent<Enemy> ().TakeDamage (1);
					}
				}
			}
		}

	}

    IEnumerator DeactivateFailSafe()
    {
        yield return new WaitForSeconds(0.5f);

        if(isAttacking)
        {
	        DeactivateWeapon();
	        yield return isAttacking;
        }
        
        
    }
}
