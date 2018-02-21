using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject curPos;
	private GameObject p1, p2;
	private Vector2 velocity;

	public bool bounds;
	public float minX,minY,maxX,maxY;
	public float smoothTimeX, smoothTimeY;



	// Use this for initialization
	void Start () {
		p1 = GameObject.FindGameObjectWithTag ("Player1");
		p2 = GameObject.FindGameObjectWithTag ("Player2");
//		float MaxX = Mathf.Max (p1.transform.position.x, p2.transform.position.x);
//		float MaxY = Mathf.Max (p1.transform.position.y, p2.transform.position.y);
//		float MinX = Mathf.Min (p1.transform.position.x, p2.transform.position.x);
//		float MinY = Mathf.Min (p1.transform.position.y, p2.transform.position.y);
		Vector2 newPos = ((p1.transform.position/2) + (p2.transform.position/2));
		//curPos.transform.position = new Vector2 (Mathf.Abs((p1.transform.position.x + p2.transform.position.x) / 2), Mathf.Abs((p1.transform.position.y + p2.transform.position.y) / 2));
		curPos.transform.position = newPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, curPos.transform.position.x, ref velocity.x, smoothTimeX); 
		float posY = Mathf.SmoothDamp (transform.position.y, curPos.transform.position.y, ref velocity.y, smoothTimeY); 

		transform.position = new Vector3 (posX, posY, -20);
		if (bounds) {
			if (curPos.transform.position.x < minX) {
				curPos.transform.position = new Vector2 (minX, curPos.transform.position.y);
			}
			if (curPos.transform.position.y < minY) {
				curPos.transform.position = new Vector2 (curPos.transform.position.x, minY);
			}
			if (curPos.transform.position.x > maxX) {
				curPos.transform.position = new Vector2 (maxX, curPos.transform.position.y);
			}
			if (curPos.transform.position.y > maxY) {
				curPos.transform.position = new Vector2 (curPos.transform.position.x, maxY);
			}
		}
	}
}
