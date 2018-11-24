using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {

	private int enemySpeed = 2;
	private int moveDirection = 1;

	public Vector2 direction;
	public float yOffsetEnemy = 2f;




	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveDirection, 0) * enemySpeed;
		
	}




	//checking for collision with a box to see if the enemy should turn around
	void OnCollisionEnter2D(Collision2D collision){
 
		if (collision.gameObject.tag == "box" || collision.gameObject.tag == "enemy") {
			enemySpeed *= -1;
			Vector2 localScale = gameObject.transform.localScale;
			localScale.x = localScale.x * -1;
			transform.localScale = localScale;
		}




		//Check if the enemy is colliding with the player and that the player doesnt collide with the enemys head
		if (collision.gameObject.tag == "Player" && (gameObject.GetComponent<Rigidbody2D> ().transform.position.y+yOffsetEnemy > GameObject.FindGameObjectWithTag ("Player").transform.localPosition.y)
			&& collision.gameObject != null){ 
			Debug.Log("DESTROIUUUU");
			Debug.Log("enemy y:" + gameObject.GetComponent<Rigidbody2D> ().transform.position.y+yOffsetEnemy);
			Debug.Log("player y"+  GameObject.FindGameObjectWithTag ("Player").transform.localPosition.y);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1000);

			GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Move_Prototype> ().enabled = false;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D> ().enabled = false;


		}

		 
			
		
	}



}
