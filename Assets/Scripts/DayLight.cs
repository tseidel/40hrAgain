using UnityEngine;
using System.Collections;

[System.Serializable]
public class Storm
{
	//Day Cycle transition vars
	public float WeatherTransitionSpeed;
	public float DayCycleTime;
	
	//cloud attributes
	public GameObject Cloud;

	public int NumCloudsPerSpawn;
	public float TimeBetweenCloudSpawns;

	public float minCloudRadiusRange = 5.0f;
	public float maxCloudRadiusRange = 10.0f;
	public float MinCloudHeight;
	public float MaxCloudHeight;

	public Color MorningCloudColor;
	public Color EveningCloudColor;
	
	public bool stars;
	public ParticleSystem starParticle;
	public Vector3 StarSpawnPos;

	public float LightIntensity;
	public Color MorningLightColor;
	public Color EveningLightColor;

	public bool Precipitate;
	public ParticleSystem PrecipitateParticle;
	public Vector3 PrecipitateSpawnPos;

	public Color MorningSkyColor;
	public Color EveningSkyColor;
};

public class DayLight : MonoBehaviour {
	
	//used to to sendmessage to clouds
	public static Color curColor;
	public Light LightSource;

	
	float cloudTime;
	public Storm[] weather;
	float MergeTime = 0.0f;
	float curTime = 0.0f;
	float HalfDay;

	bool morn = true;
	bool eve = false;

	int Pos = 0;
	int prevPos = 0;

	bool Wchange = false;
	float timeClock;

	Color newLight;
	Color newCloud;
	Color newbackground;

	Color oldBackgroundColor;
	Color oldCloudColor;

	ParticleSystem StarSystem;
	ParticleSystem PrecipitateSystem;
	Light OldLight;

	void Start () {
		if(weather.Length  <= 0)
		{
			Debug.LogError("weather must have atleast one weather condition to work, if you would like no weather conditions, remove this script");
			return;
		}
		HalfDay = weather[Pos].DayCycleTime/2.0f;
		cloudTime = weather[Pos].TimeBetweenCloudSpawns + Time.time;
		camera.backgroundColor = weather[Pos].MorningSkyColor;
		if(weather[Pos].stars)
		{
			StarSystem = Instantiate(weather[Pos].starParticle,
			                         weather[Pos].StarSpawnPos,
			                         weather[Pos].starParticle.transform.rotation) as ParticleSystem;

			StarSystem.enableEmission = false;
		}
		if(weather[Pos].Precipitate)
		{
			PrecipitateSystem = Instantiate(weather[Pos].PrecipitateParticle,
			                               weather[Pos].PrecipitateSpawnPos,
			                               weather[Pos].PrecipitateParticle.transform.rotation) as ParticleSystem;

			PrecipitateSystem.enableEmission = true;
		}
		LightSource.intensity = weather[Pos].LightIntensity;
	}
	
	// Update is called once per frame
	void Update () {
		//handels cloud spawn rate
		if(Wchange)
		{
			WeatherUpdate();
		}
		else
		{
			if(cloudTime - Time.time <= 0)
			{
				SpawnClouds(weather[Pos].NumCloudsPerSpawn);
				cloudTime = weather[Pos].TimeBetweenCloudSpawns + Time.time;
			}

			//handels morning evening cycle
			if(morn)
			{

				if(curTime <= 1.0f)
				{
					//alter lighting
					LightSource.color = Color.Lerp(weather[Pos].MorningLightColor,
					                       weather[Pos].EveningLightColor,
					                       curTime);

					curColor = Color.Lerp(weather[Pos].MorningCloudColor,
					                      weather[Pos].EveningCloudColor,
					                      curTime);
					// alter skybox + stars

					camera.backgroundColor = Color.Lerp(weather[Pos].MorningSkyColor,
					                                    weather[Pos].EveningSkyColor,
					                                    curTime);
					timeClock = (curTime*180 + 60);
					LightSource.transform.eulerAngles = new Vector3(timeClock,0,0);
				}
				if(curTime >= .75f)
				{
					if(weather[Pos].stars)
					{
						StarSystem.enableEmission = true;
					}
				}
				if(curTime >= 1.0f)
				{
					eve = true;
					morn = false;
					curTime = 0.0f;
				}
			}
			if(eve)
			{

				if(curTime <= 1.0)
				{
					camera.backgroundColor = Color.Lerp(weather[Pos].EveningSkyColor,
					                                    weather[Pos].MorningSkyColor,
					                                    curTime);

					//alter lighting
					LightSource.color = Color.Lerp(weather[Pos].EveningLightColor,
					                       weather[Pos].MorningLightColor,
					                       curTime);
					
					curColor = Color.Lerp(weather[Pos].EveningCloudColor,
					                      weather[Pos].MorningCloudColor,
					                      curTime);

					timeClock = (curTime*180 + 240)%360;

					LightSource.transform.eulerAngles = new Vector3(timeClock,0,0);
				}
				if(curTime >= .3f)
				{
					if(weather[Pos].stars)
					{
						StarSystem.enableEmission = false;
					}
				}

			
				if(curTime >= 1.0f)
				{
					eve = false;
					morn = true;
					curTime = 0.0f;
				}
			}
			curTime += Time.deltaTime/HalfDay;
		}

	}

	void SpawnClouds(int NumC)
	{
		while(NumC != 0)
		{
			Vector2 InnerunitCirc =  Random.insideUnitCircle.normalized;
		
			Vector2 OuterunitCirc =  InnerunitCirc;

			InnerunitCirc *= weather[Pos].minCloudRadiusRange;
			OuterunitCirc *= weather[Pos].maxCloudRadiusRange;
		
			Vector3 randPos = new Vector3 (Random.Range(InnerunitCirc.x,OuterunitCirc.x),
			                               Random.Range(weather[Pos].MinCloudHeight,weather[Pos].MaxCloudHeight),
			                               Random.Range(InnerunitCirc.y,OuterunitCirc.y));

			Instantiate(weather[Pos].Cloud,
			            randPos,
			            weather[Pos].Cloud.transform.rotation);
			NumC--;
		}
	}

	void ChangeWeather(int Position)
	{
		if(Position == Pos)
		{
			return;
		}
		if(weather.Length <= Position)
		{
			Debug.LogError("the specified weather does not exist, you are accessing a point out of the array bounds");
			return;
		}
		if(weather[Pos].stars)
		{
			Destroy(StarSystem);
		}
		if(weather[Pos].Precipitate)
		{
			Destroy(PrecipitateSystem);
		}

		prevPos = Pos;
	

		Pos = Position;

		HalfDay = weather[Pos].DayCycleTime/2.0f;
		cloudTime = weather[Pos].TimeBetweenCloudSpawns + Time.time;
		if(weather[Pos].Precipitate)
		{
			weather[Pos].PrecipitateParticle.enableEmission = false;
		}
		Wchange = true;


		if(weather[Pos].stars)
		{

			StarSystem = Instantiate(weather[Pos].starParticle,
			                         weather[Pos].StarSpawnPos,
			                         weather[Pos].starParticle.transform.rotation) as ParticleSystem;
			if(morn)
			{
				StarSystem.enableEmission = false;
			}
			else
			{
				StarSystem.enableEmission = true;
			}
		}
	
		if(morn)
		{
			newLight = Color.Lerp(weather[Pos].MorningLightColor,weather[Pos].EveningLightColor,curTime);

			newCloud = Color.Lerp(weather[Pos].MorningCloudColor,weather[Pos].EveningCloudColor,curTime);

			newbackground = Color.Lerp(weather[Pos].MorningSkyColor,weather[Pos].EveningSkyColor,curTime);

			oldCloudColor = Color.Lerp(weather[prevPos].MorningCloudColor,weather[prevPos].EveningCloudColor,curTime);
		}
		else
		{
			newLight = Color.Lerp(weather[Pos].EveningLightColor,weather[Pos].MorningLightColor,curTime);
			
			newCloud = Color.Lerp(weather[Pos].EveningCloudColor,weather[Pos].MorningCloudColor,curTime);
			
			newbackground = Color.Lerp(weather[Pos].EveningSkyColor,weather[Pos].MorningSkyColor,curTime);

			oldCloudColor = Color.Lerp(weather[prevPos].EveningCloudColor,weather[prevPos].MorningCloudColor,curTime);

		}

		oldBackgroundColor = camera.backgroundColor;
		OldLight = LightSource;

	}

	private void WeatherUpdate()
	{


		camera.backgroundColor = Color.Lerp(oldBackgroundColor,newbackground,
			                                    MergeTime);
			
		LightSource.color = Color.Lerp(OldLight.color,newLight,MergeTime);

		LightSource.intensity = Mathf.Lerp(weather[prevPos].LightIntensity,weather[Pos].LightIntensity,MergeTime);

		curColor = Color.Lerp(oldCloudColor,newCloud,MergeTime);

		
		MergeTime += Time.deltaTime/weather[Pos].WeatherTransitionSpeed;
		
		if(MergeTime >= 1.0f)
		{
			Wchange = false;
			MergeTime = 0.0f;

			if(weather[Pos].Precipitate)
			{
				PrecipitateSystem = Instantiate(weather[Pos].PrecipitateParticle,
				                               weather[Pos].PrecipitateSpawnPos, 
				                               weather[Pos].PrecipitateParticle.transform.rotation) as ParticleSystem;

				PrecipitateSystem.enableEmission = true;
			}
		}
	}


	
}
