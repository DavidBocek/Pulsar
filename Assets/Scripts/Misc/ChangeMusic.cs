using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

	private AudioSource newMusic;

	void OnLevelWasLoaded(int levelIndex){
		newMusic = GameObject.FindWithTag("LeaderboardMusic").GetComponent<AudioSource>();
		StartCoroutine("cFadeInNewMusic");
	}

	IEnumerator cFadeInNewMusic(){
		newMusic.Play();
		for (float i=0; i<.5f; i+=Time.deltaTime){
			newMusic.volume = Mathf.Lerp(0f,1f,i/.5f);
			yield return null;
		}
	}
}
