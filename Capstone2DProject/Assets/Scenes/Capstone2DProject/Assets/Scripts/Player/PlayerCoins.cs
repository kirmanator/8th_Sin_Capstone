using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoins : MonoBehaviour {

	private Text thisText;

	// Use this for initialization
	void Start () {
		thisText = GetComponent<Text> ();
		SetCoinText();
	}

	public void SetCoinText(){
		thisText.text = "$: " + GameManager.NumCoins.ToString();
	}
}
