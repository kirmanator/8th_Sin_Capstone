using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins_Total : MonoBehaviour {
    public int MonyT;
    public Text Total;
        
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MonyT = P1coinige.coins + P2coinage.coins2;
        Total.text = "$: " + MonyT;
    }
}
