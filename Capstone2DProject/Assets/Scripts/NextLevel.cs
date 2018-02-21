using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class NextLevel : MonoBehaviour
{

    public bool ExitLevel;
	public KeyCode escapekey;
    //public string nextLevel;

    // Use this for initialization
    void Start()
    {
        ExitLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKey(escapekey))
		{
			Application.Quit();
			Debug.Log ("I have quit");
		}

    }

   
}
