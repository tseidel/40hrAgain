using UnityEngine;
using System.Collections;

public class EnemyJumper : MonoBehaviour {

	// Use this for initialization
	bool grounded = false;
	
	public GameObject gcBase;
	public GameObject gcCenter;
	public float addJumpForce;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(grounded)
		{
			this.rigidbody2D.velocity = new Vector2(0,addJumpForce);
		}
	}

	void FixedUpdate()
	{
		grounded = Physics2D.Linecast (gcBase.transform.position, gcCenter.transform.position, 1 << LayerMask.NameToLayer("Ground"));
		/*if(!grounded && rigidbody2D.velocity.y > 0){
			this.rigidbody2D.AddForce(new Vector2(0,addJumpForce));
		}*/
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag == "Player")
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
