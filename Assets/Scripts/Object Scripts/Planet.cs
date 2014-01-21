using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public float normalPulseRadius;
	public float tempPulseDelay;
	public float pulseTime;
	public Transform pulseObjectTrans;

	private float tempPulseTimer;
	private AudioSource pulseSound;
	private float initialScale;

	private float dt;

	// Use this for initialization
	void Start () {
		pulseSound = GetComponent<AudioSource>();
		tempPulseTimer = tempPulseDelay;
		pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().enabled = false;
		initialScale = pulseObjectTrans.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.smoothDeltaTime;
		if (tempPulseTimer<= 0){
			pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().enabled = true;
			StartCoroutine("Pulse",normalPulseRadius);
			pulseSound.Play();
			tempPulseTimer = tempPulseDelay;
		} else {
			tempPulseTimer -= dt;
		}
	}


	IEnumerator Pulse(float radius){
		float newScale;
		Color c = pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().material.color;
		for (float t = 0; t<=1f; t += dt/pulseTime){
			//for right now, lerp between 0 and 1 alpha
			c.a = t;
			pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().material.color = c;

			newScale = Mathf.Lerp(initialScale,radius,t);
			pulseObjectTrans.localScale = new Vector3(newScale,newScale,.1f);
			t += dt/pulseTime;
			yield return null;
		}
		for (float t = 1; t>=0f; t -= dt/pulseTime){
			//then back from 1 to 0
			c.a = t;
			pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().material.color = c;

			newScale = Mathf.Lerp (initialScale,radius, t);
			pulseObjectTrans.localScale = new Vector3(newScale,newScale,.1f);
			t -= dt/pulseTime;
			yield return null;
		}
		pulseObjectTrans.gameObject.GetComponent<MeshRenderer>().enabled = false;
	}


}
