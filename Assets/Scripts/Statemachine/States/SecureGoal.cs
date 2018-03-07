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
      
        // If the oposite team has the ball or the ball is free
        if(currentHolder != null)
        {
            if(GameObject.ReferenceEquals(currentHolder, player))
            {
                stateMachine.RandomState();
                return;
            }
        } // Player has received the ball
       

        Vector3 goalCenter = ballController.OwnGoal.GetComponent<Goal>().GoalCenter;
        Vector3 direction = (ballController.Ball.transform.position - goalCenter).normalized;

        // Chase ball if close
        if(Vector3.Distance(ballController.Ball.transform.position, player.transform.position) < 4f)
        {
            plyMove.TargetPosition = ballController.Ball.transform.position;
        }
        else
        {
            //TODO: If receive ball, kick far out, or pass to teammate
            plyMove.TargetPosition = goalCenter + direction * 1.2f;
        }
    }
    public void ExitState(GameObject player)
    {
    }
}
