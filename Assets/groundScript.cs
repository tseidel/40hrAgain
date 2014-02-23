using UnityEngine;
using System.Collections;

public class groundScript : MonoBehaviour {

	Material sheetMaterial;

	public GameObject particleRock;

	public float tilesize = 10;

	// Use this for initialization
	void Awake () {
		//sheetMaterial = renderer.material; // modify this instance of the material

		renderer.material.mainTextureScale = this.transform.localScale/tilesize;
		renderer.material.mainTextureOffset = (this.transform.position - (this.transform.localScale/2))/tilesize;

		Vector3 rockOffset = this.transform.position + new Vector3(-transform.localScale.x/2 + .5f, transform.localScale.y/2,0);

		for(int i=0; i<this.transform.localScale.x; i++){
			Instantiate(particleRock,new Vector3(rockOffset.x + i, rockOffset.y, this.transform.position.z-1), this.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
