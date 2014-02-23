using UnityEngine;
using System.Collections;

public class PlanetImageShift : MonoBehaviour {

	// Use this for initialization
	public Sprite Earth;
	public Sprite Venus;
	public Sprite Mars;
	public Sprite Neptune;
	public Sprite Uranus;
	public Sprite Jupider;
	public Sprite Mercury;
	public Sprite Pluto;
	
	SpriteRenderer mySprite;
	int pos;

	void Start () {
		mySprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.gravity.y == -9.81f)
		{
			mySprite.sprite = Earth;
			pos = 0;
			Camera.main.SendMessage("ChangeWeather",pos);
		}
		else if(Physics2D.gravity.y == -3.71f)
		{
			mySprite.sprite = Mars;
			Camera.main.SendMessage("ChangeWeather",1);
		}
		else if(Physics2D.gravity.y == -24.79f)
		{
			mySprite.sprite = Jupider;
			Camera.main.SendMessage("ChangeWeather",2);
		}
		else if(Physics2D.gravity.y == -8.87f)
		{
			mySprite.sprite = Venus;
		}
		else if(Physics2D.gravity.y == -11.15f)
		{
			mySprite.sprite = Neptune;
		}
		else if(Physics2D.gravity.y == -3.7f)
		{
			mySprite.sprite = Mercury;
		}
		else if(Physics2D.gravity.y == -.58f)
		{
			mySprite.sprite = Pluto;
		}
		else if(Physics2D.gravity.y == -3.7f)
		{
			mySprite.sprite = Mercury;
		}
	}
}
