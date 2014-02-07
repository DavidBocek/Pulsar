using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public BoxCollider2D leftWallCollider;
	public BoxCollider2D topWallCollider;
	public BoxCollider2D rightWallCollider;
	public BoxCollider2D bottomWallCollider;
	public float delayUntilSpawningBegins;
	public float delayBetweenSpawn;
	public int maxEnemies;

	private float xMin; private float xMax;
	private float yMin; private float yMax;

	private bool frozen = false;
	private bool overwhelming = false;
	private LevelManager levelManager;
	private GameObject player1;
	private GameObject player2;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
		player1 = GameObject.FindWithTag("Player");
		player2 = GameObject.FindWithTag("Player2");
		xMin = -topWallCollider.transform.localScale.x/2f;
		xMax = -xMin;
		yMin = -rightWallCollider.transform.localScale.y/2f;
		yMax = -yMin;
		StartCoroutine("cFreezeForTime",delayUntilSpawningBegins);
	}
	
	// Update is called once per frame
	void Update () {
		if (frozen) return;
		if (GameObject.FindGameObjectsWithTag("Enemy").Length > maxEnemies && !overwhelming) return;
		else if (GameObject.FindGameObjectsWithTag("Enemy").Length > maxEnemies + 40) return;
		int wall = Random.Range(0,4);
		int type = Random.Range(0,3);
		GameObject newEnemy = new GameObject();
		Vector3 newPos = Vector3.zero;

		if (type == 0){
			newEnemy = (GameObject)Instantiate(Resources.Load("BasicEnemyBlue"));
		} else if (type == 1){
			newEnemy = (GameObject)Instantiate(Resources.Load("BasicEnemyYellow"));
		} else if (type == 2){
			newEnemy = (GameObject)Instantiate(Resources.Load("BasicEnemyRed"));
		}

		switch (wall){
		case 0:
			newPos = new Vector3(leftWallCollider.transform.position.x + 1f, Random.Range(yMin,yMax),0f);
			break;
		case 1:
			newPos = new Vector3(Random.Range(xMin,xMax),topWallCollider.transform.position.y - 1f,0f);
			break;
		case 2:
			newPos = new Vector3(rightWallCollider.transform.position.x - 1f, Random.Range(yMin,yMax),0f);
			break;
		case 3:
			newPos = new Vector3(Random.Range(xMin,xMax),bottomWallCollider.transform.position.y + 1f,0f);
			break;
		}
		newEnemy.transform.position = newPos;
		if (levelManager.numPlayers == 2){
			newEnemy.GetComponent<FollowTarget>().target = Random.Range(0,1) == 0 ? player1 : player2;
		} else {
			newEnemy.GetComponent<FollowTarget>().target = player1;
		}
		if (!overwhelming){
			StartCoroutine("cFreezeForTime",delayBetweenSpawn);
		}
	}

	public void Overwhelm() {
		overwhelming = true;
	}

	IEnumerator cFreezeForTime(float time){
		frozen = true;
		yield return new WaitForSeconds(time);
		frozen = false;
	}
}
