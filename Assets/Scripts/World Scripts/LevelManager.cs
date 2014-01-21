using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int score;
	public int lives;
	public float timeLeft;
	public bool gameEnded = false;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		if (!LeaderboardInfo.initialized){
			LeaderboardInfo.Initialize();
		}
	}

	void Update(){
		if (lives < 0 && !gameEnded){
			GameOver();
		}
	}

	public void NextLevel(){
		SaveInfo.SaveGame(score,lives,timeLeft,SaveInfo.currentLevel + 1);
		Application.LoadLevel("scene"+SaveInfo.currentLevel);
	}

	void GameOver(){
		LeaderboardInfo.AddScore(score);
		Application.LoadLevel("gameover");
		gameEnded = true;
	}

}
