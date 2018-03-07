using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour 
{
	public Text StateText;
	private string _startingState = "Idle";
	private IState _currentState;
	public Dictionary<string, IState> States { get; set; }
	void Start ()
	{
		States = GameObject.FindGameObjectWithTag("GameStates").GetComponent<GameStates>().States;
		ChangeState(States[_startingState]);
		UpdateStateText();
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

		UpdateStateText();
	}

	private void UpdateStateText()
	{
		StateText.text = _currentState.GetStateName();
	}

	/// <summary>
	/// Changes to a random state based on possibillity for differnt field positions
	/// </summary>
	public void RandomState()
	{	
		int possibility = Random.Range(0, 100);
			
		switch(GetComponent<PlayerInfo>().Position)
		{
			case StaticData.FieldPosition.OFFENSE:
				if(possibility > 0 && possibility < 40)
				{
					ChangeState(States["ShootGoal"]);
				}
				else if(possibility > 40 && possibility < 80)
				{
					ChangeState(States["Dribble"]);
				}else
				{
					ChangeState(States["PassBall"]);
				}
			break;

			case StaticData.FieldPosition.DEFENSE:
				if(possibility > 0 && possibility < 60)
				{
					ChangeState(States["PassBall"]);
				}
				else if(possibility > 60 && possibility < 90)
				{
					ChangeState(States["Dribble"]);
				}else
				{
					ChangeState(States["ShootGoal"]);
				}
			break;

			case StaticData.FieldPosition.GOALKEEPER:
				if(possibility > 0 && possibility < 60)
				{
					ChangeState(States["PassBall"]);
				}else
				{
					ChangeState(States["PassBall"]);
				}
			break;
		}
	}
}
