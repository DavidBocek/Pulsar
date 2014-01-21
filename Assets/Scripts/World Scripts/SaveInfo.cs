using UnityEngine;
using System.Collections;

public static class SaveInfo {

	public static int score;
	public static int lives;
	public static float timeLeft;			//in seconds
	public static int currentLevel;

	public static void SaveGame(int score, int lives, float timeLeft, int currentLevel){
		SaveInfo.score = score;
		SaveInfo.lives = lives;
		SaveInfo.timeLeft = timeLeft;
		SaveInfo.currentLevel = currentLevel;
	}
	
}
