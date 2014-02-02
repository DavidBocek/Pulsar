using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public float width;
	public float height;

	void OnGUI(){
		if (GUI.Button(new Rect(Screen.width/2-width/2,Screen.height/2+100f,width,height),"PLAY GAME")){
			Application.LoadLevel("scene1");
		}
	}
}
