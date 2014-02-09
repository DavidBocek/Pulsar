using UnityEngine;
using System.Collections;

public class MinePickup : MonoBehaviour {

	public GameObject PickUp(){
		foreach (MeshRenderer r in gameObject.GetComponentsInChildren<MeshRenderer>()){
			r.enabled = false;
		}
		foreach (CircleCollider2D c in gameObject.GetComponentsInChildren<CircleCollider2D>()){
		    c.enabled = false;
		}
		gameObject.GetComponent<AudioSource>().enabled = false;
		gameObject.GetComponent<Planet>().pickedup = true;
		return gameObject;
	}

	public void Drop(){
		foreach (MeshRenderer r in gameObject.GetComponentsInChildren<MeshRenderer>()){
			r.enabled = true;
		}
		foreach (CircleCollider2D c in gameObject.GetComponentsInChildren<CircleCollider2D>()){
			c.enabled = true;
		}
		gameObject.GetComponent<AudioSource>().enabled = true;
		gameObject.GetComponent<Planet>().pickedup = false;
		StartCoroutine("cDelayPickup",1.5f);
	}

	IEnumerator cDelayPickup(float time){
		gameObject.GetComponentInChildren<PulseDamage>().delaying = true;
		yield return new WaitForSeconds(time);
		gameObject.GetComponentInChildren<PulseDamage>().delaying = false;
	}

}
