using UnityEngine;
using System.Collections;

public class StayInBetweenPlayers : MonoBehaviour {

	private LevelManager levelManager;
	Vector2 lowerLeftPoint = Vector2.zero;
	Vector2 upperRightPoint = Vector2.zero;

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
			lowerLeftPoint.x = Mathf.Min(levelManager.player1.transform.position.x,levelManager.player2.transform.position.x);
			lowerLeftPoint.y = Mathf.Min(levelManager.player1.transform.position.y,levelManager.player2.transform.position.y);
			upperRightPoint.x = Mathf.Max(levelManager.player1.transform.position.x,levelManager.player2.transform.position.x);
			upperRightPoint.y = Mathf.Max(levelManager.player1.transform.position.y,levelManager.player2.transform.position.y);
			float distX = Mathf.Abs(upperRightPoint.x-lowerLeftPoint.x); float distY = Mathf.Abs(upperRightPoint.y-lowerLeftPoint.y);
			float maxDist = Mathf.Max(distX,distY);
			if (maxDist == distY && maxDist > 10f){
				Camera.main.orthographicSize = maxDist/2f+3f;
			} else if (maxDist == distX && maxDist > 40f/3f){
				Camera.main.orthographicSize = maxDist*3/8f+3f;
			} else {
				Camera.main.orthographicSize = 8;
			}
		}
	}
}
