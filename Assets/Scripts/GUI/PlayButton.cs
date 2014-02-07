using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public float width;
	public float height;

	void OnGUI(){
		/*if (GUI.Button(new Rect(Screen.width/2-width/2-75f,Screen.height/2+100f,width,height),"1 PLAYER")){
			LevelManager.playersToStart = 1;
			Application.LoadLevel("scene1");
		}
		if (GUI.Button(new Rect(Screen.width/2-width/2+75f,Screen.height/2+100f,width,height),"2 PLAYER")){
			LevelManager.playersToStart = 2;
			Application.LoadLevel("scene1");
		}*/
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			LevelManager.playersToStart = 1;
			Application.LoadLevel("scene1");
		} else if (Input.GetKeyDown(KeyCode.Alpha2)){
			LevelManager.playersToStart = 2;
			Application.LoadLevel("scene1");
		}
	}
}
