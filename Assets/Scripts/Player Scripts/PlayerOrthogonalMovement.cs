using UnityEngine;
using System.Collections;

public class PlayerOrthogonalMovement : MonoBehaviour {

	public float speed;
	public float rotationSpeed;
	public int playerNumber;
	public ParticleSystem exghaustParticles;


	private Vector2 velocity;
	private Rigidbody2D rb;
	private bool frozen = false;

	private float dt;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (frozen) return;
		dt = Time.smoothDeltaTime;
		UpdateInput(dt);
		rb.velocity = velocity;
	}

	void FixedUpdate(){
		if (frozen) return;
		UpdateRotation(dt);
	}

	/// <summary>
	/// Updates velocity and other relevant variables based on input.
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateInput(float dt){
		float horizontal = 0;
		float vertical = 0;
		if (playerNumber == 1){
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");
		} else if (playerNumber == 2){
			horizontal = Input.GetAxis ("Horizontal2");
			vertical = Input.GetAxis("Vertical2");
		}
		velocity.x = horizontal;
		velocity.y = vertical;
		if (horizontal != 0 && vertical != 0){
			velocity.Normalize();
		}
		velocity *= speed;
		exghaustParticles.enableEmission = velocity.sqrMagnitude >= .1f ? true : false;
	}

	/// <summary>
	/// Updates the rotation of the ship
	/// </summary>
	/// <param name="dt">Dt.</param>
	void UpdateRotation(float dt){
		if (velocity.x != 0 || velocity.y != 0){
			float angle;

			/*if (velocity.x == 0){
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
			}*/

			angle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y,velocity.x) - 90;

			if (angle != transform.localEulerAngles.z){
				Vector3 newAngles = new Vector3(0f,0f,Mathf.MoveTowardsAngle(transform.localEulerAngles.z,angle,rotationSpeed));
				transform.localEulerAngles = newAngles;
			}
		}
	}

	void OnLevelWasLoaded(int level){
		StartCoroutine("cFreezeForTime",3.5f);
	}

	IEnumerator cFreezeForTime(float time){
		frozen = true;
		yield return new WaitForSeconds(time);
		frozen = false;
	}
}
