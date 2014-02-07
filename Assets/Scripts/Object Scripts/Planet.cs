using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public float normalPulseRadius;
	public Transform pulseObjectTrans;
	public int type;
	public bool playing {get; set;}
	public float pulseTime;


	private AudioSource musicSource;
	private AudioSource pulseSound;
	private float initialScale;
	private int freq;
	private int samplesPerBeat;
	private int[] soundTimingsInSamples;

	private float dt;

	// Use this for initialization
	void Start () {
		musicSource = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
		pulseSound = GetComponent<AudioSource>();
		pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		pulseObjectTrans.gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
		initialScale = pulseObjectTrans.localScale.x;
		freq = audio.clip.frequency;
		samplesPerBeat = freq/3; //180 bpm => 3 beats per second
		playing = true;
	}

	private bool started = false;
	void Update(){
		dt = Time.smoothDeltaTime;
		if (!started){
			SetUpTimingsArray();
			StartCoroutine("SyncToAudio");
			started = true;
		}
	}


	IEnumerator PulseVisual(float radius){
		float newScale;
		Color c = pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().material.color;
		pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
		pulseObjectTrans.gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
		for (float t = 0; t<=1f; t += dt/pulseTime){
			//for right now, lerp between 0 and 1 alpha
			c.a = t;
			pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().material.color = c;

			newScale = Mathf.Lerp(initialScale,radius,t);
			pulseObjectTrans.localScale = new Vector3(newScale,newScale,.1f);
			t += dt/pulseTime;
			yield return null;
		}
		for (float t = 1; t>=0f; t -= dt/pulseTime){
			//then back from 1 to 0
			c.a = t;
			pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().material.color = c;

			newScale = Mathf.Lerp (initialScale,radius, t);
			pulseObjectTrans.localScale = new Vector3(newScale,newScale,.1f);
			t -= dt/pulseTime;
			yield return null;
		}
		pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		pulseObjectTrans.gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
	}


	void SetUpTimingsArray(){
		switch (type){
		case 0:
			//play at the first beat of measures 1, 3, 5, 7, etc.
			//aka every 8 beats
			//plays 123 times total (990 beats / 8 beats per play)
			soundTimingsInSamples = new int[123];
			for (int i=0; i<123; i++){
				soundTimingsInSamples[i] = i * 8 * samplesPerBeat;
			}
			break;
		case 1:
			//play at the 6th 8th note of every measure
			//aka every 4 beats starting on the 2.5th
			//plays 247 times total (990 beats / 4 beats per play)
			soundTimingsInSamples = new int[247];
			for (int i=0; i<247; i++){
				soundTimingsInSamples[i] = (int) ((i * 4 + 2.5) * samplesPerBeat);
			}
			break;
		case 2:
			//play at the 2nd beat of measures 1, 3, 5, 7, etc. and the 4th beat of measures 2, 4, 6, 8, etc.
			//plays 247 times total (990 beats / 4 beats per measure, 1 play per measure)
			soundTimingsInSamples = new int[247];
			for (int i=0; i<247; i++){
				if (i%2 == 0){
					soundTimingsInSamples[i] = (i * 4 + 1) * samplesPerBeat;
				} else {
					soundTimingsInSamples[i] = (i * 4 + 3) * samplesPerBeat;
				}
			}
			break;
		}
	}


	/*private bool completedInitialDelay = false;
	private int currentSamples = 0;
	private int deltaSamples;
	private int lastSample = 0;*/
	IEnumerator SyncToAudio(){
		int playNumber = 0;
		while (playing){
			while (musicSource.timeSamples < soundTimingsInSamples[playNumber]){
				yield return null;
			}
			playNumber ++;
			pulseSound.Play();
			StartCoroutine("PulseVisual",normalPulseRadius);
		}
	}
}
