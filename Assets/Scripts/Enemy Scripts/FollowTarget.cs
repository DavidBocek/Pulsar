using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public float speed;

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
}
