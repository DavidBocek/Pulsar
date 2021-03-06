﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class LeaderboardInfo {

	public static bool initialized = false;
	public static List<int> leaderboardScores;
	public static int mostRecentlyInputScoreIndex = -1;

	public static void Initialize(){
		leaderboardScores = new List<int>();
		for (int i = 0; i<10; i++){
			leaderboardScores.Add(0);
		}
		LeaderboardInfo.LoadScoresFile();
	}

	public static void AddScore(int score){
		if (score > leaderboardScores[0]){
			leaderboardScores.RemoveAt(0);
			leaderboardScores.Add(score);
			leaderboardScores.Sort();
			mostRecentlyInputScoreIndex = leaderboardScores.IndexOf(score);
			SaveScoresFile();
		}
	}

	public static void LoadScoresFile(){
		for (int i = 0; i<10; i++){
			leaderboardScores[i] = PlayerPrefs.GetInt("Score"+i);
		}
	}

	public static void SaveScoresFile(){
		for (int i = 0; i<10; i++){
			PlayerPrefs.SetInt("Score"+i,leaderboardScores[i]);
		}
		PlayerPrefs.Save();
	}

	public static void WipeScoresFile(){
		for (int i = 0; i<10; i++){
			PlayerPrefs.SetInt("Score"+i,0);
		}
		PlayerPrefs.Save();
	}

}
