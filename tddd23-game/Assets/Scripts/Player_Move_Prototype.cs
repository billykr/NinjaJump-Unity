using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Move_Prototype : MonoBehaviour {

    public int playerSpeed = 10;
	private bool facingright = false;
	public bool runningInAir;
    private int playerJump = 1300;
    private float moveX;
	public bool isOnGround;
	private float distancePlayerBottom = 1.2f;
	private float xOffsetEnemy= 1.20f;
	private float yOffsetEnemy= 2f;
	private float distanceToGround = 1.5f;
	private float offSetDist = 0.5f;
	public Transform[] groundPoints;

	public Transform[] rightPoints;

	public float groundRadius;

	public float sideRadius;

	public LayerMask whatIsGround;

	public List<GameObject> Enemies;

	private GameObject enemyDead;






	
	// Update is called once per frame
	void Update () {
        PlayerMove();
		JumpOnHead1();
		RayCastUp ();
		runningInAir = isPlayerRunningInAir();
		isOnGround = isPlayerOnGround ();
	
	}



    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown ("Jump") && isOnGround) {
			isOnGround = false;
			jump ();
		}
        //animation
        //Check which way the player is facing 
        if(moveX < 0 && facingright == false)
        {
            flipPlayer();
        }
        else if(moveX > 0 && facingright){
            flipPlayer();
        }


		if (!runningInAir) {  

			//physics and mevements along the x-axis
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
			gameObject.GetComponent<Animator> ().SetFloat ("speed", Mathf.Abs (moveX));

		}
		if (runningInAir) {
			//Checks if the player is moving, if it isn't the animation should be idle
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
			gameObject.GetComponent<Animator> ().SetFloat ("speed", Mathf.Abs (0));

		}

		



    }

    void jump()
    {
        //jumping code for jumping with a specific force 
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
       
    }

    void flipPlayer()
    {
		
		facingright = !facingright;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x = localScale.x * -1;
		transform.localScale = localScale;

    }





	

	void RayCastUp(){
		RaycastHit2D hit_up = Physics2D.Raycast (transform.localPosition, Vector2.up);

		if (hit_up != null && hit_up.collider != null && hit_up.distance < distancePlayerBottom && hit_up.collider.name == "Box_Unknown") {
			Debug.Log ("Hit box");
			Destroy(hit_up.collider.gameObject);

		}

		RaycastHit2D hit_up_left = Physics2D.Raycast (new Vector3(GameObject.FindGameObjectWithTag ("Player").transform.position.x-offSetDist,
			GameObject.FindGameObjectWithTag ("Player").transform.position.y,0), Vector2.up);


		if (hit_up_left != null && hit_up_left.collider != null && hit_up_left.distance < distancePlayerBottom && hit_up_left.collider.name == "Box_Unknown") {
			Debug.Log ("Hit box left");

			Destroy(hit_up_left.collider.gameObject);

		}


		RaycastHit2D hit_up_right = Physics2D.Raycast (new Vector3(GameObject.FindGameObjectWithTag ("Player").transform.position.x+offSetDist,
			GameObject.FindGameObjectWithTag ("Player").transform.position.y,0), Vector2.up);


		if (hit_up_right != null && hit_up_right.collider != null && hit_up_right.distance < distancePlayerBottom && hit_up_right.collider.name == "Box_Unknown") {
			Debug.Log ("Hit box right");
			Destroy(hit_up_right.collider.gameObject);

		}
			
	}



	void JumpOnHead1(){

		Debug.Log ("test");





		foreach(GameObject Enemy in Enemies){
			if(Enemy != null &&
				Enemy.transform.position.x - xOffsetEnemy <= GameObject.FindGameObjectWithTag ("Player").transform.position.x &&
				GameObject.FindGameObjectWithTag ("Player").transform.position.x <= Enemy.transform.position.x + xOffsetEnemy &&
				Enemy.transform.position.y + yOffsetEnemy < GameObject.FindGameObjectWithTag ("Player").transform.localPosition.y &&
				Enemy.GetComponent<BoxCollider2D> ().enabled == true){
					
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 2000);
					Enemy.GetComponent<BoxCollider2D> ().enabled = false;
					Enemy.GetComponent<Enemy_Move> ().enabled = false;
					int index = Enemies.IndexOf(Enemy);
					Enemies [index] = enemyDead;

		}
				
			} 

		}



	private bool isPlayerRunningInAir(){

			//go through the collisionpoints to the right, only need the right since the player always facing the right way when it's running towords something.
			foreach (Transform point in rightPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, sideRadius, whatIsGround);

				for(int i=0;i<colliders.Length;i++){
				//check so the player doesnt collide with itself or collide with a coin 
				if ((colliders [i].gameObject != gameObject) && (colliders [i].gameObject != GameObject.FindGameObjectWithTag ("coin"))) {
						if (!isOnGround) {
							return true;

						}

					}
			}
		}

		return false;




	}
		
  

	private bool isPlayerOnGround(){


		//checks if the player is falling down or if it is standing still
		if(gameObject.GetComponent<Rigidbody2D> ().velocity.y <=0){

			foreach (Transform point in groundPoints) {

				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for(int i=0;i<colliders.Length;i++){
					//if the current collider isnt the player itself
					if (colliders [i].gameObject != gameObject) {
						//colliding with something that isnt the player
						return true;

					}
				}
			}
		}
		return false;
	}
}
