using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public GameObject Ball;
	public Ball BallComponent { get; private set; }
	public GameObject Feet;
	void Start () 
	{
		BallComponent = Ball.GetComponent<Ball>();
	}
	
	void Update () 
	{
		
	}

	/// <summary>
	/// Kicks the ball with given force
	/// </summary>
	/// <param name="kickForce"></param>
	public void KickBall(float kickForce)
	{
		// Check if player has the ball
		if(GameObject.ReferenceEquals(BallComponent.CurrentHolder, gameObject))
		{
			BallComponent.DropBall();
			Ball.GetComponent<Rigidbody>().AddForce(transform.forward * kickForce, ForceMode.Impulse);
		}
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

	public GameObject CurrentBallHolder()
	{
		return BallComponent.CurrentHolder;
	}
}
