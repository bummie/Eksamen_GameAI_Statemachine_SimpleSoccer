using System.Collections;
using UnityEngine;
public class ResetGame : IState
{
    public string GetStateName()
    {
        return "ResetGame";
    }

    public void EnterState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();

		plyMove.ShouldMove = true;
        plyMove.MoveToOwnSide();
    }
    public void UpdateState(GameObject player)
    {   
        //TODO: If player is done movin, go into ReadyState
        PlayerMove plyMove = player.GetComponent<PlayerMove>();
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        
        if(plyMove.HasReachedDestination())
        {
            stateMachine.ChangeState(stateMachine.States["Ready"]);
        }
    }
    public void ExitState(GameObject player)
    {
    }
}
