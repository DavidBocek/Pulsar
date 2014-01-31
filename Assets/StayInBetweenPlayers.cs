using UnityEngine;
using System.Collections;

public class StayInBetweenPlayers : MonoBehaviour {

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.numPlayers == 1){
			transform.position = levelManager.player1.transform.position;
		} else if (levelManager.numPlayers == 2){
			transform.position = (levelManager.player1.transform.position + levelManager.player2.transform.position)/2f;
			if (levelManager.player2.GetComponent<MeshRenderer>().isVisible){

			}
		}
	}
}
