using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour {

	public int type;
	public int scoreValue;
	public AudioClip[] deathSounds;
	public ParticleSystem deathEmitter; public int emitCount;
	public bool alreadyDied = false;

	void OnPulseHit(int type){
		if (type == this.type && !alreadyDied){
			GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>().score += scoreValue;
			AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0,deathSounds.Length-1)],transform.position,1f);
			deathEmitter.Emit(emitCount);
			//kill enemy, perhaps call for respawn again elsewhere?
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<KillOnTouch>().enabled = false;
			GetComponent<FollowTarget>().enabled = false;
			Destroy(gameObject,.25f);
			alreadyDied = true;
		}
	}
}
