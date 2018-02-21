using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerActions player;
	public bool targetInRange;
	private LittleFattyM enemy;
    // Use this for initialization
    void Start()
    {
        player = GetComponentInParent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
		if (targetInRange) {
			if (player.isAttacking) {
				targetInRange = false;
				float dmgTaken = enemy.takeDamage (1);
				player.wrath += dmgTaken;
				player.ConfirmHit ();

				if (dmgTaken == 0 && enemy != null) {
					enemy.damaged = true;
					enemy.knockbackDerection = enemy.gameObject.transform.position - transform.position;
					enemy.knockbackDerection = enemy.knockbackDerection.normalized;
					if (enemy.damaged == true && enemy.Isknockback <= 0) {
						enemy.Isknockback = enemy.knockBackTime;
					}
					enemy.damaged = false;
					player.audio.PlayOneShot (enemy.damagedClip);
				}
			}
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag ("hitBox")) {
			if (other.GetComponentInParent<LittleFattyM> ()) {
				enemy = other.gameObject.GetComponentInParent<LittleFattyM> ();
				targetInRange = true;
			}
		}
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("hitBox")) {
			if (targetInRange) {
				targetInRange = false;
			}
		}
	}
}
