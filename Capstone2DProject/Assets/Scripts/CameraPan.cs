using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

	private Vector3 PanDistance;
	private int CameraPosIndex;
	public bool pan;
	public float lerpRate;
	private Vector3 curPos, destination;
	public int xpan;
	public int origXpan;
	private GameMaster GM;
	private Enemy_Spawn eSpawn;
	public bool cameraPan;
	private GameObject nextWave;
	public GameObject nextWaveSign;


	// Use this for initialization
	void Start () {

		//Using Vector3's because otherwise the camera's z value gets reset to 0
		PanDistance = new Vector3 (xpan, 0, 0);
		GM = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster>();
		eSpawn = transform.Find ("EnemySpawner").gameObject.GetComponent<Enemy_Spawn>();
		CameraPosIndex = 0;
		nextWave = transform.Find("NextWave").gameObject;
		nextWave.GetComponent<BoxCollider2D> ().enabled = false;
		origXpan = xpan;

	}

	// Update is called once per frame
	void Update () {

		if (GM.numEnemiesKilled == eSpawn.waves [eSpawn.waveNum] || Input.GetKeyDown(KeyCode.N)) {
			Debug.Log ("enemies killed = number of enemies spawned");
			curPos = transform.position;
			PanDistance = new Vector3 (xpan, 0, 0);
			destination = curPos + PanDistance;
			pan = true;
			eSpawn.waveNum++;
			nextWave.GetComponent<BoxCollider2D> ().enabled = true;
			nextWaveSign.transform.position = new Vector2 (transform.position.x, nextWaveSign.transform.position.y);

			nextWaveSign.GetComponent<Animator>().Play("nextWaveSign",-1,0f);
		

		}

		if (pan && cameraPan) {
				if (transform.position.x >= destination.x) {
					nextWave.GetComponent<BoxCollider2D> ().enabled = false;
					eSpawn.respawn = true;
					Debug.Log ("espawn respawn = true");
					pan = false;
					cameraPan = false;
					GM.numEnemiesKilled = 0;

				}
			transform.position = Vector3.Lerp (this.transform.position, destination + new Vector3(5,0,0), lerpRate);
			}
	}

}
