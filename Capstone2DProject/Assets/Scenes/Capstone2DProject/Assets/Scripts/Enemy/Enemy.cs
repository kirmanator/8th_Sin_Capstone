using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	[SerializeField] protected int health;
	[SerializeField] protected int attackPoints;
	protected bool isFacingLeft;
	protected bool isAlive = true;
	public bool canBeHit = true;

	public int AttackPoints{get{return attackPoints;}}

	public abstract void TakeDamage(int amount);

	protected abstract void Update();

	void Awake(){
		isAlive = true;
	}
}
