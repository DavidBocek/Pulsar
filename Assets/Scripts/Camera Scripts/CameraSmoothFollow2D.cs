using UnityEngine;
using System.Collections;

public class CameraSmoothFollow2D : MonoBehaviour {

	public float leftBound; public float rightBound; public float topBound; public float bottomBound;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

		Messenger.Broadcast<Vector2>("parallaxUpdate",new Vector2(transform.position.x,transform.position.y));

		/*Vector3 newPos = transform.position;
		if (transform.position.x+400f > rightBound){
			newPos.x = rightBound - 400f;
		} else if (transform.position.x-400f < leftBound){
			newPos.x = leftBound + 400f;
		} if (transform.position.y+300f > topBound){
			newPos.y = topBound - 300f;
		} else if (transform.position.y-300f < bottomBound){
			newPos.y = bottomBound + 300f;
		}
		if (newPos != transform.position){
			transform.position = newPos;
		}*/
	}
}