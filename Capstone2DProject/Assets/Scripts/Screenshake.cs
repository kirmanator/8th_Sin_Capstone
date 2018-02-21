using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

	public float ShakeTimer;
	public float ShakeIntensity;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ShakeTimer >= 0) {
			
			Vector3 ShakePos = Random.insideUnitCircle * ShakeIntensity;
			transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z + ShakePos.z);

			ShakeTimer -= 0.01f;
			ShakeIntensity += 0.01f;

		} 
		else {
			
			ShakeTimer = 0;
			ShakeIntensity = 0;
		}

	}

	public void Shake(float shaket,float shakePow)
	{
		ShakeTimer = shaket;
		ShakeIntensity = shakePow;
	}
}
