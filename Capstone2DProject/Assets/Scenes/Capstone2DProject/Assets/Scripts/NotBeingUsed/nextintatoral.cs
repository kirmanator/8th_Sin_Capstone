using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextintatoral : MonoBehaviour {
    private TatoralCameraPan tpan;
    public bool active;
	// Use this for initialization
	void Start () {
        tpan = FindObjectOfType<TatoralCameraPan>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player1")
        {
            tpan.p1 = true;
        }

        if (other.name == "Player2")
        {
            tpan.p2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player1")
        {
            tpan.p1 = false;
        }

        if (other.name == "Player2")
        {
            tpan.p2 = false;
        }
    }
}
