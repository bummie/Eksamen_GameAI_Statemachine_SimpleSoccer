using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour 
{
	public GameObject GoalPostLeft, GoalPostRight;
	public StaticData.SoccerTeam GoalTeam;
	public Vector3 GoalCenter{get; private set;}
	private MatchHandler _matchHandler;
	private float _magicPostZPosition = 3;
	void Start()
	{
		FindGoalCenter();
		_matchHandler = GameObject.FindGameObjectWithTag("MatchHandler").GetComponent<MatchHandler>();
	}

	/// <summary>
	/// Check if ball is inside goal
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ball")
		{
			_matchHandler.GoalScored(GoalTeam);
		}
	}


	/// <summary>
	/// Retuns a random point between the goal posts
	/// </summary>
	/// <returns></returns>
	public Vector3 RandomPointBetweenPosts()
	{
		Vector3 leftPost = GoalPostLeft.transform.position;
		leftPost.y = 0f;
		Vector3 rightPost = GoalPostRight.transform.position;
		rightPost.y = 0f;
		
		return Vector3.Lerp(leftPost, rightPost, Random.value);
	}

	/// <summary>
	/// Gives a point between the goal posts based on ballposition
	/// </summary>
	/// <param name="ballPosition"></param>
	/// <returns></returns>
	public Vector3 PointBetweenPosts(Vector3 ballPosition)
	{
		Vector3 pos = GoalCenter;
		if(GoalCenter.x < 0)
		{
			pos.x += 1f; //Random.Range(0,3f);
		}else
		{
			pos.x -= 1f; //Random.Range(0,3f);
		}
		
		pos.z = ballPosition.z;

		if(pos.z > _magicPostZPosition)
		{
			pos.z = _magicPostZPosition;
		}else if(pos.z < -_magicPostZPosition)
		{
			pos.z = -_magicPostZPosition;
		}

		return pos;

	}

	/// <summary>
	/// Finds the center of the goal
	/// </summary>
	private void FindGoalCenter()
	{
		Vector3 leftPost = GoalPostLeft.transform.position;
		leftPost.y = 0f;
		Vector3 rightPost = GoalPostRight.transform.position;
		rightPost.y = 0f;

		GoalCenter = (leftPost + rightPost)/2f;
	}
}
