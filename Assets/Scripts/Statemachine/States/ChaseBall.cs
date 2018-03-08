using System.Collections;
using UnityEngine;
public class ChaseBall : IState
{
    public string GetStateName()
    {
        return "ChaseBall";
    }

    public void EnterState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();

		plyMove.ShouldMove = true;
		plyMove.TargetPosition = player.GetComponent<BallController>().Ball.transform.position;
    }
    public void UpdateState(GameObject player)
    {   
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
        PlayerMove plyMove = player.GetComponent<PlayerMove>();

        if(currentHolder != null)
        {
            // We caught the ball!
            if(GameObject.ReferenceEquals(currentHolder, player))
            {
                stateMachine.RandomState();
                return;
            }

            // Stop chasing if a teammate has the ball
            if( currentHolder.GetComponent<PlayerInfo>().Team == player.GetComponent<PlayerInfo>().Team)
            {
                stateMachine.ChangeState(stateMachine.States["Idle"]);
                return;
            }
        }

        // Chase the ball if closest, if not move to good spot!
        if(player.GetComponent<PlayerInfo>().TeamInfo.IsPlayerClosestToBall(player))
        {
            Vector3 direction = (player.GetComponent<BallController>().Ball.transform.position - player.transform.position).normalized;
            plyMove.TargetPosition = player.GetComponent<BallController>().Ball.transform.position + direction * 1.2f;
            plyMove.ShouldMove = true;
        }else
        {
            stateMachine.ChangeState(stateMachine.States["MoveGoodSpot"]);
            return;
        }
    }
    public void ExitState(GameObject player)
    {
    }
}
