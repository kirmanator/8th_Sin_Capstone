using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public EventSystem ES;
	[SerializeField] private GameObject storedSelected;
	public GameObject instructScreen,instructUI, mainMenu;
	public KeyCode instructions, back;
	public bool instruct;
	public Image cursor;
	public float distanceFromButton;


	// Use this for initialization
	void Start () {
		storedSelected = ES.firstSelectedGameObject;
		if (SceneManager.GetActiveScene ().name.Equals ("MainMenu")) {
			instructScreen.SetActive (false);
			instructUI.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (ES.currentSelectedGameObject == null) {
			ES.SetSelectedGameObject (storedSelected);
		} 
		else {
			storedSelected = ES.currentSelectedGameObject;
			if (cursor != null) {
				cursor.rectTransform.position = 
			new Vector2 (storedSelected.GetComponent<RectTransform> ().position.x, storedSelected.GetComponent<RectTransform> ().position.y + distanceFromButton);
			}
		}

		/*if (Input.GetKeyDown (instructions)) {
			if (!(instruct)) {
				mainMenu.SetActive (false);
				instructScreen.SetActive (true);
				instructUI.SetActive (true);
				instruct = true;
			}
		}
		if (Input.GetKeyDown (back)) {
			if (instruct) {
				mainMenu.SetActive (true);
				instructScreen.SetActive (false);
				instructUI.SetActive (false);
				instruct = false;
			}
		}*/

	}
}
