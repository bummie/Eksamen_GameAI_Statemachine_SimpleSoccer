using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceWave : MonoBehaviour {

	private Vector3 _pos;
	private float _originalY;
	void Start () 
	{
		_pos = transform.position;
		_originalY = _pos.y;
	}
	
	void Update () 
	{
		//_pos.y = _originalY + Random.Range(0f, 1f);//(Mathf.Sin(Time.time) + _originalY);
		//transform.position = _pos;
	}
}
