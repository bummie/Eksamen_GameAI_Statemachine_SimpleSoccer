using System.Collections;
using UnityEngine;
public class ShootGoal : IState
{

    public string GetStateName()
    {
        return "ShootGoal";
    }

    public void EnterState(GameObject player)
    {
        BallController ballController = player.GetComponent<BallController>();
        PlayerMove plyMove = player.GetComponent<PlayerMove>();
        StateMachine stateMachine = player.GetComponent<StateMachine>();

        Vector3 shootDirection = ballController.OpponentsGoal.GetComponent<Goal>().RandomPointBetweenPosts() - player.transform.position;
        plyMove.SnapToDirection(shootDirection);
        ballController.KickBall(12f, Random.Range(0f, 15f));

        stateMachine.ChangeState(stateMachine.States["Idle"]);
        return;
    }
    public void UpdateState(GameObject player)
    {
        //TODO: Check if we got a clearance so we can shoot then ball towards the goal
    }
    public void ExitState(GameObject player)
    {
    }
}
