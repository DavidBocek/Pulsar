using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public int score;
	public int lives;
	public int numPlayers;
	public float timeLeft;
	public bool gameEnded = false;
	public bool endWhenEnemiesAreDestroyed; public int enemiesRemainingWhenEnd;
	public static int playersToStart;

	public GameObject player1 {get; set;}
	public GameObject player2 {get; set;}

	private EnemySpawner enemySpawner;
	private UpdateLivesAndTime livesText;
	private bool startedOverwhelm;
	private AudioSource musicSource;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		if (!LeaderboardInfo.initialized){
			LeaderboardInfo.Initialize();
		}
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		musicSource = GameObject.FindWithTag ("Music").GetComponent<AudioSource>();
	}

	void OnLevelWasLoaded(int levelIndex){
		livesText = GameObject.FindWithTag("LivesText").GetComponent<UpdateLivesAndTime>();
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		if (levelIndex == 1){
			if (playersToStart == 1){
				player2.SetActive(false);
				numPlayers = 1;
			} else {
				numPlayers = 2;
			}
		}
		if (numPlayers == 1 && levelIndex != 1) player2.SetActive(false);
		if (SaveInfo.currentLevel == 4) enemySpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawner>();
	}

	void Update(){
		if (gameEnded) return;
		//handing game over
		if (lives <= 0){
			player1.GetComponent<PlayerCollisions>().KillPermanant();
			if (numPlayers == 2) player2.GetComponent<PlayerCollisions>().KillPermanant();
			gameEnded = true;
			livesText.GameOver();
			Time.timeScale = .2f;
			StartCoroutine("cLerpMusicPitchDown");
			StartCoroutine("cGameOverAfterDelay",.75f);
		} else if (timeLeft <= 0 && !startedOverwhelm){
			lives = 1;
			if (SaveInfo.currentLevel != 4){
				OutOfTime();
			} else {
				OverwhelmPlayer();
			}
		}
		//handling level switches
		if (endWhenEnemiesAreDestroyed){
			if (GameObject.FindGameObjectsWithTag("Enemy").Length <= enemiesRemainingWhenEnd && !gameEnded){
				if (SaveInfo.currentLevel != 4)
					NextLevel();
			}
		} /*else if (endOnTime){
			if (timeLeft <= endTime){
				NextLevel();
			}
		}*/

		if (Debug.isDebugBuild){
			if (Input.GetKeyDown (KeyCode.Q)){
				NextLevel();
			}
			if (Input.GetKeyDown (KeyCode.Keypad8)){
				Debug.Log ("wiping scores file");
				LeaderboardInfo.WipeScoresFile();
			}
		}

		//player addition handling
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
		musicSource.volume = 0;
		LeaderboardInfo.AddScore(score);
		Application.LoadLevel("gameover");
	}

	void OverwhelmPlayer(){
		enemySpawner.Overwhelm();
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")){
			obj.GetComponent<FollowTarget>().speed += 2;
		}
		startedOverwhelm = true;
	}

	void OutOfTime(){
		startedOverwhelm = true;
		livesText.OutOfTimeMessage();
		player1.GetComponent<PlayerCollisions>().KillPermanant();
		if (numPlayers == 2) player2.GetComponent<PlayerCollisions>().KillPermanant();
		StartCoroutine("cGameOverAfterDelay",.75f);
	}

	IEnumerator cLerpMusicPitchDown(){
		for (float i = 0; i < .75f; i += Time.deltaTime){
			musicSource.pitch = Mathf.Lerp (1f,0f,i);
			yield return null;
		}
	}

	IEnumerator cGameOverAfterDelay(float time){
		yield return new WaitForSeconds(time);
		GameOver();
	}

}
