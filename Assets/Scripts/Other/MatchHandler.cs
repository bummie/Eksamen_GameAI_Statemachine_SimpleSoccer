using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour 
{
	public GameObject Goal_BLUE, Goal_RED;
	public int PointsBlue{get; private set;}
	public int PointsRed{get; private set;}

	public GameObject[] AllPlayers{get; private set;}
	public GameObject Ball{get; private set;}
	void Start () 
	{
		AllPlayers = GameObject.FindGameObjectsWithTag("Player");
		Ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () 
	{
		//TODO: Start game if all players are in ready state
	}

	public void GoalScored(StaticData.SoccerTeam team)
	{
		if(team == StaticData.SoccerTeam.BLUE)
		{
			PointsRed++;
		}else
		{
			PointsBlue++;
		}

		ResetGame();
	}

	/// <summary>
	/// Resets the game
	/// </summary>
	private void ResetGame()
	{
		foreach(GameObject ply in AllPlayers)
		{
			// Reset Game
			if(ply.GetComponent<PlayerInfo>().Position != StaticData.FieldPosition.GOALKEEPER)
			{
				ply.GetComponent<StateMachine>().ChangeState(ply.GetComponent<StateMachine>().States["ResetGame"]);
			}
		}
		Ball.transform.position = new Vector3(0, 5f, 0);
		Ball.GetComponent<Rigidbody>().isKinematic = true;
	}
}
