using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumScreen : MonoBehaviour {

	public GameObject Display;
	public Texture camTopTexture;
	public Texture camBlueTexture;
	public Texture camRedTexture;
	private GameObject _ball;
	
	private Material _displayMaterial;
	public int SectorDistance{get; private set;}
	void Start ()
	{
		_ball = GameObject.FindGameObjectWithTag("Ball");
		SectorDistance = 10;
		_displayMaterial = Display.GetComponent<Renderer>().material;
	}
	
	void Update ()
	{
		// Change to cam based on balls position
		if(_ball.transform.position.x > SectorDistance)
		{
			_displayMaterial.SetTexture("_MainTex", camRedTexture);
		}else if(_ball.transform.position.x < -SectorDistance)
		{
			_displayMaterial.SetTexture("_MainTex", camBlueTexture);
		}else
		{
			_displayMaterial.SetTexture("_MainTex", camTopTexture);
		}
	}
}
