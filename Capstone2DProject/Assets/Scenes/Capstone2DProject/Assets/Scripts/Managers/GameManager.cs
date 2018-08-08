using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	#region LittleFatty Variables
	[Header("Little Fatty")]
	[Tooltip("distance the object covers in a single movement")]
	[SerializeField] private float lfRadius; // 2f

	[Tooltip("distance the object covers in an attack sequence")]
	[SerializeField] private float lfAttackRadius; // 3f

	[Tooltip("how long the object takes to complete a movement")]
	[SerializeField] private float lfDuration; // 0.3f

	[Tooltip("wait time between each movement")]
	[SerializeField] private float lfWaitTime; // 0.4f

	[Tooltip("wait time between spawning and moving")]
	[SerializeField] private float lfSpawnWait; // 1f

	[Tooltip("amount of damage done to player")]
	[SerializeField] private int attackPoints; // 1f

	#endregion
	#region BigFatty Variables
	[Header("Big Fatty")]
	[Tooltip("distance the object covers in a single movement")]
	[SerializeField] private float bfRadius;

	[Tooltip("distance the object covers in an attack sequence")]
	[SerializeField] private float bfAttackRadius;

	[Tooltip("how long the object takes to complete a movement")]
	[SerializeField] private float bfDuration;

	[Tooltip("wait time between each movement")]
	[SerializeField] private float bfWaitTime;

	[Tooltip("wait time between spawning and moving")]
	[SerializeField] private float bfSpawnWait;
	#endregion
	#region Old Code
//	public List<Vector2>[] typesOfMovements;

//    [Tooltip("wait time between movements")]
//    public float waitTime = 1.0f;
    
//	public int numMovements;
//	public float ctrlHeightAdjuster, ctrlAngleAdjuster;
//	public LayerMask bgLayerMask;
//  public PlayerActions player1;
//  public PlayerActions player2;
//  public Control_Suport playerC1;
//  public Control_Suport playerC2;
//	public Transform startPt, controlPt, endPt;
	#endregion
	[SerializeField] private int numEnemiesKilled;
	[SerializeField] private bool player2Ready, player1Ready;
	[SerializeField] private int numPlayersReady;
	[SerializeField] private int numPlayersAlive;
	[SerializeField] private int numCoins;
	[SerializeField] private float curFade, fadeRate;

	public Sprite hp0, hp1, hp2, hp3, hp4, hp5, hp6, hp7, hp8, hpOver1, hpOver2, hpOver3, hpFull;
	private Sprite[] healthSprites;

   // public GameObject GameOver;
    public GameObject PauseScreen;
	[SerializeField] private GameObject food;
	[SerializeField] private GameObject coin;
	[SerializeField] private GameObject spitBall;
	[SerializeField] private GameObject hitConfirm;
	[SerializeField] private Text coinText;
	[SerializeField] private Image fadeOutImg;
	[SerializeField] private bool isPaused;
	[SerializeField] private int bosstime;
	[SerializeField] private GameObject boss;
	[SerializeField] private Transform bossSpawn;
    //public float min;
    //public float max;
	[SerializeField] private Color enemyDeathColor;
	[SerializeField] private Color foodNotReadyColor;
	[SerializeField] private CameraPan cam;

	[SerializeField] private LayerMask playerMask, bgLayerMask, noSpawnMask;

	[SerializeField] private LittleFattyM enemy;
	[SerializeField] private KeyCode nextWave;

	private static GameManager instance;
    public Text endGameT;
	//Setters/Getters
	#region Setters/Getters
	public static int NumEnemiesKilled { get { return instance.numEnemiesKilled; } set{ instance.numEnemiesKilled = value; } }
	public static bool Player1Ready { get { return instance.player1Ready; } set{ instance.player1Ready = value; } }
	public static bool Player2Ready { get { return instance.player2Ready; } set{ instance.player2Ready = value; } }
	public static int NumPlayersReady{ get { return instance.numPlayersReady; } set{ instance.numPlayersReady = value; } }
	public static int NumPlayersAlive{ get { return instance.numPlayersAlive; } set{ instance.numPlayersAlive = value; } }
	public static int NumCoins{ get { return instance.numCoins; } set{ instance.numCoins = value; } }
	public static bool IsPaused{ get { return instance.isPaused; } }
	public static Color FoodNotReadyColor{ get { return instance.foodNotReadyColor; } }
	public static Color EnemyDeathColor{ get { return instance.enemyDeathColor; } }
	public static LayerMask PlayerMask{ get  {return instance.playerMask; } }
	public static LayerMask BGLayerMask{ get { return instance.bgLayerMask; } }
	public static LayerMask NoSpawnMask{ get { return instance.noSpawnMask; } }
	public static GameObject Food{ get { return instance.food; } }
	public static GameObject Coin{ get { return instance.coin; } }
	public static GameObject SpitBall { get { return instance.spitBall; } }
	public static GameObject HitConfirm{ get { return instance.hitConfirm; } }
	public static Text CoinText{ get { return instance.coinText; } }
	public static Sprite[] HealthSprites{ get { return instance.healthSprites; } }
	public static KeyCode NextWave{ get { return instance.nextWave; } }
//	public static GameManager Instance{ get { return instance; } }
	#endregion

    void Awake()
    {
		instance = this;
        numPlayersAlive = 2;
		playerMask = 1 << 12;
		bgLayerMask = 1 << 10;
		healthSprites = new Sprite[] { hp0, hp1, hp2, hp3, hp4, hp5, hp6, hp7, hp8, hpOver1, hpOver2, hpOver3, hpFull };

		foodNotReadyColor = new Color (120f/255f, 120f/255f, 120f/255f);
		#region Old Code
//		typesOfMovements = new List<Vector2>[enemy.numMovements];
//		populateTypes(typesOfMovements, enemy.numMovements, enemy.radius, startPt, endPt, controlPt, ctrlHeightAdjuster, ctrlAngleAdjuster, enemy.duration);
//		Debug
//      /*foreach(List<Vector2> v in typesOfMovements)
//      {
//          foreach(Vector2 pos in v)
//           
//              Debug.Log("[ " + pos.x + ", " + pos.y + " ]");
//          }
//      }*/
		#endregion

    }

    void Start() {
		fadeRate = Mathf.Max (1, fadeRate);
		fadeRate = Time.deltaTime / fadeRate;
        numEnemiesKilled = 0;
      //  GameOver.SetActive(false);
        unpause();
      //  Scene currintSene = SceneManager.GetActiveScene();
		if (fadeOutImg != null) {
			fadeOutImg.color = new Color (0f, 0f, 0f, 0f);
		}
    }

    void Update()
    {
        if (numPlayersAlive <= 0) {
			StartCoroutine (FadeOut ());
			numPlayersAlive = 2;
//			Debug.Log ("Both players have died");
//        	GameOver.SetActive(true);
        }
		UpdateNextWave ();
		GetInput ();

		if (numPlayersReady == 2) {
			numPlayersReady = 0;
			player1Ready = false;
			player2Ready = false;
		}
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
             
            GameObject.Find("Player1").GetComponent<PlayerMovement>().frendlyFire = true;
            GameObject.Find("Player2").GetComponent<PlayerMovement>().frendlyFire = true;
            if(GameObject.Find("Player1").GetComponent<PlayerActions>().IsAlive == false || GameObject.Find("Player2").GetComponent<PlayerActions>().IsAlive == false)
            {
                StartCoroutine(DelayedRestart());
            }
			if (Input.GetKeyDown (KeyCode.R)) {
				StartCoroutine(DelayedRestart ());
			}
        }
        else
        {
            GameObject.Find("Player1").GetComponent<PlayerMovement>().frendlyFire = false;
            GameObject.Find("Player2").GetComponent<PlayerMovement>().frendlyFire = false;
        }
    }

	private void GetInput(){
		//        if(Input.GetKeyDown(KeyCode.H))
		//        {
		//            BossTime();
		//        }
		if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.K))
		{
			if (!isPaused)
			{
				PauseScreen.SetActive(true);
				isPaused = true;
				Time.timeScale = 0;
                
			}
			else
			{
				PauseScreen.SetActive(false);
				isPaused = false;
				Time.timeScale = 1;
			}
		}
		if (Input.GetKeyDown (nextWave)) {
			cam.ActivateNextWave ();
		}
	}

	private IEnumerator DelayedRestart()
	{
        endGameT.text = "By defeating this mighty(ish) warrior, you have at last become the 8th sin ... betrayal. ";

        yield return new WaitForSeconds (4);
        SceneManager.LoadScene(1);
    }

	private void UpdateNextWave(){
		if (numPlayersReady == 2) {
			cam.SetIsMoveable (true);
			numPlayersReady = 0;
		}
	}

	#region Old Code
    //creating the positions of one direction
//    private List<Vector2> PrecalculatePositions(float duration, Transform sp, Transform cp, Transform ep)
//    {
//        float dt = Time.deltaTime;
//        float currentTime = 0;
//        //setting capacity of List
//        List<Vector2> result = new List<Vector2>((int)(duration / Time.deltaTime + 10));
//        while (currentTime <= duration)
//        {
//           // result.Add(QuadraticInterpolation(currentTime / duration, sp.position, cp.position, ep.position));
//			result.Add(LinearInterpolation(currentTime/duration,sp.position,ep.position));
//            currentTime += dt;
//        }
//        return result;
//    }

//    public Vector2 QuadraticInterpolation(float t, Vector2 p, Vector2 q, Vector2 r)
//    {
//        //result =( 1 - t)^2 * p + 2((1 - t) * t * q) + t^2 * r
//        float u = 1 - t;
//        Vector2 term1 = u * u * p;
//        Vector2 term2 = u * t * q * 2;
//        Vector2 term3 = t * t * r;
//        Vector2 result = term1 + term2 + term3;
//        return result;
//
//    }

//	public Vector2 LinearInterpolation(float t, Vector2 p, Vector2 q)
//	{
//		return ((1 - t) * p + (t * q));
//	}
//
//    private float DegToRad(float angle)
//    {
//        return ((Mathf.PI * angle) / 180.0f);
//    }


    //Instead of using circle, choose random x and y positions from radius / 2 to radius
    //Random.range(-radius/2, radius/2)
    //if value is less than 0, subtract radius/2
    //else add radius/2
    //Have bool for when enemy hits wall
    //Give enemy gameobject child with box collider2D to detect horizontal and vertical collisions
    //Set int numMovesLeft

//	public void populateTypes(List<Vector2>[] typesOfMovements, int numMovements, float radius, Transform startPt, Transform endPt, Transform controlPt, float ctrlHeightAdjuster, float ctrlAngleAdjuster, float duration)
//    {
//        //want 8 different directional movements
//        //up, down, left, right, upright, upleft, downright, downleft
//        //create a radius around the object
//
//        // using a for loop, interate at intervals of 45 degrees from 0 to numMovements
//        // float angle = (360/numMovements) * i
//
//        // setting control point
//        // if the angle is 90 or 270, set control point to the end point (creates a straight line)
//        // otherwise, if angle is on the right side of the object, set control point = new Vector2(r/2 * cos(angle + 22.5), r/2 * sin(angle + 22.5))
//        // if angle is on left side of object, set it equal to same thing, but subtract 22.5 instead
//        // Precalculate the Vector2 array of positions and add it to the typesOfMovements
//
//        //iterate through each direction using angles
//        for (int i = 0; i < numMovements; i++) {
//
//            //the angle interval, if 'numMovements' was 8, the interval would be 45 degrees
//            float interval = 360 / numMovements;
//
//            //the current angle in the loop
//            float angle = interval * i;
//
//            //finds a point on the edge of the object's radius at the given angle
//            endPt.position = new Vector2(radius * Mathf.Cos(DegToRad(angle)), radius * Mathf.Sin(DegToRad(angle)));
//
//            //if the object is going straight up or straight down, don't implement any curvature in the movement
//            if (angle == 90 || angle == 270) {
//                controlPt.position = endPt.position;
//            }
//            else {
//                if (angle < 90 || angle > 270) {
//                    controlPt.position = new Vector2((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 + ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 + ctrlAngleAdjuster))));
//                }
//                else if (angle > 90 && angle < 270) {
//                    controlPt.position = new Vector2((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 - ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 - ctrlAngleAdjuster))));
//                }
//            }
//            typesOfMovements[i] = (PrecalculatePositions(duration, startPt, controlPt, endPt));
//        }
//    }
	#endregion

    public void unpause()
    {
        PauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

	public void RestartGame(){

	}

	public IEnumerator FadeOut(){
		while (curFade < 1) {
			fadeOutImg.color += new Color (0f, 0f, 0f, fadeRate);
			curFade += fadeRate;
		}
		yield return new WaitForSeconds (0.5f);
		GetComponent<SceneTransition> ().goToScene (9);
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
