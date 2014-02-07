using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	public float respawnTime;
	public Transform respawnTransform;
	public AudioClip deathSound;
	public ParticleSystem deathEmitter; public int emitCount;

	private bool invulnerable = false;
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
		if (invulnerable) return;
		KillOnTouch kill = other.gameObject.GetComponent<KillOnTouch>();
		if (kill){
			if (kill.killsPlayer){
				if (kill.tag == "Enemy"){
					GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
					foreach (GameObject e in enemies){
						e.SendMessage("KillSuccessful",2.5f);
					}
				}
				Kill();
			}
		}
	}

	void OnPulseHit(int type){
		if (invulnerable) return;
		//every pulsing planet kills the player (for now)
		//Kill();
	}

	void Kill(){
		//explosion sound play
		AudioSource.PlayClipAtPoint(deathSound,transform.position,.75f);
		//play death visual effect
		deathEmitter.Emit(emitCount);

		//respawn
		isRespawning = true;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		gameObject.GetComponent<PlayerOrthogonalMovement>().exghaustParticles.enableEmission = false;
		gameObject.GetComponent<PlayerOrthogonalMovement>().enabled = false;
		gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
		levelManager.lives -= 1;
	}

	public void KillPermanant(){
		//explosion sound play
		AudioSource.PlayClipAtPoint(deathSound,transform.position,.75f);
		//play death visual effect
		deathEmitter.Emit (emitCount*3);

		//no respawn
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		gameObject.GetComponent<PlayerOrthogonalMovement>().exghaustParticles.enableEmission = false;
		gameObject.GetComponent<PlayerOrthogonalMovement>().enabled = false;
		gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
	}

	void Respawn(){
		//respawn sound play
		//play respawn visual effect
		if (levelManager.numPlayers == 1){
			transform.position = respawnTransform.position;
		} else {
			if (levelManager.player1 == gameObject){
				transform.position = levelManager.player2.transform.position;
			} else if (levelManager.player2 == gameObject){
				transform.position = levelManager.player1.transform.position;
			}
		}
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		gameObject.GetComponent<PlayerOrthogonalMovement>().enabled = true;
		gameObject.GetComponent<PlayerOrthogonalMovement>().exghaustParticles.enableEmission = true;
		gameObject.GetComponentInChildren<TrailRenderer>().enabled = true;
		GiveTemporaryInvulnerability(2.5f);
	}

	public void GiveTemporaryInvulnerability(float time){
		StartCoroutine("cInvuln",time);
		StartCoroutine("cBlink",time);
	}


	IEnumerator cInvuln(float time){
		invulnerable = true;
		yield return new WaitForSeconds(time);
		invulnerable = false;
	}

	IEnumerator cBlink(float time){
		MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
		mr.enabled = false;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = true;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = false;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = true;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = false;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = true;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = false;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = true;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = false;
		yield return new WaitForSeconds(.1f*time);
		mr.enabled = true;
	}

}
