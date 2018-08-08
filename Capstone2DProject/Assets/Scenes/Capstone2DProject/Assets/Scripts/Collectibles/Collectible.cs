using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	private bool canPickup;
	private bool stillDropping;
	private Vector2 startPosition, destination;
	private float currentTime, rateOfMovement;
	[SerializeField] private int healthUp;
	[SerializeField] private float movementDuration;

	public bool CanPickup{ get { return canPickup; } }
	public int HealthUp{ get { return healthUp; } }
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		destination = new Vector2 (startPosition.x + Random.Range(-2f,2f), startPosition.y + Random.Range(-2f,2f));
		rateOfMovement = Time.deltaTime / movementDuration;
		currentTime = rateOfMovement;
		stillDropping = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (canPickup) {
			return;
		}
		UpdatePosition ();
		UpdateColor ();

	}

	private void UpdatePosition(){
		if (currentTime >= movementDuration) {
			stillDropping = false;
			canPickup = true;
			return;
		}

		if (stillDropping) {
			transform.position = Vector2.Lerp (startPosition, destination, currentTime);
			currentTime += rateOfMovement;
		}
	}

	private void UpdateColor(){
		if (canPickup) {
			GetComponent<SpriteRenderer> ().material.color = new Color (1f, 1f, 1f);
			return;
		}
		GetComponent<SpriteRenderer> ().material.color = GameManager.FoodNotReadyColor;
	}
}
