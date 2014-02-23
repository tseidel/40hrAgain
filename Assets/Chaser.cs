using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour {

	// Use this for initialization
	public float movementSpeed;

	float difference;
	float direction = 0;
	float prevDirection = 0;
	bool WallCollide = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		difference = (playerScript.playerPosition.x - this.transform.position.x);

		// get direction sign
		direction = (difference/ Mathf.Abs(difference));

		//change difference absolute difference
		difference = Mathf.Abs(difference);

		if(difference <= 20.0f)
		{
			if(prevDirection == direction && WallCollide)
			{
				//do nothing
			}
			else
			{
				if(difference <= .1f)
				{
					// sets position exactly equual to player's when close
					this.transform.position = new Vector3(playerScript.playerPosition.x,
					                                      this.transform.position.y,
					                                      this.transform.position.z);
				}
				else
				{
					//apply movement
					this.transform.position += Vector3.right * direction * movementSpeed;
				}
				prevDirection = direction;
				WallCollide = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Player")
		{
			WallCollide = true;
		}
		else
		{
			if(other.rigidbody2D.velocity.y <= -12.0f)
			{
				Destroy(this.gameObject);
			}
			else
			{
				other.SendMessage("Kill");
			}
		}
	}

}