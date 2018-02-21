using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Spawn : MonoBehaviour {

	public GameObject enemy;

	private float randX, randY;
	private Vector2 spawnLoc;
	public float spawntime = 1f;
	public int[] waves = {6,8,10,12};
	public int waveNum = 0;
	public bool respawn, respawnCheck;
	public KeyCode spawn;
	public CameraPan cam;
	public Animation entrance;
	public GameObject holeEntrance;
	public bool entered;
	public ParticleSystem groundParticles;
	private GameMaster gm;
	public bool spawnBoss;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<CameraPan> ();
		StartCoroutine (Spawn(waves[waveNum],spawntime));
		gm = GameObject.Find ("GameMaster").GetComponent<GameMaster>();

	}
	
	// Update is called once per frame
	void Update () {
//		if (enemy.Equals(null)) {
//			enemy = GameObject.FindGameObjectWithTag ("LittleFatty");
//		}
		if (respawn) {
			StartCoroutine (Spawn (waves [waveNum], spawntime));
			Debug.Log ("espawn's  espawn respawn = true");
			respawnCheck = true;

		}
		if (waveNum > waves.Length - 1) {
			SceneManager.LoadScene (6);
		}
		else {
			cam.xpan = cam.origXpan;
		}
		if (Input.GetKeyDown (spawn)) {
			respawn = true;

		}

		if (waveNum == waves.Length - 1) {
			spawnBoss = true;
			spawnBoss = false;
		}

//		if (spawnBoss == true) {
//			gm.BossTime ();
//		}
	}

	void SpawnEnemy()
	{
		Instantiate (enemy, spawnLoc, Quaternion.identity);

	}

	private IEnumerator Spawn(int numEnemies, float waitTime)
	{
		yield return new WaitForEndOfFrame ();
		respawn = false;
		int curEnemies = 0;
		while(curEnemies < numEnemies)
		{
			randX = Random.Range (transform.position.x - 6,transform.position.x + 6);
			randY = Random.Range (transform.position.y - 5,transform.position.y + 2.5f);
			spawnLoc = new Vector2 (randX, randY);


			Instantiate (holeEntrance, spawnLoc, Quaternion.identity);
			Instantiate (groundParticles, spawnLoc, Quaternion.identity);
				if (!(entered)) {
					SpawnEnemy ();
					yield return new WaitForSeconds (waitTime);
					curEnemies++;
				}
			yield return null;
		}

	}
}
