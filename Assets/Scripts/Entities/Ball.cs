using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{

	public GameObject CurrentHolder { get; set; }
	private Transform _oldParent;
	void Start ()
	{
		_oldParent = transform.parent;
	}
	
	void Update ()
	{
		
	}

	/// <summary>
	/// Sets the "carrier" of the ball to the given player
	/// </summary>
	/// <param name="player"></param>
	public void PickupBall(GameObject player)
	{
		CurrentHolder = player;
		gameObject.transform.parent = CurrentHolder.GetComponent<BallController>().Feet.transform;
		transform.localPosition = Vector3.zero;
		transform.GetComponent<Rigidbody>().isKinematic = true;
	}

	public void DropBall()
	{
		CurrentHolder = null;
		transform.GetComponent<Rigidbody>().isKinematic = false;
	}
}
