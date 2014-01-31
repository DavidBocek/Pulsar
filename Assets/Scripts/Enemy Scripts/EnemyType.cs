using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour {

	public int type;
	public int scoreValue;

	void OnPulseHit(int type){
		if (type == this.type){
			GameObject.FindWithTag("levelManager").GetComponent<LevelManager>().score += scoreValue;
			//kill enemy, perhaps call for respawn again elsewhere?
			Destroy(gameObject);
		}
	}
}
