using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmetics : MonoBehaviour 
{

	#region Material Parameters
	public Material[] HairColors;
	public Material[] SkinTone;
	public Material[] Shirts;

	public GameObject Head;
	public GameObject Body;
	public GameObject Hair;
	public GameObject Shirt;

	public StaticData.SoccerTeam AudienceTeam{get; private set;}
	#endregion
	void Start () 
	{
		Hair.GetComponent<Renderer>().material = HairColors[Random.Range(0, HairColors.Length)];
		
		Material skinTone =  SkinTone[Random.Range(0, SkinTone.Length)];
		Head.GetComponent<Renderer>().material = skinTone;
		Body.GetComponent<Renderer>().material = skinTone;

		//For Audience
		if(transform.tag != "Player")
		{
			AudienceTeam = Random.Range(0, 2) == 0 ? StaticData.SoccerTeam.BLUE : StaticData.SoccerTeam.RED;
			ChangeShirtColor((int)AudienceTeam);
		}
	}

	public void ChangeShirtColor(int shirtIndex)
	{
		Shirt.GetComponent<Renderer>().material = Shirts[shirtIndex];
	}
}
