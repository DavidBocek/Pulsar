using UnityEngine;
using System.Collections;

public class LevelLoadHints : MonoBehaviour {

	private GUIText text;
	private GUIText readySetGo;

	// Use this for initialization
	void Start () {
		text = GetComponent<GUIText>();
	}

	void OnLevelWasLoaded(int level){
		text = GetComponent<GUIText>();
		readySetGo = GameObject.FindWithTag ("ReadySetGo").GetComponent<GUIText>();
		switch (level){
		case 1:
			StartCoroutine("cShowHelpText","WELCOME TO PULSAR! KILL ENEMIES BY LURING THEM\n INTO PLANETS AT THE RIGHT BEAT");
			StartCoroutine("cReadySetGo");
			break;
		case 2:
			StartCoroutine("cShowHelpText","ENEMIES CAN ONLY BE KILLED\n BY THE SAME COLOR PLANET");
			StartCoroutine("cReadySetGo");
			break;
		case 3:
			StartCoroutine("cShowHelpText","PICKUP AND DROP GREEN MINES");
			StartCoroutine("cReadySetGo");
			break;
		case 4:
			StartCoroutine("cShowHelpText","GOOD LUCK");
			StartCoroutine("cReadySetGo");
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

	IEnumerator cReadySetGo(){
		readySetGo.text = "READY";
		yield return new WaitForSeconds (1.5f);
		readySetGo.text = "SET";
		yield return new WaitForSeconds (1.5f);
		readySetGo.text = "GO!";
		yield return new WaitForSeconds (1f);
		readySetGo.text = "";
	}
}
