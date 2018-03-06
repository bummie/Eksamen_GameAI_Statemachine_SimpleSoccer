using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public Goal OpponentsGoal {get; private set;}
	public GameObject Ball { get; private set; }
	public Ball BallComponent { get; private set; }
	public GameObject Feet;
	void Start () 
	{
		Ball = GameObject.FindGameObjectWithTag("Ball");
		BallComponent = Ball.GetComponent<Ball>();

		//TODO: Refactor this
		MatchHandler matchHandler = GameObject.FindGameObjectWithTag("MatchHandler").GetComponent<MatchHandler>();
		if(GetComponent<PlayerInfo>().Team == StaticData.SoccerTeam.BLUE)
		{
			OpponentsGoal = matchHandler.Goal_RED.GetComponent<Goal>();
		}else
		{
			OpponentsGoal = matchHandler.Goal_BLUE.GetComponent<Goal>();
		}
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
	/// Kicks the ball with given force and angle
	/// </summary>
	/// <param name="kickForce"></param>
	public void KickBall(float kickForce, float angle)
	{
		// Check if player has the ball
		if(GameObject.ReferenceEquals(BallComponent.CurrentHolder, gameObject))
		{
			BallComponent.DropBall();
			Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.forward;
			Ball.GetComponent<Rigidbody>().AddForce(direction * kickForce, ForceMode.Impulse);
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
			BallComponent.PickupBall(gameObject);
		}
	}

	/// <summary>
	/// Returns the current holder of the ball
	/// </summary>
	/// <returns></returns>
	public GameObject CurrentBallHolder()
	{
		return BallComponent.CurrentHolder;
	}
}
