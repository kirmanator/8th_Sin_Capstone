using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droped_pickup : MonoBehaviour
{

   /* private bool canPickup;
    private float stillDroping;
    public float stillDropGoal;
    public PlayerActions player;
    public Vector3 dropedArea;
    public Vector3 dropedArea2;
    
    // Use this for initialization
    void Start()
    {

        
        if (tag == "Cake")
        {
            transform.position = dropedArea;
        }


        if (tag == "Cake")
        {
            transform.position = dropedArea2;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position != dropedArea)
        {
            transform.position = dropedArea;
        }
        
        if (stillDroping < stillDropGoal)
        {
            stillDroping += Time.deltaTime;
        }

        if (stillDropGoal <= stillDroping)
        {
            canPickup = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && canPickup)
        {
            player = other.GetComponent<PlayerActions>();
            if(tag == "Cake")
            {
                if (player.HP < player.maxHP)
                {
                    if (player.HP == player.baseHP)
                    {
                        player.walkItOffTime = player.origWalkItOffTime;
                        player.walkItOff = true;
                    }
                    Destroy(gameObject);
                    player.gluttony += 0.5f; // subtract resistGluttony when implemented
                    player.HP += 1f;
                }
            }

            if (tag == "Coin")
            {
                Destroy(gameObject);
                player.greed += (0.5f - player.resistGreed);

                //John's code
                P2coinage.p2coins(1);
            }

        }

       
    }*/
}    
