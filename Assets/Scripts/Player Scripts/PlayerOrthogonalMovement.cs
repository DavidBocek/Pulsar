using UnityEngine;
using System.Collections;

public class PlayerOrthogonalMovement : MonoBehaviour {

	public float speed;

	private Vector2 velocity;
	private Rigidbody2D rb;

	private float dt;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		UpdateInput(dt);
		rb.velocity = velocity;
	}


	/// <summary>
	/// Updates velocity and other relevant variables based on input.
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateInput(float dt){
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		velocity.x = horizontal;
		velocity.y = vertical;
		if (horizontal != 0 && vertical != 0){
			velocity.Normalize();
		}
		velocity *= speed;
	}
}
