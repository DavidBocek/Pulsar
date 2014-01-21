using UnityEngine;
using System.Collections;

public class UpdateTimer : MonoBehaviour {
	
	private LevelManager levelManager;
	private GUIText text;

	void Start(){
		text = GetComponent<GUIText>();
		this.levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
	}

	void Update(){
		levelManager.timeLeft -= Time.smoothDeltaTime;
		text.text = "TIME REMAINING: "+(int)levelManager.timeLeft;
	}
}
