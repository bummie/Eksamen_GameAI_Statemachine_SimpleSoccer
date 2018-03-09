using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MatchHandler : MonoBehaviour 
{
	public GameObject Goal_BLUE, Goal_RED;
	public Text ScoreText;
	public int PointsBlue{get; private set;}
	public int PointsRed{get; private set;}
	public GameObject[] AllPlayers{get; private set;}
	private GameObject[] _allAudience;
	public GameObject Ball{get; private set;}
	void Start () 
	{
		AllPlayers = GameObject.FindGameObjectsWithTag("Player");
		_allAudience = GameObject.FindGameObjectsWithTag("Audience");
		Ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () 
	{
		if(AllPlayersReady())
		{
			StartGame();
		}		
	}

	private bool AllPlayersReady()
	{
		foreach(GameObject ply in AllPlayers)
		{
			if(ply.GetComponent<StateMachine>().CurrentState.GetStateName() != "Ready")
			{
				return false;
			}
		}
		return true;
	}

	public void GoalScored(StaticData.SoccerTeam team)
	{
		if(team == StaticData.SoccerTeam.BLUE)
		{
			PointsRed++;
			MakeAudienceCheer(true, StaticData.SoccerTeam.RED);
		}else
		{
			PointsBlue++;
			MakeAudienceCheer(true, StaticData.SoccerTeam.BLUE);
		}

		ScoreText.text = "BLUE " + PointsBlue + " - " + PointsRed + " RED";

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
			}else
			{
				ply.GetComponent<StateMachine>().ChangeState(ply.GetComponent<StateMachine>().States["Ready"]);
			}
		}
		Ball.GetComponent<Rigidbody>().isKinematic = true;
	}

	/// <summary>
	/// Start the game
	/// </summary>
	private void StartGame()
	{
		Ball.transform.position = new Vector3(0, 5f, 0);
		Ball.GetComponent<Rigidbody>().isKinematic = false;
		
		foreach(GameObject ply in AllPlayers)
		{
			ply.GetComponent<StateMachine>().ChangeState(ply.GetComponent<StateMachine>().States["Idle"]);
		}

		MakeAudienceCheer(false, StaticData.SoccerTeam.BOTH);
	}

	/// <summary>
	/// Makes the crowd go wild
	/// </summary>
	/// <param name="shouldCheer"></param>
	private void MakeAudienceCheer(bool shouldCheer, StaticData.SoccerTeam team)
	{
		foreach(GameObject audience in _allAudience)
		{	
			if(audience.GetComponent<Cosmetics>().AudienceTeam == team || team == StaticData.SoccerTeam.BOTH)
			{
				audience.GetComponent<AudienceWave>().AudienceCheer = shouldCheer;
			}
		}
	}
}