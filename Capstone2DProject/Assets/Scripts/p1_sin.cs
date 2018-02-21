using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p1_sin : MonoBehaviour {

    public GameObject player;
	public float glutton;

    public float sincap;
	float colorVal;
	public Color imgC;

    void Update()
    {
		glutton = player.GetComponent<PlayerActions>().gluttony;
		//subtract 0.15 from 1 for every 1 additional sin
		colorVal = 1 - (0.3f * glutton);
		GetComponent<Image> ().color = new Color(colorVal, colorVal, colorVal, 1);
		imgC = GetComponent<Image> ().color;
    }
}
