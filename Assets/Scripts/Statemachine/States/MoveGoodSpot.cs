using System.Collections;
using UnityEngine;
public class MoveGoodSpot : IState
{
    public string GetStateName()
    {
        return "MoveGoodSpot";
    }

    public void EnterState(GameObject player)
    {
        PlayerInfo plyInfo = player.GetComponent<PlayerInfo>();
        PlayerMove plyMove = player.GetComponent<PlayerMove>();       
        
        Vector3 newPosition = new Vector3(Random.Range(plyMove.TopLimit, plyMove.BottomLimit), 0, Random.Range(-plyMove.SideLimit, plyMove.SideLimit) );

        if(plyInfo.Team == StaticData.SoccerTeam.BLUE && plyInfo.Position == StaticData.FieldPosition.DEFENSE )
        {
            newPosition.x *= -1;
        }
        else if(plyInfo.Team == StaticData.SoccerTeam.RED && plyInfo.Position == StaticData.FieldPosition.OFFENSE )
        {
            newPosition.x *= -1;
        }

        plyMove.TargetPosition = newPosition;
        plyMove.ShouldMove = true;
    }
    public void UpdateState(GameObject player)
    {
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
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
            stateMachine.RandomState();
            return;
        }

        // We have found our position!
        if(plyMove.HasReachedDestination())
        {
            stateMachine.ChangeState(stateMachine.States["Idle"]);
            return;
        }
        
    }
    public void ExitState(GameObject player)
    {

    }
}
