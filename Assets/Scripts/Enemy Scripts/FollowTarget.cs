using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public float speed;
	public float rotationSpeed;

	private Rigidbody2D rb;
	private Vector2 vectorToTarget;
	private Vector2 velocity;

	private float dt;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		Vector3 vec3d = (target.position - transform.position).normalized;
		vectorToTarget.x = vec3d[0]; vectorToTarget.y = vec3d[1];
		velocity = vectorToTarget * speed;
		rb.velocity = velocity;
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

}
