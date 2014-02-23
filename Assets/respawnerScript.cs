using UnityEngine;
using System.Collections;

public class respawnerScript : MonoBehaviour {


	public GameObject respawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		/*if(other.tag == "Player"){
			other.SendMessage("updateRespawn", respawnPoint.transform.position);
		}*/

		staticVars.respawnPos = respawnPoint.transform.position;
	}
}
