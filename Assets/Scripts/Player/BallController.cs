using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public GameObject Ball;
	public GameObject Feet;
	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Ball")
		{
			Ball.GetComponent<Ball>().PickupBall(gameObject);
		}
	}
}
