using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public GameObject target;
	public float speed;
	public float rotationSpeed;

	private Rigidbody2D rb;
	private Vector2 vectorToTarget;
	private Vector2 velocity;
	private Vector2 randVec;
	private LevelManager levelManager;
	private bool ignoringPlayer;

	private float dt;

	// Use this for initialization
	void Awake () {
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
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
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
		UpdateRotation(dt);
	}

	/// <summary>
	/// Updates the rotation of this object
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateRotation(float dt){
		if (velocity.x != 0 || velocity.y != 0){
			float angle;

			angle = Mathf.Rad2Deg * Mathf.Atan2(vectorToTarget.y,vectorToTarget.x) - 90;

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
		if (levelManager.numPlayers == 1){
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

}
