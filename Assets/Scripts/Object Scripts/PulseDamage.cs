﻿using UnityEngine;
using System.Collections;

public class PulseDamage : MonoBehaviour {

	public int type;

	void OnTriggerEnter2D(Collider2D other){
		other.gameObject.SendMessage("OnPulseHit",type);
	}
}