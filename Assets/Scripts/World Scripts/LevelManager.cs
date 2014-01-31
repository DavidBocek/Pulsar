using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int score;
	public int lives;
	public int numPlayers;
	public float timeLeft;
	public bool gameEnded = false;

	public GameObject player1 {get; set;}
	public GameObject player2 {get; set;}

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		if (!LeaderboardInfo.initialized){
			LeaderboardInfo.Initialize();
		}
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		if (numPlayers == 1) player2.SetActive(false);
	}

	void Update(){
		if ((lives < 0 && !gameEnded) || (timeLeft <= 0 && !gameEnded)){
			GameOver();
		}

		if (Input.GetButtonDown("2Player")){
			numPlayers = 2;
		}

		//enter new player during gameplay
		if (numPlayers == 2 && !player2.activeSelf){
			player2.SetActive(true);
			player2.transform.position = player1.transform.position;
			player2.GetComponent<PlayerCollisions>().GiveTemporaryInvulnerability(3.5f);
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

	/*public static Vector3 FindDecentLocationNearOtherPlayer(GameObject otherPlayer){
		Vector2 newLoc = Vector2.zero;
		GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		float dist = Random.Range (1.5f,5.5f);
		float angle = Random.Range (0,359);

		newLoc = 

	}*/

}
