using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {


    public List<Vector2>[] typesOfMovements;

    [Tooltip("wait time between movements")]
    public float waitTime = 1.0f;
    public LittleFattyM enemy;
    public PlayerActions player1;
    public PlayerActions player2;
    public Control_Suport playerC1;
    public Control_Suport playerC2;
    public Transform startPt, controlPt, endPt;
    public int numEnemiesKilled;
    public bool waveOver;
    public bool player2Ready, player1Ready;
    public int numPlayersReady;
    public int numPlayersAlive;
	public Sprite hp0, hp1, hp2, hp3, hp4, hp5, hp6, hp7, hp8, hpOver1, hpOver2, hpOver3, hpFull;
    public Sprite[] healthSprites;
    public GameObject GameOver;
    public GameObject PauseScreen;
    public bool Ispaused;
    public int bosstime;
    public GameObject boss;
    public Transform bossSpawn;
    public float min;
    public float max;
	public Color enemyDeathColor;
    void Awake()
    {
        //enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<LittleFattyM>();
        typesOfMovements = new List<Vector2>[enemy.numMovements];
        populateTypes(typesOfMovements, enemy.numMovements, enemy.radius, startPt, endPt, controlPt, enemy.ctrlHeightAdjuster, enemy.ctrlAngleAdjuster);
        numPlayersAlive = 2;
		healthSprites = new Sprite[] { hp0, hp1, hp2, hp3, hp4, hp5, hp6, hp7, hp8, hpOver1, hpOver2, hpOver3, hpFull };
        foreach(List<Vector2> v in typesOfMovements)
        {
            foreach(Vector2 pos in v)
            {
                Debug.Log("[ " + pos.x + ", " + pos.y + " ]");
            }
        }

    }

    void Start() {
        numEnemiesKilled = 0;
        GameOver.SetActive(false);
        unpause();
    }

    void Update()
    {
        if (numPlayersAlive <= 0) {
            SceneManager.LoadScene(6);
			Debug.Log ("Both players have died");
           // GameOver.SetActive(true);
        }

//        if(Input.GetKeyDown(KeyCode.H))
//        {
//            BossTime();
//        }
        if (Input.GetButtonDown("Puase") || Input.GetKeyDown(KeyCode.K))
        {
            if (!Ispaused)
            {
                PauseScreen.SetActive(true);
                Ispaused = true;
                Time.timeScale = 0;
            }
            else
            {
                PauseScreen.SetActive(false);
                Ispaused = false;
                Time.timeScale = 1;
            }
        }
        // if(Ispaused)
        //  {
        //     Time.timeScale = 0;
        // }

    }

	private IEnumerator DelayedRestart()
	{
		yield return new WaitForSeconds (1);
	}

    //creating the positions of one direction
    private List<Vector2> PrecalculatePositions(float duration, Transform sp, Transform cp, Transform ep)
    {
        float dt = Time.deltaTime;
        float currentTime = 0;
        //setting capacity of List
        List<Vector2> result = new List<Vector2>((int)(duration / Time.deltaTime + 10));
        while (currentTime <= duration)
        {
           // result.Add(QuadraticInterpolation(currentTime / duration, sp.position, cp.position, ep.position));
			result.Add(LinearInterpolation(currentTime/duration,sp.position,ep.position));
            currentTime += dt;
        }
        return result;
    }

    public Vector2 QuadraticInterpolation(float t, Vector2 p, Vector2 q, Vector2 r)
    {
        //result =( 1 - t)^2 * p + 2((1 - t) * t * q) + t^2 * r
        float u = 1 - t;
        Vector2 term1 = u * u * p;
        Vector2 term2 = u * t * q * 2;
        Vector2 term3 = t * t * r;
        Vector2 result = term1 + term2 + term3;
        return result;

    }

	public Vector2 LinearInterpolation(float t, Vector2 p, Vector2 q)
	{
		return ((1 - t) * p + (t * q));
	}

    private float DegToRad(float angle)
    {
        return ((Mathf.PI * angle) / 180.0f);
    }


    //Instead of using circle, choose random x and y positions from radius / 2 to radius
    //Random.range(-radius/2, radius/2)
    //if value is less than 0, subtract radius/2
    //else add radius/2
    //Have bool for when enemy hits wall
    //Give enemy gameobject child with box collider2D to detect horizontal and vertical collisions
    //Set int numMovesLeft
    public void populateTypes(List<Vector2>[] typesOfMovements, int numMovements, float radius, Transform startPt, Transform endPt, Transform controlPt, float ctrlHeightAdjuster, float ctrlAngleAdjuster)
    {
        //want 8 different types of movements
        //up, down, left, right, upright, upleft, downright, down left
        //create a radius around the object

        // using a for loop, interate at intervals of 45 degrees from 0 to numMovements
        // float angle = (360/numMovements) * i

        // setting control point
        // if the angle is 90 or 270, set control point to the end point (creates a straight line)
        // otherwise, if angle is on the right side of the object, set control point = new Vector2(r/2 * cos(angle + 22.5), r/2 * sin(angle + 22.5))
        // if angle is on left side of object, set it equal to same thing, but subtract 22.5 instead
        // Precalculate the Vector2 array of positions and add it to the typesOfMovements

        //iterate through each direction using angles
        for (int i = 0; i < numMovements; i++) {

            //the angle interval, if 'numMovements' was 8, the interval would be 45 degrees
            float interval = 360 / numMovements;

            //the current angle in the loop
            float angle = interval * i;

            //finds a point on the edge of the object's radius at the given angle
            endPt.position = new Vector2(radius * Mathf.Cos(DegToRad(angle)), radius * Mathf.Sin(DegToRad(angle)));

            //if the object is going straight up or straight down, don't implement any curvature in the movement
            if (angle == 90 || angle == 270) {
                controlPt.position = endPt.position;
            }
            else {
                if (angle < 90 || angle > 270) {
                    controlPt.position = new Vector2((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 + ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 + ctrlAngleAdjuster))));
                }
                else if (angle > 90 && angle < 270) {
                    controlPt.position = new Vector2((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 - ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 - ctrlAngleAdjuster))));
                }
            }
            typesOfMovements[i] = (PrecalculatePositions(enemy.duration, startPt, controlPt, endPt));
        }
    }

    public void unpause()
    {
        PauseScreen.SetActive(false);
        Ispaused = false;
        Time.timeScale = 1;
    }

//    public void BossTime()
//    {
//       
//
//        if((player1.gluttony >= min && player1.gluttony <= max) || (player2.gluttony >= min && player2.gluttony <= max))
//        {
//            Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
//        }
//
//        if (player1.gluttony >= max ||  player2.gluttony >= max)
//        {
//            Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
//            if(player1.gluttony >= max)
//            {
//                playerC1.MoveHorisontal = GetComponent<Control_Suport>().slowHorizontal;
//                playerC1.MoveVertical = GetComponent<Control_Suport>().slowVertical;
//            }
//            if (player2.gluttony >= max)
//            {
//                playerC2.MoveHorisontal = GetComponent<Control_Suport>().slowHorizontal;
//                playerC2.MoveVertical = GetComponent<Control_Suport>().slowVertical;
//            }
//        }
//        numEnemiesKilled = 0;
//        
//    }
}
