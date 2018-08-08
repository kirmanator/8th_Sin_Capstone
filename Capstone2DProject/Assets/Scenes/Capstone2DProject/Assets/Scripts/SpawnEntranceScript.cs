using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntranceScript : MonoBehaviour {

	[SerializeField] private static float length;
	private float particleLength;
	private GameObject gParticles;

	void Start(){
		gParticles = Instantiate (Enemy_Spawn.GroundParticles.gameObject, transform.position, Quaternion.identity) as GameObject;
		particleLength = Enemy_Spawn.GroundParticles.main.startLifetime.constant;

		length = GetComponent<Animation> ().clip.length;
		StartCoroutine (KillOnAnimEnd (gameObject, length));
		StartCoroutine(KillOnAnimEnd(gParticles, particleLength));
	}

	// Use this method as a trigger in the object's animation
	public void EnterEnemy(){
		Enemy_Spawn.Entered ();
	}

	//Use this method to destroy the object after its animation ends
	private IEnumerator KillOnAnimEnd(GameObject go, float lifetime)
	{
		yield return new WaitForSeconds (length);
		Destroy (go);
	}
}
