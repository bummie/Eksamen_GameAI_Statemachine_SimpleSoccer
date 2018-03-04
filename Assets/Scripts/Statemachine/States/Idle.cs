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
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
       
        // If the oposite team has the ball or the ball is free
        if(currentHolder == null || currentHolder.GetComponent<PlayerInfo>().Team != player.GetComponent<PlayerInfo>().Team)
        {
            StateMachine stateMachine = player.GetComponent<StateMachine>();

            if(player.GetComponent<PlayerInfo>().TeamInfo.IsPlayerClosestToBall(player))
            {
                stateMachine.ChangeState(stateMachine.States["ChaseBall"]);
                return;
            }
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
