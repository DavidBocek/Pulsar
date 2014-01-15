using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour {

	public int type;

	void OnPulseHit(int type){
		if (type == this.type){
			//kill enemy, perhaps call for respawn again elsewhere?
			Destroy(gameObject);
		}
	}
}
