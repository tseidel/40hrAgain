using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{
	
	public float moveForce = 6;

	public float InitalJumpForce = 20000;
	public float addJumpForce = 50;
	public int JumpAngle;
	private float jumpAngleRadians;
	int chargelevel;
	public GameObject gcBase;
	public GameObject gcCenter;
	public GameObject gcBaseBig;
	public GameObject gcCenterBig;
	
	public bool grounded = false;
	public bool grounded2 = false;
	public bool readyToJump;

	public Vector2 vel2D;

	public float maxFuel = 20;
	public float curFuel = 100;

	//1 is right, -1 is left
	int moveDir = 1;
	
	public static Vector3 playerPosition;

	public BoxCollider2D bottomBox;
	
	float JumpMovementSupressionTime; // after you jump dont allow movement for a breift period. this stops buggy behaviour where the game does not realize we are not grounded yet

	private Animator spaceAnimator;
	
	// Use this for initialization
	void Start()
	{
		spaceAnimator = this.GetComponent<Animator> ();
		if (!staticVars.firstSpawn)
		{
			this.transform.position = staticVars.respawnPos;
		}
		else
		{
			staticVars.respawnPos = this.transform.position;
		}
		staticVars.firstSpawn = false;
		
		jumpAngleRadians = Mathf.Deg2Rad * JumpAngle;
	}
	
	// Update is called once per frame
	void Update()
	{

		if(rigidbody2D.velocity.y>0){
			bottomBox.enabled = false;
		}
		else{
			bottomBox.enabled = true;
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			moveDir = 1;
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			moveDir = -1;
		}

		this.transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x) * moveDir, transform.localScale.y, transform.localScale.z);

		/*
		//cancels jump
		if(readyToJump && Input.GetKeyDown(KeyCode.DownArrow)){

			//reset transparency
			Color tmp = this.renderer.material.color;
			tmp.a = 1;
			this.renderer.material.color = tmp;

			
			readyToJump = false;

			chargelevel = 0;
		}

		if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
		{
			readyToJump = true;
		}
		
		if (readyToJump && grounded && (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)))
		{
			//reset transparency
			Color tmp = this.renderer.material.color;
			tmp.a = 1;
			this.renderer.material.color = tmp;
			
			this.rigidbody2D.velocity = new Vector2(); // dont cary movement from when you are walking into the jump
			
			readyToJump = false;
			
			float jumpforce = InitalJumpForce * chargelevel / 23f;
			this.rigidbody2D.AddForce(new Vector2(Mathf.Cos(jumpAngleRadians) * jumpforce * moveDir, Mathf.Sin(jumpAngleRadians) * jumpforce));
			chargelevel = 0;
			
			//after we jump dont allow movement for .1second to make sure game calculates grounded correctly
			JumpMovementSupressionTime = Time.time + .1f;
		}
		*/

		if (Input.GetKeyDown(KeyCode.Q))
		{
			Physics2D.gravity = new Vector2(0, -3.71f);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			Physics2D.gravity = new Vector2(0, -9.81f);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			Physics2D.gravity = new Vector2(0, -24.79f);
		}


	}
	
	void FixedUpdate()
	{
		playerPosition = this.transform.position;
		
		grounded = Physics2D.Linecast(gcBase.transform.position, gcCenter.transform.position, 1 << LayerMask.NameToLayer("Ground"));
		grounded2 = Physics2D.Linecast(gcBaseBig.transform.position, gcCenterBig.transform.position, 1 << LayerMask.NameToLayer("Ground")) && rigidbody2D.velocity.y<=0;
		spaceAnimator.SetBool ("Grounded", grounded);
		spaceAnimator.SetBool ("Moving", rigidbody2D.velocity.magnitude > 0.1f);
		/*
		// DONT MOVE WHILE CHARGING JUMP
		if (readyToJump)
		{
			Color blah = this.renderer.material.color;
			if (blah.a > 1f / 2f)
			{
				chargelevel++;
				blah.a = blah.a * 9.7f / 10f; //23/60 of a second to fully charge
			}
			this.renderer.material.color = blah;
			
			//  this.rigidbody2D.velocity = new Vector2();
			return;
		}
		if (Time.time < JumpMovementSupressionTime) { return; } // dont allow movement if we just jumped
		*/


		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && grounded)
		{
			this.rigidbody2D.velocity = new Vector2(moveForce * -1, this.rigidbody2D.velocity.y);
		}
		else if (Input.GetKey(KeyCode.RightArrow) && grounded)
		{
			this.rigidbody2D.velocity = new Vector2(moveForce, this.rigidbody2D.velocity.y);
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && grounded2)
		{
			this.rigidbody2D.velocity = new Vector2(moveForce/5 * -1, this.rigidbody2D.velocity.y);
		}
		else if (Input.GetKey(KeyCode.RightArrow) && grounded2)
		{
			this.rigidbody2D.velocity = new Vector2(moveForce/5, this.rigidbody2D.velocity.y);
		}
		else { }

		/*if(!grounded){
			rigidbody2D.AddForce(new Vector2(Input.GetAxis("Horizontal")*10f,0));
		}*/
		
		
		if(Input.GetKey(KeyCode.Space) && curFuel>0){
			this.rigidbody2D.AddForce(new Vector2( (moveDir * 9.81f * rigidbody2D.mass / Mathf.Tan(jumpAngleRadians)), 9.81f*rigidbody2D.mass));
			//Debug.Log ("hi");
			spaceAnimator.SetBool("Jumping", true);
			curFuel = Mathf.Max (0, curFuel-1);

		} else {
			spaceAnimator.SetBool("Jumping", false);

			if(grounded){
				curFuel = Mathf.Min (maxFuel, curFuel+1);
			}
		}

		vel2D = rigidbody2D.velocity;
	}
	
	void Kill()
	{
		Debug.Log("I is Dead");
		//Do Death YEAH
		
		staticVars.respawn();
	}
}
