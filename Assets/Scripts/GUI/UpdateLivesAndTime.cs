using UnityEngine;
using System.Collections;

public class UpdateLivesAndTime : MonoBehaviour {
	
	private LevelManager levelManager;
	private GUIText text;
	private bool outOfTimeBeforeScene4 = false;

	void Start(){
		text = GetComponent<GUIText>();
		this.levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
	}

	void Update(){
		levelManager.timeLeft -= Time.smoothDeltaTime;
		if (levelManager.timeLeft > 0){
			text.text = "LIVES :  "+(int)levelManager.lives;
		} else if (!outOfTimeBeforeScene4){
			text.text = "S U D D E N  D E A T H !";
			text.color = new Color(.8f,.2f,.2f);
			text.fontSize = 30;
		} else {
			text.text = "O U T  O F  T I M E !";
			text.color = new Color(.8f,.2f,.2f);
			text.fontSize = 30;
		}
	}

	public void OutOfTimeMessage(){
		outOfTimeBeforeScene4 = true;
	}
}
