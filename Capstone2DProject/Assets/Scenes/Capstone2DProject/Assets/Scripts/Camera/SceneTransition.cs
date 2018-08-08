using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Button reset;
    public Button Quit;
    public float moveslected;
    private GameManager manager;
    public GameObject hilightedreset;
    // public bool timeskip;
    // public float numtimeskip;
    // public float MaxNumTimeskip;

    void Start()
    {
       // reset = GetComponent<Button>();
        
        manager = GetComponent<GameManager>();
    }
		
    void Update()
    {
      
 	}

    public void levelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToScene(int scene)
    {
		if (scene == 420) {
			Application.Quit ();
		} 
		else {
			SceneManager.LoadScene (scene);
		}
       
    }
}

