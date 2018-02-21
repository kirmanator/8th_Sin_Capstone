using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

    public string nextLevel;
    public NextLevel good;
    public NextLevel2 good2;
    private bool next;
    // Use this for initialization
    void Start () {

        next = false;
        good = FindObjectOfType<NextLevel>();
        good2 = FindObjectOfType<NextLevel2>();
       
	}
	
	// Update is called once per frame
	void Update () {
		if (good.ExitLevel == true && good2.ExitLevel2 == true)
        {
            next = true;
 
        }
        if (next)
          {

                SceneManager.LoadScene(nextLevel); 
              }
    }
}
