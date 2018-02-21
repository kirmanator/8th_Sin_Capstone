using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour
{
    public Button reset;
    public Button Quit;
    public int selected;
    public GameMaster master;
    public float moveslecte;
    public float moveslected;
    // public bool timeskip;
    // public float numtimeskip;
    // public float MaxNumTimeskip;
    // Use this for initialization
    void Start()
    {
        master = FindObjectOfType<GameMaster>();
        reset = GetComponent<Button>();
        selected = 1;
    }

    // Update is called once per frame
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

