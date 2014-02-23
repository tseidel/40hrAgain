using UnityEngine;
using System.Collections;

public class StormTrigger : MonoBehaviour {

	// Use this for initialization
	public int storm;

	
	// Update is called once per frame

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player")
		{
			Camera.main.SendMessage("ChangeWeather",storm);
		}
	}
}
