using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour {

	public int type;
	public int scoreValue;
	public AudioClip[] deathSounds;
	public ParticleSystem deathEmitter; public int emitCount;
	public bool alreadyDied = false;
	public GameObject pointsPopup;

	void OnPulseHit(int type){
		if (type == this.type && !alreadyDied){
			LevelManager levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
			if (!levelManager.gameEnded){
				levelManager.score += scoreValue;
			}
			AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0,deathSounds.Length-1)],transform.position,1f);
			deathEmitter.Emit(emitCount);
			//kill enemy, perhaps call for respawn again elsewhere?
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<KillOnTouch>().enabled = false;
			GetComponent<FollowTarget>().enabled = false;
			Destroy(gameObject,.4f);
			alreadyDied = true;
			pointsPopup.transform.Rotate(-transform.localEulerAngles);
			pointsPopup.GetComponent<MeshRenderer>().enabled = true;
			StartCoroutine("cRise");
		}
	}

	IEnumerator cRise(){
		for (float i=0;i<.4;i+=Time.deltaTime){
			Vector3 temp = new Vector3(transform.position.x,transform.position.y + .1f,transform.position.z);
			pointsPopup.transform.position = temp;
			yield return null;
		}
	}
}
