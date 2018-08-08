using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {
	/*
	 * Summary: Once wave is complete, the players must both reach the end of the screen
	 * When they do, unlock camera's position and set its position to between the two players
	 * The camera cannot move backwards, only forward until it reaches its destination
	 * Once it reaches the destination, lock the camera's position to its destination
	 * 
	 * Variables: 
	 * float distance between current x and destination x
	 * Vector2 destination position 
	 * bool moveable
	 * bool movingForward
	 * float minimum x position to keep camera from going backwards
	 * float x position between players
	 *
	 * 
	 */

	[SerializeField] private Transform player1, player2;
	[SerializeField] private GameObject nextWave;
	[SerializeField] private GameObject nextWaveSign;
	[SerializeField] private float xDistance; // = 40

	[SerializeField] private Vector3 destination;
	private float minX;
	private float centerOfPlayers;
	private Vector3 playerPos;
	private bool moveable;
	private bool waveOver;


	// Use this for initialization
	void Start () {

		nextWave = transform.Find("NextWave").gameObject;
		nextWave.GetComponent<BoxCollider2D> ().enabled = false;
		nextWaveSign = GameObject.Find ("NextWaveSign");
		SetNewPosition();
	}

	// Update is called once per frame
	void Update () {
		SetWaveOver ();
		if (waveOver) {
			//Debug.Log ("Wave is Over");
			//Debug.Log ("waveOver: " + waveOver);
			ActivateNextWave ();
			//Debug.Log ("waveOver: " + waveOver);
		}
		if (moveable) {
			if (IsMovingForward ()) {
				UpdatePosition ();
			}
		}
	}

	public void SetNewPosition(){
		SetWaveOver ();
		destination = new Vector3 (transform.position.x + xDistance, transform.position.y, transform.position.z);
		minX = transform.position.x;
		GameManager.NumEnemiesKilled = 0;
	}

	//Called if both players are ready
	public void SetIsMoveable(bool isMoveable){
		moveable = isMoveable;
	}

	private bool IsMovingForward(){
		return (transform.position.x >= minX);
	}

	private void UpdatePosition(){
		if (transform.position.x > destination.x) {
			transform.position = destination;
			SetIsMoveable (false);
			Enemy_Spawn.Respawn = true;
			nextWave.GetComponent<BoxCollider2D> ().enabled = false;
			return;
		}
		centerOfPlayers = (player1.position.x + player2.position.x) / 2;
		playerPos = new Vector3 (centerOfPlayers, transform.position.y, transform.position.z);
		transform.position = playerPos;
		minX = transform.position.x;
	}

	private void SetWaveOver(){
		if (GameManager.NumEnemiesKilled == Enemy_Spawn.Waves [Enemy_Spawn.WaveNum]) {
			waveOver = true;
		}
		else {
			waveOver = false;
		}
	}
		
	public void ActivateNextWave(){
		nextWave.GetComponent<BoxCollider2D> ().enabled = true;
		Enemy_Spawn.StopWave ();
		//Debug.Log ("Wave Activated");
		nextWaveSign.transform.position = new Vector2 (transform.position.x, nextWaveSign.transform.position.y);
		nextWaveSign.GetComponent<Animator>().Play("nextWaveSign",-1,0f);
		SetNewPosition ();
		GameManager.Player1Ready = false;
		GameManager.Player2Ready = false;
		Enemy_Spawn.WaveNum++;
		waveOver = false;
		GameManager.NumEnemiesKilled = 0;
	}
	#region Old Code
//
//	private void UpdateCamPosition(){
//		if (transform.position.x >= destination.x) {
//			nextWave.GetComponent<BoxCollider2D> ().enabled = false;
//			eSpawn.Respawn = true;
////			Debug.Log ("espawn respawn = true");
//			pan = false;
//			cameraPan = false;
//			GameManager.Instance.numEnemiesKilled = 0;
//		}
//		transform.position = Vector3.Lerp (this.transform.position, destination + new Vector3(5,0,0), lerpRate);
//	}
	#endregion
}
