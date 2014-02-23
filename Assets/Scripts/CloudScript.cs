using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {

	// Use this for initialization
	public float cloudSpeed;
	public float MinCloudDuration;
	public float MaxCloudDuration;
	public float fadeTime = 15.0f;
	
	bool die = false;
	bool born = true;

	Color changeColor;
	float alpha_minus = 0;
	float alpha_plus = 0;
	float CloudDur = 0;

	void Start () {
		CloudDur += Time.time + Random.Range(MinCloudDuration,MaxCloudDuration);

		this.gameObject.renderer.material.color = Color.clear;
		//born = true;
		//die = false;
	}
	
	// Update is called once per frame
	void Update () {


		//fade out
		if(die)
		{
			Death();
			if(changeColor.a <= 0)
			{
				//this.renderer.enabled = false;
				Destroy(this.gameObject);
			}
		}
		else if(born)
		{
			Birth();
			alpha_minus = 1 - alpha_plus;
			if(alpha_minus < 0)
			{
				alpha_minus = 0;
			}
			if(changeColor.a >= 1)
			{
				born = false;
			}
		}
		else
		{
			changeColor = DayLight.curColor;
			changeColor.a = 1.0f;
		}

		this.renderer.material.color = changeColor;
		this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * cloudSpeed,Space.World);

		if(CloudDur - Time.time <= 0 && !die)
		{
			die = true;
		}
	}

	void Death()
	{
		changeColor = DayLight.curColor;
		changeColor.a = 1.0f;
		changeColor.a -= alpha_minus;
		alpha_minus += (Time.deltaTime/fadeTime);
	}

	void Birth()
	{
		changeColor = DayLight.curColor;
		changeColor.a = 0;
		changeColor.a += alpha_plus;
		alpha_plus += (Time.deltaTime/fadeTime);
	}
	
}
