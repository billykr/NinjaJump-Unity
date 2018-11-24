using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

	public bool hasDied;
	public int health;

	// Use this for initialization
	void Start () {
		hasDied = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -20) {
			hasDied = true;	
		}
		if (hasDied == true) {
			StartCoroutine ("Die");
		}
	}


	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "water" || collision.gameObject.tag == "lava") {
			Debug.Log ("Has died");
			hasDied = true;
			StartCoroutine ("Die");


		}
	}

	IEnumerator Die (){
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
		yield return null;

	}

}
