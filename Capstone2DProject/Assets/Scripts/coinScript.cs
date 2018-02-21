using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{

    // Use this for initialization
    public int coin;
    public GameObject P1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1" )
        {
            Debug.Log("other.tag is P1"); 
            P1coinige.p1coins(coin);
            Destroy(gameObject);
        }
        else if (other.tag == "Player2")
        {
            Debug.Log("other.tag is P2");
            P2coinage.p2coins(coin);
            Destroy(gameObject);
        }
       
   
    }


}
