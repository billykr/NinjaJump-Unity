using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame(){
		PlayerPrefs.SetFloat ("PlayerScore", 0f);
		SceneManager.LoadScene ("Prototype_1");

	}
}
