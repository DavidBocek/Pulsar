using UnityEngine;
using System.Collections;

public class MinePickup : MonoBehaviour {

	public GameObject PickUp(){
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
		return gameObject;
	}

	public void Drop(){
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
		gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
	}

}
