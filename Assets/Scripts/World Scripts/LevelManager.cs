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
	public AudioClip nextLevelClip;
	public AudioClip startSound;

	public GameObject player1 {get; set;}
	public GameObject player2 {get; set;}

	private EnemySpawner enemySpawner;
	private UpdateLivesAndTime livesText;
	private bool startedOverwhelm;
	private AudioSource musicSource;
	private AudioSource sfxSource;
	private bool transitioning = false;
	private GUIText p1Mines;
	private GUIText p2Mines;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		if (!LeaderboardInfo.initialized){
			LeaderboardInfo.Initialize();
		}
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		p1Mines = GameObject.FindWithTag("MineText").GetComponent<GUIText>();
		p2Mines = GameObject.FindWithTag("MineText2").GetComponent<GUIText>();
		musicSource = GameObject.FindWithTag ("Music").GetComponent<AudioSource>();
		sfxSource = GameObject.FindWithTag("SFXSource").GetComponent<AudioSource>();
		StartCoroutine("cFadeInMusic");
		AudioSource.PlayClipAtPoint(startSound, player1.transform.position);
	}

	void OnLevelWasLoaded(int levelIndex){
		if (levelIndex == 5) return;
		livesText = GameObject.FindWithTag("LivesText").GetComponent<UpdateLivesAndTime>();
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		p1Mines = GameObject.FindWithTag("MineText").GetComponent<GUIText>();
		p2Mines = GameObject.FindWithTag("MineText2").GetComponent<GUIText>();
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
				if (SaveInfo.currentLevel != 4 && !transitioning){
					StartCoroutine("cFadeToNextLevel");
				}
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

		//update Mine gui
		if (player1.activeSelf){
			if (player1.GetComponent<PlayerCollisions>().mineList.Count == 3){
				p1Mines.text = "P1 MINES : "+player1.GetComponent<PlayerCollisions>().mineList.Count+" (MAX)";
			} else{
				p1Mines.text = "P1 MINES : "+player1.GetComponent<PlayerCollisions>().mineList.Count;
			}
		} if (player2.activeSelf){
			if (player2.GetComponent<PlayerCollisions>().mineList.Count == 3){
				p2Mines.text = "P1 MINES : "+player2.GetComponent<PlayerCollisions>().mineList.Count+" (MAX)";
			} else{
				p2Mines.text = "P1 MINES : "+player2.GetComponent<PlayerCollisions>().mineList.Count;
			}
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

	IEnumerator cFadeToNextLevel(){
		transitioning = true;
		sfxSource.clip = nextLevelClip;
		sfxSource.Play();
		for (float i = 0; i < 1.5f; i += Time.deltaTime){
			Camera.main.gameObject.GetComponent<GlowEffect>().glowIntensity = Mathf.Lerp (1.2f,2.0f,i/1.5f);
			yield return null;
		}
		transitioning = false;
		Camera.main.gameObject.GetComponent<GlowEffect>().glowIntensity = 1.2f;
		NextLevel();
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

	IEnumerator cFadeInMusic(){
		musicSource.volume = 0;
		for (float i = 0; i < .8f; i += Time.deltaTime){
			musicSource.volume = Mathf.Lerp(0f,1f,i/.8f);
			yield return null;
		}
	}

}
