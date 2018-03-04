using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {

	public GameObject[] TeamPlayers;
	private GameObject _ball;
	void Start () 
	{
		_ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	public void ShuffleTeam()
	{
		for (int i = TeamPlayers.Length-1; i > 0; i--)
			{
				// Randomize a number between 0 and i (so that the range decreases each time)
				int rnd = Random.Range(0, i);
				
				// Save the value of the current i, otherwise it'll overright when we swap the values
				GameObject temp = TeamPlayers[i];
				
				// Swap the new and old values
				TeamPlayers[i] = TeamPlayers[rnd];
				TeamPlayers[rnd] = temp;
			}
	}
	/// <summary>
	/// Takes a teamplayer and returns whether the given players is the closest one
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public bool IsPlayerClosestToBall(GameObject player)
	{
		GameObject closestPlayer = null;
		float closestDistance = -1;
		foreach(GameObject ply in TeamPlayers)
		{
			if(closestDistance == -1)
			{
				closestDistance = Vector3.Distance(ply.transform.position, _ball.transform.position);
				closestPlayer = ply;
				continue;
			}

			float distance = Vector3.Distance(ply.transform.position, _ball.transform.position);
			if(distance < closestDistance)
			{
				closestDistance = distance;
				closestPlayer = ply;
			}
		}

		return GameObject.ReferenceEquals(player, closestPlayer) ? true : false;
	}
}
