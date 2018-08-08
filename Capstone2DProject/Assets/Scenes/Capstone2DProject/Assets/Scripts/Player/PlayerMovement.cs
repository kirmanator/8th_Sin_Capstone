using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    #region Variables
    [SerializeField] private float moveHorizontal, slowHorizontal; // move speed for left and right 
    [SerializeField] private float moveVertical, slowVertical; // move speed for up and down 
    [SerializeField] private float orig_horiz_m, orig_vert_m;
    [SerializeField] private float moveVelocityH;
    [SerializeField] private float moveVelocityV;
    [SerializeField] private float dashVelocityH;
    [SerializeField] private float dashVelocityV;
    [SerializeField] private float saveVelocityH;
    [SerializeField] private float saveVelocityV;

    [SerializeField] private bool knockedBack;
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float totalKnockbackTime;
    [SerializeField] private float knockbackSpeed;

    [SerializeField] private bool isFacingLeft;

    [SerializeField] private bool isDashing;

    [SerializeField] private float dashTime;
    [SerializeField] private float totalDashTime;
    [SerializeField] private float dashRefresh;
    [SerializeField] private float totalDashRefresh;
    [SerializeField] private bool refreshDash;

    [SerializeField] private bool walkItOff;
    [SerializeField] private float walkItOffTime, totalWalkItOffTime;

    private Rigidbody2D rb2d;
    private PlayerActions player_actions;
    private PlayerAttack player_attack;
   
    [SerializeField] private string playerH;
    [SerializeField] private string playerV;
    [SerializeField] private string dash;

	[SerializeField] private float scaleX, scaleY;
    
    public bool frendlyFire;
    #endregion

    //Setters/Getters
    #region Setters/Getters


    public bool KnockedBack { get { return knockedBack; } }
    public bool IsDashing { get { return isDashing; } }
    public bool WalkItOff { get { return walkItOff; } set { walkItOff = value; } }
    public float MoveVelocityH { get { return moveVelocityH; } set { moveVelocityH = value; } }
    public float MoveVelocityV { get { return moveVelocityV; } set { moveVelocityV = value; } }
    public float MoveHorizontal { get { return moveHorizontal; } }
    public float MoveVertical { get { return moveVertical; } }
    public string PlayerH { get { return playerH; } }
    public string PlayerV { get { return playerV; } }
    #endregion

    void Start()
    {

        orig_horiz_m = moveHorizontal;
        orig_vert_m = moveVertical;
        dashTime = totalDashTime;
        saveVelocityH = orig_horiz_m;
        saveVelocityV = orig_vert_m;
        rb2d = GetComponent<Rigidbody2D>();
        player_actions = gameObject.GetComponent<PlayerActions>();
        player_attack = transform.Find("attackCollider").gameObject.GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (player_actions.IsAlive && !(GameManager.IsPaused))
        {
            UpdateMovement();
            UpdateDirection();
            UpdateDashRefresh();
            UpdateWalkItOff();
            UpdateKnockback();
            GetInput();

        }
        else
        {
            return;
        }
    }
    //Inputs and Updates
    #region Input and Updates
    private void GetInput()
    {
        moveVelocityH = moveHorizontal * Input.GetAxisRaw(playerH);
        moveVelocityV = moveVertical * Input.GetAxisRaw(playerV);
        if (Input.GetButtonDown(dash) && CanDash())
        {
			player_actions.audio.PlayOneShot (AudioManager.PlayerDash);
            GetComponent<Animator>().SetTrigger("dash");
            isDashing = true;
            dashRefresh = totalDashRefresh;
        }
    }

    private void UpdateDirection()
    {
        if (rb2d.velocity.x > 0)
        {
            isFacingLeft = false;

        }
        else if (rb2d.velocity.x < 0)
        {
            isFacingLeft = true;
        }
        if (isFacingLeft)
        {
			transform.localScale = new Vector2(-6, 6);
        }
        else
        {
			transform.localScale = new Vector2(6, 6);
        }
    }

    private void UpdateMovement()
    {
        if (!isDashing)
        {
            Walk();
        }
        else
        {
            Dash();
        }
    }

    private void UpdateDashRefresh()
    {
        if (refreshDash)
        {
            if (dashRefresh <= 0)
            {
                dashRefresh = 0;
                refreshDash = false;
                return;
            }
            dashRefresh -= Time.deltaTime;
        }
    }

    public void StartWalkitOff()
    {
        walkItOff = true;
        walkItOffTime = totalWalkItOffTime;
		player_actions.SetIndicator (1);
    }

    private void UpdateWalkItOff()
    {
        if (walkItOff)
        {
            if (walkItOffTime <= 0)
            {
                if (player_actions.Health > player_actions.BaseHealth)
                {
                    player_actions.Health = player_actions.BaseHealth;
                }
                walkItOffTime = 0;
                walkItOff = false;
                return;
            }
			if (IsMoving ()) {
				walkItOffTime -= Time.deltaTime;
			}
        }
    }
    #endregion

    //Dash
    #region Dash
    private bool CanDash()
    {
        return ((!(isDashing)) && walkItOff == false && knockedBack == false && !(refreshDash));
    }

    private void Dash()
    {
        if (dashTime < 0)
        {
            dashTime = 0;
            StartDashRefresh();
            return;
        }
        dashVelocityH = saveVelocityH * 3;
        dashVelocityV = saveVelocityV * 3;
        rb2d.velocity = new Vector2(dashVelocityH, rb2d.velocity.y); // sets velocity for horizontal movment  
        rb2d.velocity = new Vector2(rb2d.velocity.x, dashVelocityV); // sets velocity for vertical movement
        dashTime -= Time.deltaTime;
    }

    private void StartDashRefresh()
    {
        rb2d.velocity = Vector2.zero;
        isDashing = false;
        refreshDash = true;
        dashTime = totalDashTime;
    }
    #endregion

    //Knockback
    #region Knockback
    public void SetupKnockback(Transform enemy)
    {
        knockbackDirection = transform.position - enemy.position;
        knockbackDirection = knockbackDirection.normalized;
        knockbackTime = totalKnockbackTime;
        knockedBack = true;
		player_actions.audio.PlayOneShot (AudioManager.PlayerDamaged, 1.5f);
    }

    private void UpdateKnockback()
    {
        if (knockedBack)
        {
            if (knockbackTime < 0)
            {
                knockbackTime = 0;
                knockedBack = false;
                return;
            }
            rb2d.velocity = new Vector2(knockbackDirection.x * knockbackSpeed, knockbackDirection.y * knockbackSpeed);
            knockbackTime -= Time.deltaTime;
        }

    }
    #endregion

    //Movement
    #region Movement
    private void Walk()
    {
        if (walkItOff)
        {
            moveHorizontal = slowHorizontal;
            moveVertical = slowVertical;

        }
        else
        {
            moveHorizontal = orig_horiz_m;
            moveVertical = orig_vert_m;
        }
        rb2d.velocity = new Vector2(moveVelocityH, rb2d.velocity.y); // sets velocity for horizontal movment  
        rb2d.velocity = new Vector2(rb2d.velocity.x, moveVelocityV); // sets velocity for vertical movement
    }

    public bool IsMoving()
    {
        if (Mathf.Abs(moveVelocityH) + Mathf.Abs(moveVelocityV) != 0)
        {
            saveVelocityH = moveVelocityH;
            saveVelocityV = moveVelocityV;
            return true;
        }
        return false;
    }
    #endregion

    void OnTriggerEnter2D(Collider2D col)
    {
		if (player_actions.IsAlive) {
			if (col.CompareTag ("Enemy")) {
				if (!(player_attack.IsAttacking)) {
					if (col.GetComponent<GroundEnemy> ()) {
						if (!(col.GetComponent<GroundEnemy> ().EState == EnemyState.damaged) && col.GetComponent<GroundEnemy> ().IsAlive) {
							GetComponent<Animator> ().SetTrigger ("damaged");
							player_actions.TakeDamage (col.GetComponent<GroundEnemy> ().AttackPoints);

							if (isDashing) {
								StartDashRefresh ();
							}
							SetupKnockback (col.transform);
							knockedBack = true;
						}
					}
				}
			}
			else if (col.CompareTag ("Food")) {
				if (col.GetComponent<Collectible> ().CanPickup) {
					player_actions.AddHealth (col.GetComponent<Collectible> ().HealthUp);
					Destroy (col.gameObject);
				}
			}
			else if (col.CompareTag ("Coin")) {
				if (col.GetComponent<Collectible> ().CanPickup) {
					GameManager.NumCoins++;
					Destroy (col.gameObject);
					GameManager.CoinText.gameObject.GetComponent<PlayerCoins> ().SetCoinText ();
				}
			}
			else if (col.CompareTag ("nextWave")) {
				if (name.Equals ("Player1")) {
					if (!(GameManager.Player1Ready)) {
						GameManager.Player1Ready = true;
						GameManager.NumPlayersReady++;
					}
				}
				else if (name.Equals ("Player2")) {
					if (!(GameManager.Player2Ready)) {
						GameManager.Player2Ready = true;
						GameManager.NumPlayersReady++;
					}
				}
			}
			else if (col.CompareTag ("Projectile")) {
				if (col.GetComponent<Projectile> ().IsMoving) {
					player_actions.TakeDamage (col.GetComponent<Projectile> ().AttackPoints);
					SetupKnockback (col.transform);
				}
			}
			else if (col.CompareTag ("Weapon")) {
				if (frendlyFire) {
					player_actions.TakeDamage (1);
					SetupKnockback (col.transform);
				}
			}
		}
//		else if (col.CompareTag ("Practice")) {
//			Debug.Log ("Hit dummy");
//			col.GetComponent<Animator> ().Play ("dummyhit", -1, 0f);
//		}
    }

}
