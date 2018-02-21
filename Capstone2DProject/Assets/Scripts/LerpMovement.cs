using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour {

	//create 2 dimensional Vector2 that saves different types of movements
	//use startPt as the current object's position
	//save the Vector2 points from 'createBezierCurve' in an array
	//put that array in the typesOfMovements
	//after calculating items in array, set endPt and controlPt to new positions (play around with it in editor)
	//set startTime
	//use Time.time - startTime
	//Repeat process
	//positions = new List<Vector2> ((int) (duration / Time.deltaTime) +30)

	[Tooltip("number of directions the object can moveTowards (initialized to 8)")]
	public int numMovements = 8;
	[Tooltip("distance the object covers in a single movement")]
	public float radius = 2.0f;
	[Tooltip("how long the object takes to complete a movement")]
	public float duration = 0.5f;
	[Tooltip("wait time between movements")]
	public float waitTime = 1.0f;
	[Tooltip("the lower the value, the closer you get to the line between startPt and endPt")]
	public float ctrlAngleAdjuster = 0.5f;
	[Tooltip("Should only be value between 0 and 1. The higher this value, the greater the height of the controlPt")]
	public float ctrlHeightAdjuster = 0.5f;
	public System.Random randMovement;
	private GameMaster GM;



	//public List<Vector2> positions;


	// Use this for initialization
	void Start () {

		randMovement = new System.Random ();
		GM = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();

		StartCoroutine (InterpolateMovement (GM.typesOfMovements, duration, numMovements));

	}

	private IEnumerator InterpolateMovement(List<Vector2>[] typesOfMovements, float duration, int numMovements)
	{
		Vector2 curPos = transform.position;
		float startTime = Time.time;
		float endTime = startTime + duration;
		int index = 0;

		//pick a random direction
		int movement = randMovement.Next (0, numMovements);

		while ((Time.time < endTime) && (index < typesOfMovements[movement].Count)) {
			
			transform.position = typesOfMovements[movement][index++] + curPos;
			yield return null;
		}
//		endPt.position = transform.position;
//		startPt.position = endPt.position;


		yield return new WaitForSeconds (waitTime);
		StartCoroutine (InterpolateMovement(typesOfMovements, duration, numMovements));
	}

}
