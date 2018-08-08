using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Suport : MonoBehaviour {

	private PlayerMovement player_move;

    void Start()
    {
		player_move = GetComponent<PlayerMovement> ();
    }


    void Update()
	{
		GetInput ();
	}
//		if (GetComponent<PlayerActions> ().isAlive == true) {
			// MoveVelocityH = 0f; // prevents player from sliding 
			// MoveVelocityV = 0f; // prevents player from sliding 
//            if(Isknockback <= 0)
//            {
//                player_anim.SetBool ("isMoving", isMoving);
//            }
			
//			player_anim.SetBool ("isDashing", isDashing);

//			if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
//				isFacingLeft = false;
//			} else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
//				isFacingLeft = true;
//			}


			
//
//			MoveVelocityH = MoveHorisontal * Input.GetAxisRaw (playerH);
//
//			MoveVelocityV = MoveVertical * Input.GetAxisRaw (playerV);

//            if(dashAgean > 0)
//            {
//                dashAgean = dashAgean - Time.deltaTime;
//            }
//
//            if(dashtime > 0)
//            {
//                dashtime = dashtime - Time.deltaTime;
//            }
//            else if(dashtime <= 0)
//            {
//                isDashing = false;
//            }

//			if (Isknockback <= 0) {
//                if(!isDashing)
//                {
//                GetComponent<Rigidbody2D> ().velocity = new Vector2 (MoveVelocityH, GetComponent<Rigidbody2D> ().velocity.y); // sets velosity for horsontal movment  
//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, MoveVelocityV); // sets velosity for veritacl movment4
//                }
//
//                if (isDashing)
//                {
//                    GetComponent<Rigidbody2D>().velocity = new Vector2(DashMoveVelocityH * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment  
//                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, DashMoveVelocityV * 3); // sets velosity for veritacl movment4
//                }
//                
//                if (isFacingLeft)
//                {
//                    transform.localScale = new Vector2(-1, 1);
//
//                    if (isDashing && DashMoveVelocityH <= 0 && Mathf.Abs (DashMoveVelocityV) <= 0 && PlayerA.walkItOff == false)
//                    {
//                        GetComponent<Rigidbody2D>().velocity = new Vector2(-MoveHorisontal * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment  
//                    }
//                }
//                else
//                {
//                    transform.localScale = new Vector2(1, 1);
//
//                    if (isDashing && DashMoveVelocityH <= 0 && Mathf.Abs(DashMoveVelocityV) <= 0 && PlayerA.walkItOff == false)
//                    {
//                        GetComponent<Rigidbody2D>().velocity = new Vector2(MoveHorisontal * 3, GetComponent<Rigidbody2D>().velocity.y); // sets velosity for horsontal movment                                                                                                                
//                    }
//                }
//            } 
//			else {
//				GetComponent<Rigidbody2D> ().velocity = new Vector2 (PlayerA.knockbackDerection.x * knockBackLenth, PlayerA.knockbackDerection.y * knockBackLenth);
//			}
//
//			if (Isknockback > 0) {
//				Isknockback = Isknockback - Time.deltaTime;
//			}
//			if (PlayerA.damaged == true && Isknockback <= 0) {
//				Isknockback = knockBackTime;
//			}

//			if (MoveVelocityH == 0 && MoveVelocityH == 0) {
//				isMoving = false;
//			} else {
//				isMoving = true;
//			}

//			if (Input.GetKeyDown (repent)) {
//				StartCoroutine (GetComponent<PlayerActions> ().Repent ());
//			}

//            if(Input.GetKeyDown(dash) && dashAgean <= 0 && PlayerA.walkItOff == false)
//            {
//                DashMoveVelocityH = MoveHorisontal * Input.GetAxisRaw(playerH);
//                DashMoveVelocityV = MoveVertical * Input.GetAxisRaw(playerV);
//                dashAgean = setDashAgean;
//                dashtime = setdashtime;
//                isDashing = true;
//            }
//		}
//    }

	private void GetInput(){
		player_move.MoveVelocityH = player_move.MoveHorizontal * Input.GetAxisRaw (player_move.PlayerH);
		player_move.MoveVelocityV = player_move.MoveVertical * Input.GetAxisRaw (player_move.PlayerV);
	}
}
