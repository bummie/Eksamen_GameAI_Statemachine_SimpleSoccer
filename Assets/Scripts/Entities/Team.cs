using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {

	public GameObject[] TeamPlayers;
	public GameObject Ball;
	void Start () 
	{
		
	}
	
	void Update ()
	{
		
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
				closestDistance = Vector3.Distance(ply.transform.position, Ball.transform.position);
				closestPlayer = ply;
				continue;
			}

			float distance = Vector3.Distance(ply.transform.position, Ball.transform.position);
			if(distance < closestDistance)
			{
				closestDistance = distance;
				closestPlayer = ply;
			}
		}

		return GameObject.ReferenceEquals(player, closestPlayer) ? true : false;
	}
}
