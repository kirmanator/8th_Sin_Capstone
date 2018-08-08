using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P2coinage : MonoBehaviour {

    // Use this for initialization
    public static int coins2;
   // public Text p2mony;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coins2 < 0)
        {
            coins2 = 0;
        }
     //   p2mony.text = "P2 $: " + coins2;


    }

    public static void p2coins(int p2coins)
    {
        coins2 += p2coins;
    }
}
