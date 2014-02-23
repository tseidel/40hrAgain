using UnityEngine;
using System.Collections;

public class staticVars : MonoBehaviour {


	public static Vector3 respawnPos;
	public static bool firstSpawn = true;


	public static void respawn(){

		Application.LoadLevel (Application.loadedLevel);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
