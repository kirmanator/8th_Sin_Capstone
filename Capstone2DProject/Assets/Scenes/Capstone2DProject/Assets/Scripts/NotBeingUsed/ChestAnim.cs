using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnim : MonoBehaviour {
    public int MonyT;
   // public P1coinige P1Coin;
   // public P2coinage p2coin;
	// Use this for initialization
	void Start () {
       // P1Coin = GetComponent<P1coinige>();
       // p2coin = GetComponent<P2coinage>();
	}
	
	// Update is called once per frame
	void Update () {
        MonyT = P1coinige.coins + P2coinage.coins2;

    }
}
