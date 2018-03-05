using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour 
{
	public Text StateText;
	private IState _currentState;
	public Dictionary<string, IState> States { get; set; }
	void Start ()
	{
		States = GameObject.FindGameObjectWithTag("GameStates").GetComponent<GameStates>().States;
		ChangeState(States["Idle"]);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_currentState != null)
		{
			_currentState.UpdateState(gameObject);
		}
	}

	/// <summary>
	/// Changes the state of the statemachine
	/// </summary>
	/// <param name="newState"></param>
	public void ChangeState(IState newState)
	{
		if(_currentState == null)
		{
			_currentState = newState;
			newState.EnterState(gameObject);
			return;
		}

		_currentState.ExitState(gameObject);
		_currentState = newState;
		_currentState.EnterState(gameObject);

		StateText.text = _currentState.GetStateName();
	}
}
