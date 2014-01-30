using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public float normalPulseRadius;
	public Transform pulseObjectTrans;
	public int type;
	public bool playing {get; set;}
	public float pulseTime;
	public AudioSource musicSource;
	
	private AudioSource pulseSound;
	private float initialScale;
	private int freq;
	private int samplesPerBeat;

	private float dt;

	// Use this for initialization
	void Start () {
		pulseSound = GetComponent<AudioSource>();
		pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		initialScale = pulseObjectTrans.localScale.x;
		freq = audio.clip.frequency;
		samplesPerBeat = freq/3; //180 bpm => 3 beats per second
		playing = true;
	}

	private bool started = false;
	void Update(){
		dt = Time.smoothDeltaTime;
		if (!started){
			StartCoroutine("SyncToAudio");
			started = true;
		}
	}


	IEnumerator PulseVisual(float radius){
		float newScale;
		Color c = pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().material.color;
		pulseObjectTrans.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
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
	}

	private bool completedInitialDelay = false;
	private int currentSamples = 0;
	private int deltaSamples;
	private int lastSample = 0;
	IEnumerator SyncToAudio(){
		while (playing){
			switch (type){
			case 0:
				//play at the first beat of measures 1, 3, 5, 7, etc.
				//aka every 8 beats
				while (currentSamples < 8 * samplesPerBeat){
					deltaSamples = musicSource.timeSamples - lastSample;
					if (deltaSamples < 0){
						deltaSamples = musicSource.timeSamples;
						lastSample = 0;
					}
					currentSamples += deltaSamples;
					lastSample = musicSource.timeSamples;
					yield return null;
				}
				currentSamples = 0;
				pulseSound.Play ();
				StartCoroutine("PulseVisual",normalPulseRadius);
				break;
			case 1:
				//play at the 3rd beat of every measure
				//aka every 4 beats starting on the 3rd
				if (!completedInitialDelay){
					while (currentSamples < 3 * samplesPerBeat){
						deltaSamples = musicSource.timeSamples - lastSample;
						if (deltaSamples < 0){
							deltaSamples = musicSource.timeSamples;
							lastSample = 0;
						}
						currentSamples += deltaSamples;
						lastSample = musicSource.timeSamples;
						yield return null;
					}
					pulseSound.Play ();
					StartCoroutine("PulseVisual",normalPulseRadius);
					completedInitialDelay = true;
				}
				while (currentSamples < 4 * samplesPerBeat){
					deltaSamples = musicSource.timeSamples - lastSample;
					if (deltaSamples < 0){
						deltaSamples = musicSource.timeSamples;
						lastSample = 0;
					}
					currentSamples += deltaSamples;
					lastSample = musicSource.timeSamples;
					yield return null;
				}
				currentSamples = 0;
				pulseSound.Play();
				StartCoroutine("PulseVisual",normalPulseRadius);
				break;
			case 2:
				//play at the 2nd beat of measures 1, 3, 5, 7, etc. and the 4th beat of measures 2, 4, 6, 8, etc.

				break;
			}
			yield return null;
		}
	}
}
