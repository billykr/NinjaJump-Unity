using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_score : MonoBehaviour {
	

	public float timeLeft = 60;
	public int playerScore = 0;
	public int totalScore;
	public GameObject timeLeftUI;
	public GameObject ScoreUI;
	private float endTime = 0.0000001f;

	private int scoreJumpLimit = 30;

	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		timeLeftUI.gameObject.GetComponent<Text> ().text = ("Time Left: " + (int)Mathf.Round(timeLeft));
		ScoreUI.gameObject.GetComponent<Text> ().text = ("Score: " + playerScore);

		if (timeLeft < endTime) {
			switch (SceneManager.GetActiveScene ().name)
			{
			case "Prototype_1":
				SceneManager.LoadScene ("Prototype_1");
				break;
			case "level1":
				SceneManager.LoadScene ("level1");
				break;

			case "level2":
				SceneManager.LoadScene ("level2");
				break;
			case "level3":
				SceneManager.LoadScene ("level3");
				break;
			case "level4":
				SceneManager.LoadScene ("level4");
				break;


			default:
				Debug.Log("Default case");
				SceneManager.LoadScene ("Prototype_1");
				break;
			}


		}
			
	}


	void OnTriggerEnter2D(Collider2D triggerCollision){

		if (triggerCollision.gameObject.name == "Coin") {
			playerScore += 10;
			Destroy (triggerCollision.gameObject);

		}
			
	}



	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "jump" && playerScore >= scoreJumpLimit) {
			Debug.Log ("JUMMMPMPMPMPMPMPMPM");
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 3000);

		}

	}


	void calculateScore(){
		playerScore += (int)Mathf.Round(timeLeft * 10);
		Debug.Log ("Player score" + playerScore);
	}



		

		
}
