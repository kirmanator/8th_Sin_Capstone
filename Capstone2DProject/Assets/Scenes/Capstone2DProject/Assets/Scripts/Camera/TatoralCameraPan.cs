using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TatoralCameraPan : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private Transform player1, player2;
    [SerializeField] private GameObject nextWave;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject billbord;
    [SerializeField] private GameObject nextWaveSign;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject foodPre;
    [SerializeField] private GameObject dummy;
    [SerializeField] private float xDistance; // = 40
     

    private Vector3 destination;
    private float minX;
    private float centerOfPlayers;
    private Vector3 playerPos;
    private bool moveable;
    private bool waveOver;
    public bool p1;
    public bool p2;
    public int curentTataralStage = 0;
    private bool stageClear;
	private bool moveOn;
    public string[] tText;
    public Text tTextBox;
    private bool spawnAThing;
    // Use this for initialization
    void Start()
    {
       // billbord = transform.Find("box").gameObject;
        spawner = transform.Find("spawnTatralShit").gameObject;
        nextLevel = transform.Find("nextLevel").gameObject;
        nextWave = transform.Find("NextWave").gameObject;
        nextWave.GetComponent<BoxCollider2D>().enabled = false;
        nextLevel.GetComponent<BoxCollider2D>().enabled = false;
        nextWaveSign = GameObject.Find("NextWaveSign");
        spawnAThing = false;
        SetNewPosition();
        billbord.GetComponent<Animator>().Play("signGoUp", -1, 0f);
       
    }

    // Update is called once per frame
    void Update()
    {
        /*  SetWaveOver();
          if (waveOver)
          {
              ActivateNextWave();
          }
          if (moveable)
          {
              if (IsMovingForward())
              {
                  UpdatePosition();
              }
          }*/
        tTextBox.text = tText[curentTataralStage];

        if(curentTataralStage == 0)
        {
            if(player1.GetComponent<Rigidbody2D>().velocity.x > 0)
            {

                if (!stageClear)
                {
                    billbord.GetComponent<Animator>().Play("sighnDown", -1, 0f);
                    stageClear = true;
					moveOn = true;
                }
                
                
                
            }
        }


        if (curentTataralStage == 1)
        {
            if (Input.GetButtonDown("Dash"))
            {
                if (!stageClear)
                {
                    billbord.GetComponent<Animator>().Play("sighnDown", -1, 0f);
                    nextWaveSign.GetComponent<Animator>().Play("nextWaveSign", -1, 0f);
                    stageClear = true;
					moveOn = true;
                }
            }

            
        }

        if (curentTataralStage == 2)
        {
            if(spawnAThing)
            {
                Instantiate(foodPre, spawner.transform.position, spawner.transform.rotation);
                spawnAThing = false;
            }
          
            if (player1.GetComponent<PlayerMovement>().WalkItOff == true || player2.GetComponent<PlayerMovement>().WalkItOff == true)
            {
                if (!stageClear)
                {
                    billbord.GetComponent<Animator>().Play("sighnDown", -1, 0f);
                    nextWaveSign.GetComponent<Animator>().Play("nextWaveSign", -1, 0f);
                    stageClear = true;
					moveOn = true;
                }
            }
        }

        if (curentTataralStage == 3)
        {

            if (spawnAThing)
            {
                Instantiate(dummy, spawner.transform.position, spawner.transform.rotation);
                spawnAThing = false;
            }

            if (Input.GetButtonDown("Attack"))
            {
                if(nextLevel.GetComponent<BoxCollider2D>().enabled == false)
                {
                    billbord.GetComponent<Animator>().Play("sighnDown", -1, 0f);
                    nextWaveSign.GetComponent<Animator>().Play("nextWaveSign", -1, 0f);
                    nextLevel.GetComponent<BoxCollider2D>().enabled = true;
                    stageClear = true;
					//moveOn = true;
                }
                
            }
        }

        if (p1 && p2)
        {
            SetNewPosition();
            UpdatePosition();
            spawnAThing = true;
            curentTataralStage++;
            billbord.GetComponent<Animator>().Play("signGoUp", -1, 0f);
            p1 = false;
            p2 = false;
            stageClear = false;
           
        }

		if(stageClear)
        {
            nextWave.GetComponent<BoxCollider2D>().enabled = true;
            nextWaveSign.transform.position = new Vector2(transform.position.x, nextWaveSign.transform.position.y);
            
            moveOn = false;
        }
        else
        {
            nextWave.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void SetNewPosition()
    {
        SetWaveOver();
        destination = new Vector3(transform.position.x + xDistance, transform.position.y, transform.position.z);
        minX = transform.position.x;
        GameManager.NumEnemiesKilled = 0;
    }

    //Called if both players are ready
    public void SetIsMoveable(bool isMoveable)
    {
        moveable = isMoveable;
    }

    private bool IsMovingForward()
    {
        return (transform.position.x >= minX);
    }

    private void UpdatePosition()
    {
        if (transform.position.x > destination.x)
        {
            transform.position = destination;
            SetIsMoveable(false);
            Enemy_Spawn.Respawn = true;
            nextWave.GetComponent<BoxCollider2D>().enabled = false;
            
            return;
        }
        centerOfPlayers = (player1.position.x + player2.position.x) / 2;
        playerPos = new Vector3(centerOfPlayers, transform.position.y, transform.position.z);
        transform.position = playerPos;
        minX = transform.position.x;
    }

    private void SetWaveOver()
    {
        if ( Input.GetKeyDown(KeyCode.P))
        {
            waveOver = true;
        }
        else
        {
            waveOver = false;
        }
    }

    private void ActivateNextWave()
    {
       // nextWave.GetComponent<BoxCollider2D>().enabled = true;
       // nextWaveSign.transform.position = new Vector2(transform.position.x, nextWaveSign.transform.position.y);
       // nextWaveSign.GetComponent<Animator>().Play("nextWaveSign", -1, 0f);
       // waveOver = false;
    }

    
    public void playhit()
    {
        dummy.GetComponent<Animator>().Play("dummyhit");
        
       // dummy.GetComponent<Animator>().Play("Dummy", -1, 0f);
    }

}
