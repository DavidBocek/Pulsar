using UnityEngine;
using System.Collections;

public class LevelLoadHints : MonoBehaviour {

	private GUIText text;

	// Use this for initialization
	void Start () {
		text = GetComponent<GUIText>();
	}

	void OnLevelWasLoaded(int level){
		text = GetComponent<GUIText>();
		switch (level){
		case 1:
			StartCoroutine("cShowHelpText","WELCOME TO PULSAR! KILL ENEMIES BY LURING THEM INTO PLANETS AT THE RIGHT TIME");
			break;
		case 2:
			StartCoroutine("cShowHelpText","ENEMIES CAN ONLY BE KILLED BY THE SAME COLOR PLANET");
			break;
		default:
			break;
		}
	}

	IEnumerator cShowHelpText(string textToShow){
		text.text = textToShow;
		yield return new WaitForSeconds(4f);
		text.text = "";
	}
}
