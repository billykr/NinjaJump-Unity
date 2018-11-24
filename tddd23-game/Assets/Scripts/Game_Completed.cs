using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Completed : MonoBehaviour {
	
	public GameObject ScoreUI;
	private float finalScore;



	void Update () {
		finalScore = PlayerPrefs.GetFloat ("PlayerScore");
		Debug.Log ("hej :)2"+finalScore);
		ScoreUI.gameObject.GetComponent<Text> ().text = ("Total Score: " + finalScore);
	
	}
}
