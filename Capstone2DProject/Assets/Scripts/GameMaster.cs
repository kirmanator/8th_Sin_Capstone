using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {


	public List<Vector2>[] typesOfMovements;

	[Tooltip("wait time between movements")]
	public float waitTime = 1.0f;
	LerpMovement enemy;

	public Transform startPt, controlPt, endPt;

	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag ("Player").GetComponent<LerpMovement>();
		typesOfMovements = new List<Vector2>[enemy.numMovements];
		populateTypes (typesOfMovements, enemy.numMovements, enemy.radius, startPt, endPt, controlPt, enemy.ctrlHeightAdjuster, enemy.ctrlAngleAdjuster);

	}

	void Start () {


	}

	//creating the positions of one direction
	private List<Vector2> PrecalculatePositions(float duration, Transform sp, Transform cp, Transform ep)
	{
		float dt = Time.deltaTime;
		float currentTime = 0;
		//setting capacity of List
		List<Vector2> result = new List<Vector2>((int) (duration/Time.deltaTime + 10));
		while(currentTime <= duration)
		{
			result.Add(QuadraticInterpolation(currentTime / duration, sp.position, cp.position,ep.position));

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

	private float DegToRad(float angle)
	{
		return((Mathf.PI * angle) / 180.0f);
	}

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
			//the angle interval 
			float interval = 360 / numMovements;
			float angle = interval * i;

			//finds a point on the edge of the object's radius at the given angle
			endPt.position = new Vector2 (radius * Mathf.Cos (DegToRad (angle)), radius * Mathf.Sin (DegToRad (angle)));
			if (angle == 90 || angle == 270) {
				controlPt.position = endPt.position;
			} 
			else {
				if (angle < 90 || angle > 270) {
					controlPt.position = new Vector2 ((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 + ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 + ctrlAngleAdjuster))));
				} 
				else if (angle > 90 && angle < 270) {
					controlPt.position = new Vector2 ((radius * ctrlHeightAdjuster) * Mathf.Cos(DegToRad(angle * (1 - ctrlAngleAdjuster))), (radius * ctrlHeightAdjuster) * Mathf.Sin(DegToRad(angle * (1 - ctrlAngleAdjuster))));
				}
			}
			typesOfMovements[i] = (PrecalculatePositions (enemy.duration, startPt, controlPt, endPt));
		}
	}
}
