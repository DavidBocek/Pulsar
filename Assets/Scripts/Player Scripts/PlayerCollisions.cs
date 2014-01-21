using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	public float respawnTime;
	public Transform respawnTransform;

	private float respawnTimer;
	private bool isRespawning = false;
	private LevelManager levelManager;

	void Awake(){
		respawnTimer = respawnTime;
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}

	void Update(){
		if (isRespawning){
			if (respawnTimer <= 0){
				isRespawning = false;
				respawnTimer = respawnTime;
				Respawn();
			}
			else {
				respawnTimer -= Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		KillOnTouch kill = other.gameObject.GetComponent<KillOnTouch>();
		if (kill){
			if (kill.killsPlayer){
				Kill();
			}
		}
	}

	void OnPulseHit(int type){
		//every pulsing planet kills the player (for now)
		Kill();
	}

	void Kill(){
		//explosion sound play
		//play death visual effect
		isRespawning = true;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		gameObject.GetComponent<PlayerOrthogonalMovement>().enabled = false;
		levelManager.lives -= 1;
	}

	void Respawn(){
		//respawn sound play
		//play respawn visual effect
		transform.position = respawnTransform.position;
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		gameObject.GetComponent<PlayerOrthogonalMovement>().enabled = true;
	}

}
