using UnityEngine;
using System.Collections;

public class LeaderboardDisplay : MonoBehaviour {

	GUIText leaderboardText;

	// Use this for initialization
	void Start () {
		if (!LeaderboardInfo.initialized){
			LeaderboardInfo.Initialize();
		}
		LeaderboardInfo.LoadScoresFile();
		leaderboardText = GetComponent<GUIText>();
		leaderboardText.text = 
				"HIGH SCORES\n" +
				LeaderboardInfo.leaderboardScores[9] + "\n" +
				LeaderboardInfo.leaderboardScores[8] + "\n" +
				LeaderboardInfo.leaderboardScores[7] + "\n" +
				LeaderboardInfo.leaderboardScores[6] + "\n" +
				LeaderboardInfo.leaderboardScores[5] + "\n" +
				LeaderboardInfo.leaderboardScores[4] + "\n" +
				LeaderboardInfo.leaderboardScores[3] + "\n" +
				LeaderboardInfo.leaderboardScores[2] + "\n" +
				LeaderboardInfo.leaderboardScores[1] + "\n" +
				LeaderboardInfo.leaderboardScores[0] + "\n";
	}
}
