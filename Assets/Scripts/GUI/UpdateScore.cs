using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {

	private LevelManager levelManager;
	private GUIText text;

	void Start(){
		levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
		text = GetComponent<GUIText>();

	}

	void Update () {
		if (levelManager.score < LeaderboardInfo.leaderboardScores[9]){
			text.text = "HIGHSCORE : " + LeaderboardInfo.leaderboardScores[9]
						+ "\nSCORE : " + levelManager.score;
		} else {
			text.text = "HIGHSCORE : " + levelManager.score
						+ "\nSCORE : " + levelManager.score;
			text.color = Color.yellow;
		}
	}
}
