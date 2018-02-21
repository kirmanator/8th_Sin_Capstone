using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathRadus : MonoBehaviour {

    public PlayerActions player;
    public PlayerActions otherPlayer;
    public string otherPlayerName;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "HelthSpawn")
        {
            player.healInRang = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HelthSpawn")
        {
            player.healInRang = false;
        }
    }
}
