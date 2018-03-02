using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmetics : MonoBehaviour 
{

	public Material[] HairColors;
	public Material[] SkinTone;

	public GameObject Head;
	public GameObject Body;
	public GameObject Hair;

	void Start () 
	{
		Hair.GetComponent<Renderer>().material = HairColors[Random.Range(0, HairColors.Length)];
		
		Material skinTone =  SkinTone[Random.Range(0, SkinTone.Length)];
		Head.GetComponent<Renderer>().material = skinTone;
		Body.GetComponent<Renderer>().material = skinTone;
	}
	
	void Update () 
	{
		
	}
}
