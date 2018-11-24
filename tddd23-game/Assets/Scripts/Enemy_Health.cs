using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Health : MonoBehaviour {



	void Update () {
		if (gameObject.transform.position.y < -10 && gameObject != null) {
			Destroy (gameObject);
		}

	}



}
