using System.Collections;
using UnityEngine;
public class Idle : IState
{
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Idle");
        player.GetComponent<PlayerMove>().ShouldMove = false;
    }
    public void UpdateState(GameObject player)
    {
        // Check if current holder of ball is on same team 
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();

        if(currentHolder == null || currentHolder.GetComponent<PlayerInfo>().Team != player.GetComponent<PlayerInfo>().Team)
        {
            StateMachine stateMachine = player.GetComponent<StateMachine>();

            stateMachine.ChangeState(stateMachine.States["ChaseBall"]);
            return;
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
