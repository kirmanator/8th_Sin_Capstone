using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel2 : MonoBehaviour {

    public bool ExitLevel2;
    //public string nextLevel;

    // Use this for initialization
    void Start()
    {
        ExitLevel2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  if(ExitLevel && ExitLevel2)
        //  {

        //    SceneManager.LoadScene(nextLevel); 
        //  }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "P2exit")
        {
            ExitLevel2 = true;
        }


    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.name == "P2exit")
        {
            ExitLevel2 = false;
        }


    }
}

