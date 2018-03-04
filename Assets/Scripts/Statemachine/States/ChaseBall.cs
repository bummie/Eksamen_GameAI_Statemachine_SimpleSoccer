using System.Collections;
using UnityEngine;
public class ChaseBall : IState
{
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state ChaseBall");
		player.GetComponent<PlayerMove>().ShouldMove = true;
		player.GetComponent<PlayerMove>().TargetPosition = player.GetComponent<BallController>().Ball.transform.position;
    }
    public void UpdateState(GameObject player)
    {   
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
        if(currentHolder != null)
        {
            // We caught the ball!
            if(GameObject.ReferenceEquals(currentHolder, player))
            {
                StateMachine stateMachine = player.GetComponent<StateMachine>();

                stateMachine.ChangeState(stateMachine.States["Idle"]);
                return;
            }

            // Stop chasing if a teammate has the ball
            if( currentHolder.GetComponent<PlayerInfo>().Team == player.GetComponent<PlayerInfo>().Team)
            {
                StateMachine stateMachine = player.GetComponent<StateMachine>();

                stateMachine.ChangeState(stateMachine.States["Idle"]);
                return;
            }
        }

        // Chase the ball!
        player.GetComponent<PlayerMove>().TargetPosition = player.GetComponent<BallController>().Ball.transform.position;
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Wander");
    }
}
