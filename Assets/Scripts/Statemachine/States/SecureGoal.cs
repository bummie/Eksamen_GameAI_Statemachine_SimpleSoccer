using System.Collections;
using UnityEngine;
public class SecureGoal : IState
{
    public string GetStateName()
    {
        return "SecureGoal";
    }

    public void EnterState(GameObject player)
    {
       PlayerMove plyMove = player.GetComponent<PlayerMove>();
       plyMove.ShouldMove = true;
    }
    public void UpdateState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        BallController ballController = player.GetComponent<BallController>();
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
      
        // If the goal keeper receives the ball
        if(currentHolder != null)
        {
            if(GameObject.ReferenceEquals(currentHolder, player))
            {
                stateMachine.RandomState();
                return;
            }
        } 

        // Chase ball if close
        if(Vector3.Distance(ballController.Ball.transform.position, player.transform.position) < 4f)
        {
            plyMove.TargetPosition = ballController.Ball.transform.position;
        }
        else
        {
            plyMove.TargetPosition = ballController.OwnGoal.GetComponent<Goal>().PointBetweenPosts(ballController.Ball.transform.position);
            plyMove.SnapToDirection(ballController.Ball.transform.position - player.transform.position);
        }
    }
    public void ExitState(GameObject player)
    {
    }
}
