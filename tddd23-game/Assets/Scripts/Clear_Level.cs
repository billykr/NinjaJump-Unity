using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear_Level : MonoBehaviour {

	private bool isOpen;
	private float totalScore;


	// Use this for initialization
	void Start () {
		isOpen = false;

	}

	// Update is called once per frame
	void Update () {

	}


	void calculateScoreAfterCompletedLevel(){
		

		totalScore = PlayerPrefs.GetFloat ("PlayerScore");
		totalScore += GameObject.Find ("Player").GetComponent<Player_score> ().playerScore + (int)Mathf.Round (GameObject.Find ("Player").GetComponent<Player_score> ().timeLeft * 10);
		PlayerPrefs.SetFloat("PlayerScore", totalScore);
		Debug.Log ("Total Score: "+totalScore);



	}
		



	void OnTriggerEnter2D(Collider2D triggerCollision){ 


		if (triggerCollision.gameObject.tag == "key") {

			Debug.Log ("Colliding with key");
			isOpen = true;
			Destroy (GameObject.FindGameObjectWithTag ("key"));



		}


		if (triggerCollision.gameObject.name == "EndLevel" && isOpen) {



			switch (SceneManager.GetActiveScene ().name)
			{
			case "Prototype_1":
				calculateScoreAfterCompletedLevel ();
				SceneManager.LoadScene ("level1");
				break;
			case "level1":

				calculateScoreAfterCompletedLevel ();
				SceneManager.LoadScene ("level2");
				break;

			case "level2":
				calculateScoreAfterCompletedLevel ();
				SceneManager.LoadScene ("level3");
				break;

			case "level3":
				calculateScoreAfterCompletedLevel ();
				SceneManager.LoadScene ("level4");
				break;

			case "level4":
				calculateScoreAfterCompletedLevel ();
				SceneManager.LoadScene ("GameCompleted");
				break;
			default:
				Debug.Log("Default case");
				SceneManager.LoadScene ("Prototype_1");
				break;
			}


		}
	}
		
			
}
