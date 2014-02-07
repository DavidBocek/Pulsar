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
				"HIGH SCORES\n\n" +
				/*LeaderboardInfo.leaderboardNames[9] + */LeaderboardInfo.leaderboardScores[9] + "\n" +
				/*LeaderboardInfo.leaderboardNames[8] + */LeaderboardInfo.leaderboardScores[8] + "\n" +
				/*LeaderboardInfo.leaderboardNames[7] + */LeaderboardInfo.leaderboardScores[7] + "\n" +
				/*LeaderboardInfo.leaderboardNames[6] + */LeaderboardInfo.leaderboardScores[6] + "\n" +
				/*LeaderboardInfo.leaderboardNames[5] + */LeaderboardInfo.leaderboardScores[5] + "\n" +
				/*LeaderboardInfo.leaderboardNames[4] + */LeaderboardInfo.leaderboardScores[4] + "\n" +
				/*LeaderboardInfo.leaderboardNames[3] + */LeaderboardInfo.leaderboardScores[3] + "\n" +
				/*LeaderboardInfo.leaderboardNames[2] + */LeaderboardInfo.leaderboardScores[2] + "\n" +
				/*LeaderboardInfo.leaderboardNames[1] + */LeaderboardInfo.leaderboardScores[1] + "\n" +
				/*LeaderboardInfo.leaderboardNames[0] + */LeaderboardInfo.leaderboardScores[0] + "\n";
	}
}
