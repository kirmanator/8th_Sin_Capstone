using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	[SerializeField] private AudioClip lfDamaged, lfAttack, lfMove;
	[SerializeField] private AudioClip playerDamaged, playerAttack, playerDash;

	public static AudioClip LFDamaged{ get { return instance.lfDamaged; } }

	public static AudioClip LFAttack{ get { return instance.lfAttack; } }

	public static AudioClip LFMove{ get { return instance.lfMove; } }

	public static AudioClip PlayerDamaged{ get { return instance.playerDamaged; } }

	public static AudioClip PlayerAttack{ get { return instance.playerAttack; } }

	public static AudioClip PlayerDash{ get { return instance.playerDash; } }

	void Awake(){
		instance = this;
	}

}
