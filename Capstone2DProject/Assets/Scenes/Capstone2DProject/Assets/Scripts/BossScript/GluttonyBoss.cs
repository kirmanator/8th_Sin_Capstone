using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluttonyBoss : MonoBehaviour {
	protected enum BossState{idle, moving, melee, ranged, dead}
	[SerializeField] protected int hp;
	[SerializeField] protected bool isAlive;
	[SerializeField] protected int attackPoints;
	protected bool isFacingLeft;
	protected bool canBeHit; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
