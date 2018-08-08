using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P1coinige : MonoBehaviour {

    // Use this for initialization
    public static int coins;
   // public Text p1mony;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coins < 0)
        {
            coins = 0; 
        }
        //p1mony.text = "P1 $: " + coins; 


    }

    public static void p1coins(int p1coins)
    {
        coins += p1coins; 
    }

   
}

