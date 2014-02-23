using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

	public GameObject startText;
	public GameObject exitText;

	//change if adding more options
	int numItems = 2;

	public int selection = 0;

	// Use this for initialization
	void Start () {
		staticVars.firstSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			selection = (selection+1)%numItems;
		}
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.DownArrow)){
			selection = (selection+numItems-1)%numItems;
		}


		if(selection == 0){
			startText.GetComponent<TextMesh>().color = Color.red;

			if(Input.GetKeyDown(KeyCode.Return)){
				Application.LoadLevel(1);
			}
		}
		else{
			startText.GetComponent<TextMesh>().color = Color.white;
		}

		if(selection == 1){
			exitText.GetComponent<TextMesh>().color = Color.red;

			if(Input.GetKeyDown(KeyCode.Return)){
				Application.Quit();
			}
		}
		else{
			exitText.GetComponent<TextMesh>().color = Color.white;
		}

	}
}
