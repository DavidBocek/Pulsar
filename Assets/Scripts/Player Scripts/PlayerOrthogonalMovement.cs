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

	void FixedUpdate(){
		UpdateRotation(dt);
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

	/// <summary>
	/// Updates the rotation of the ship
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateRotation(float dt){
		if (velocity.x != 0 || velocity.y != 0){
			float angle;

			if (velocity.x == 0){
				if (velocity.y > 0){
					angle = 0;
				} else {
					angle = 180;
				}
			} else if (velocity.y == 0){
				if (velocity.x > 0){
					angle = -90;
				} else {
					angle = 90;
				}
			} else {
				if (velocity.x > 0 && velocity.y > 0){
					angle = -45;
				} else if (velocity.x < 0 && velocity.y > 0){
					angle = 45;
				} else if (velocity.x > 0 && velocity.y < 0){
					angle = -135;
				} else {
					angle = 135;
				}
			}
			if (angle != transform.localEulerAngles.z){
				Vector3 newAngles = new Vector3(0f,0f,Mathf.MoveTowardsAngle(transform.localEulerAngles.z,angle,7.5f));
				transform.localEulerAngles = newAngles;
			}
		}
	}
}
