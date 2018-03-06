using System.Collections;
using UnityEngine;
public class Idle : IState
{

   public string GetStateName()
    {
        return "Idle";
    }

    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Idle");
        player.GetComponent<PlayerMove>().ShouldMove = false;
    }
    public void UpdateState(GameObject player)
    {
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        PlayerInfo plyInfo = player.GetComponent<PlayerInfo>();
        PlayerMove plyMove = player.GetComponent<PlayerMove>();

        // If the oposite team has the ball or the ball is free
        if(currentHolder == null || currentHolder.GetComponent<PlayerInfo>().Team != plyInfo.Team)
        {
            if(plyInfo.TeamInfo.IsPlayerClosestToBall(player))
            {
                stateMachine.ChangeState(stateMachine.States["ChaseBall"]);
                return;
            }
        } // Player has received the ball
        else if(GameObject.ReferenceEquals(currentHolder, player))
        {
            stateMachine.ChangeState(stateMachine.States["PassBall"]);
            return;
        }

        // If the player has a bad position, move to "better" spot
        if(!plyMove.HasGoodPosition())
        {
            stateMachine.ChangeState(stateMachine.States["MoveGoodSpot"]);
        }   
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
