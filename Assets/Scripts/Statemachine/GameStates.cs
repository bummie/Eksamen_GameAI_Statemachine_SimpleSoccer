using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

	public Dictionary<string, IState> States { get; set; }

	void Awake()
	{
		States = new Dictionary<string, IState>();
		
	#region States

		States.Add("Idle", new Idle());
		States.Add("ChaseBall", new ChaseBall());
		States.Add("PassBall", new PassBall());
		States.Add("MoveGoodSpot", new MoveGoodSpot());
		States.Add("Dribble", new Dribble());
		States.Add("ShootGoal", new ShootGoal());
		
	#endregion
	}
}
