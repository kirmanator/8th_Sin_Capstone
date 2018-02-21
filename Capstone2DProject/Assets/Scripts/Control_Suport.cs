using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Suport : MonoBehaviour {

    public float MoveHorisontal, slowHorizontal; // move speed for left and right 
    public float MoveVertical, slowVertical; // move speed for up and down 
	public float orig_horiz_m, orig_vert_m;
    public float MoveVelocityH;
    public float MoveVelocityV;
    public float DashMoveVelocityH;
    public float DashMoveVelocityV;
    public string playerH;
    public string playerV;
    public bool isFacingLeft;
    private Animator player_anim;
    public bool isMoving;
    public KeyCode attack;
    public KeyCode repent;
    public KeyCode dash;
    public float Isknockback;
    public float knockBackTime;
    public Vector2 knockbackDistans;
    public float knockBackLenth;
    public PlayerActions PlayerA;
    private bool isDashing;
    private float dashtime;
    public float setdashtime;
    public float setDashAgean;
    private float dashAgean;
    // Use this for initialization
    void Start()
    {
        isFacingLeft = false;
        player_anim = gameObject.GetComponent<Animator>();
		//slowHorizontal = MoveHorisontal / 2;
		//slowVertical = MoveVertical / 2;
		orig_horiz_m = MoveHorisontal;
		orig_vert_m = MoveVertical;
        dashtime = setdashtime;
       // dashAgean = setDashAgean;
    }

    // Update is called once per frame
    void Update()
    {
		if (GetComponent<PlayerActions> ().isAlive == true) {
			// MoveVelocityH = 0f; // prevents player from sliding 
			// MoveVelocityV = 0f; // prevents player from sliding 
            if(Isknockback <= 0)
            {
                player_anim.SetBool ("isMoving", isMoving);
            }
			
			player_anim.SetBool ("isDashing", isDashing);

			if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
				isFacingLeft = false;
			} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
				isFacingLeft = true;
			}


			

			MoveVelocityH = MoveHorisontal * Input.GetAxisRaw (playerH);

			MoveVelocityV = MoveVertical * Input.GetAxisRaw (playerV);

            if(dashAgean > 0)
            {
                dashAgean = dashAgean - Time.deltaTime;
            }

            if(dashtime > 0)
            {
                dashtime = dashtime - Time.deltaTime;
            }
            else if(dashtime <= 0)
            {
                isDashing = false;
            }

			if (Isknockback <= 0) {
                if(!isDashing)
                {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (MoveVelocityH, GetComponent<Rigidbody2D> ().velocity.y); // sets velosity for horsontal movment  
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, MoveVelocityV); // sets velosity for veritacl movment4
                }

                if (isDashing)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(DashMoveVelocityH * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment  
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, DashMoveVelocityV * 3); // sets velosity for veritacl movment4
                }
                
                if (isFacingLeft)
                {
                    transform.localScale = new Vector2(-1, 1);

                    if (isDashing && DashMoveVelocityH <= 0 && Mathf.Abs (DashMoveVelocityV) <= 0 && PlayerA.walkItOff == false)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-MoveHorisontal * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment  
                    }
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);

                    if (isDashing && DashMoveVelocityH <= 0 && Mathf.Abs(DashMoveVelocityV) <= 0 && PlayerA.walkItOff == false)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(MoveHorisontal * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment                                                                                                                
                    }
                }
            } else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (PlayerA.knockbackDerection.x * knockBackLenth, PlayerA.knockbackDerection.y * knockBackLenth);
			}

			if (Isknockback > 0) {
				Isknockback = Isknockback - Time.deltaTime;
			}
			if (PlayerA.damaged == true && Isknockback <= 0) {
				Isknockback = knockBackTime;
			}

			if (MoveVelocityH == 0 && MoveVelocityH == 0) {
				isMoving = false;
			} else {
				isMoving = true;
			}

			if (Input.GetKeyDown (repent)) {
				StartCoroutine (GetComponent<PlayerActions> ().Repent ());
			}

            if(Input.GetKeyDown(dash) && dashAgean <= 0 && PlayerA.walkItOff == false)
            {
                DashMoveVelocityH = MoveHorisontal * Input.GetAxisRaw(playerH);
                DashMoveVelocityV = MoveVertical * Input.GetAxisRaw(playerV);
                dashAgean = setDashAgean;
                dashtime = setdashtime;
                isDashing = true;
            }
		}
    }
}
