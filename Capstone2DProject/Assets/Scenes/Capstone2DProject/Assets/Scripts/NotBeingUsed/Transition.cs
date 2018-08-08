using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

    [SerializeField] private int nextLevel;
   // public NextLevel good;
   // public NextLevel2 good2;
    private bool next;
    // Use this for initialization
    void Start () {

        next = false;
       // good = FindObjectOfType<NextLevel>();
      //  good2 = FindObjectOfType<NextLevel2>();
       
	}
	
	// Update is called once per frame
	void Update () {
		
        if (next)
          {

                SceneManager.LoadScene(nextLevel); 
              }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            next = true;
        }
    }
}
