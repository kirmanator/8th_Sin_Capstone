using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Spawn : MonoBehaviour {

	public GameObject enemy;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject mainCam;

	private float randX, randY;
	private Vector2 spawnLoc;
	[SerializeField] private float spawntime = 1f;
	[SerializeField] private int[] waves = {6,8,10,12};
	[SerializeField] private int waveNum = 0;
	[SerializeField] private bool respawn;
	[SerializeField] private KeyCode spawn;

	[SerializeField] private GameObject holeEntrance;
	[SerializeField] private Animation entrance;
	[SerializeField] private static float entranceTime;
	private bool entered;
	public ParticleSystem groundParticles;

	private Transform[] players ;

	private static Enemy_Spawn instance;
//	public bool spawnBoss;

	//Setters/Getters
	#region Setters/Getters
	public static int[] Waves{ get { return instance.waves; } }
	public static int WaveNum{ get { return instance.waveNum; } set{ instance.waveNum = value; } }
	public static bool Respawn{ get { return instance.respawn; } set { instance.respawn = value; } }
	public static ParticleSystem GroundParticles{ get { return instance.groundParticles; } }

	#endregion
	void Awake(){
		instance = this;
//		GameObject prefab;
//		GameObject theActualObject = GameObject.Instantiate (prefab);
//		Here. all the scripts on that instance will already have their Awake() be called, but NOT their Start()s
	}

	void Start () {
		
		StartCoroutine (Spawn(waves[waveNum],spawntime));
		entrance = holeEntrance.GetComponent<Animation> ();
		entranceTime = entrance.clip.length/2;
		mainCam = GameObject.Find ("Main Camera");
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		spriteRenderer = transform.GetComponent<SpriteRenderer> ();

		this.players = new Transform[players.Length];
		for (int i = 0; i < players.Length; i++)
			this.players [i] = players [i].transform;
	}
	
	// Update is called once per frame
	void Update () {
//		if (enemy.Equals(null)) {
//			enemy = GameObject.FindGameObjectWithTag ("LittleFatty");
//		}
		if (instance.respawn) {
			StartCoroutine (Spawn (waves [waveNum], spawntime));
			Debug.Log ("espawn's  espawn respawn = true");

		}
		if (waveNum > waves.Length - 1) {
			SceneManager.LoadScene (9);
		}

		if (Input.GetKeyDown (spawn)) {
			StartNextWave ();

		}
		UpdatePosition ();



//		if (waveNum == waves.Length - 1) {
//			spawnBoss = true;
//			spawnBoss = false;
//		}

//		if (spawnBoss == true) {
//			gm.BossTime ();
//		}
	}

	void SpawnEnemy()
	{
		Instantiate (enemy, spawnLoc, Quaternion.identity);

	}

	private void UpdatePosition(){
		transform.position = mainCam.transform.position; 
	}

	//Called as a trigger in holeEntrance animation
	public static void Entered(){
		instance.entered = true;
	}

	public IEnumerator Spawn(int numEnemies, float waitTime)
	{
		yield return new WaitForEndOfFrame ();
		instance.respawn = false;
		int curEnemies = 0;
		while(curEnemies < numEnemies)
		{
			randX = Random.Range (transform.position.x - 90,transform.position.x + 90);
			randY = Random.Range (transform.position.y - 38,transform.position.y);
			spawnLoc = new Vector2 (randX, randY);

			while(Physics2D.OverlapCircleAll(spawnLoc, 1f, GameManager.NoSpawnMask).Length != 0){

				randX = Random.Range (transform.position.x - 6,transform.position.x + 5);
				randY = Random.Range (transform.position.y - 5,transform.position.y + 2.5f);
				spawnLoc = new Vector2 (randX, randY);
			}

			Instantiate (holeEntrance, spawnLoc, Quaternion.identity);

			yield return new WaitForSeconds (entranceTime);
				if (instance.entered) {
					SpawnEnemy ();
					curEnemies++;
				}
			yield return new WaitForSeconds (waitTime);
		}

	}

	public void StartNextWave(){
		instance.respawn = true;
	}

	public static void StopWave(){
		instance.StopCoroutine (instance.Spawn (instance.waves [instance.waveNum], instance.spawntime));
	}
}
