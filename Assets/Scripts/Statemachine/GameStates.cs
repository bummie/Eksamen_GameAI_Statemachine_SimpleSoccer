using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

	public Dictionary<string, IState> States { get; set; }

	void Awake()
	{
		States = new Dictionary<string, IState>();
		
		States.Add("Idle", new Idle());
		States.Add("Wander", new Wander());
	}
}
