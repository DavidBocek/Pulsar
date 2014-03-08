using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public GameObject target;
	public float speed;
	public float rotationSpeed;
	public float initialDelayBeforeMovement;

	private Rigidbody2D rb;
	private Vector2 vectorToTarget;
	private Vector2 velocity;
	private Vector2 randVec;
	private LevelManager levelManager;
	private bool ignoringPlayer;
	private bool frozen = false;

	private float dt;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
		if (target == null){
			if (levelManager.numPlayers == 1){
				target = levelManager.player1;
			} else {
				if (Random.Range(0,1) == 1) target = levelManager.player2;
				else target = levelManager.player1;
			}
		}
		if (levelManager.numPlayers == 2){
			StartCoroutine("cConstantlyAdjustTarget");
		}
	}

	void OnLevelWasLoaded(int level){
		StartCoroutine("cFreezeForTime",4.5f + initialDelayBeforeMovement);
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		if (frozen) return;
		if (!target.GetComponent<MeshRenderer>().enabled && !ignoringPlayer){
			StartCoroutine("cIgnoreDeadPlayer",1.75f);
		}
		if (ignoringPlayer){
			vectorToTarget = randVec;
			rb.velocity = randVec * speed;
		} else {
			Vector3 vec3d = (target.transform.position - transform.position).normalized;
			vectorToTarget.x = vec3d[0]; vectorToTarget.y = vec3d[1];
			velocity = vectorToTarget * speed;
			rb.velocity = velocity;
		}
	}

	void FixedUpdate(){
		if (frozen) return;
		UpdateRotation(dt);
	}

	/// <summary>
	/// Updates the rotation of this object
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateRotation(float dt){
		if (velocity.x != 0 || velocity.y != 0){
			float angle;

			angle = Mathf.Rad2Deg * Mathf.Atan2(vectorToTarget.y,vectorToTarget.x) + 180;

			if (angle != transform.localEulerAngles.z){
				Vector3 newAngles = new Vector3(0f,0f,Mathf.MoveTowardsAngle(transform.localEulerAngles.z,angle,rotationSpeed));
				transform.localEulerAngles = newAngles;
			}
		}
	}

	void KillSuccessful(float time){
		StartCoroutine("cIgnoreDeadPlayer",time);
	}

	IEnumerator cIgnoreDeadPlayer(float time){
		if (levelManager.numPlayers == 1 || Random.Range (0,3) == 2){
			randVec = (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f))).normalized;
			ignoringPlayer = true;
			yield return new WaitForSeconds(time);
			ignoringPlayer = false;
		} else {
			if (target == levelManager.player1){
				target = levelManager.player2;
			} else if (target == levelManager.player2){
				target = levelManager.player1;
			}
			yield return null;
		}
	}

	IEnumerator cFreezeForTime(float time){
		frozen = true;
		yield return new WaitForSeconds(time);
		frozen = false;
	}

	IEnumerator cConstantlyAdjustTarget(){
		while (gameObject.activeSelf){
			yield return new WaitForSeconds(.25f);
			GameObject newTarget = GetClosestPlayer();
			if (target != newTarget){
				target = newTarget;
			}
		}
	}

	GameObject GetClosestPlayer(){
		float distTo1 = Vector3.Distance(transform.position, levelManager.player1.transform.position);
		float distTo2 = Vector3.Distance(transform.position, levelManager.player2.transform.position);
		GameObject res = distTo1 < distTo2 ? levelManager.player1 : levelManager.player2;
		return res;
	}
}
