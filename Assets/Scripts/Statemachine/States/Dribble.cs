using System.Collections;
using UnityEngine;
public class Dribble : IState
{
    public string GetStateName()
    {
        return "Dribble";
    }

    public void EnterState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();
        BallController ballController = player.GetComponent<BallController>();
        plyMove.ShouldMove = true;
        plyMove.TargetPosition = ballController.OpponentsGoal.RandomPointBetweenPosts();

    }
    public void UpdateState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();
        PlayerInfo plyInfo = player.GetComponent<PlayerInfo>();
        BallController ballController = player.GetComponent<BallController>();
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
       
        // We lost the ball!
        if(currentHolder != null)
        {
            if(!GameObject.ReferenceEquals(currentHolder, player))
            {
                stateMachine.ChangeState(stateMachine.States["Idle"]);
                return;
            }
        }else
        {
            stateMachine.ChangeState(stateMachine.States["Idle"]);
            return;
        }

        // If we get close to the goal, kick that ball!
        // Offense players will move closer towards the goal than defensive players
        float distanceToGoal = plyInfo.Position == StaticData.FieldPosition.OFFENSE ? Random.Range(5f, 8f) : Random.Range(8f, 15f);
        if(Vector3.Distance(player.transform.position, plyMove.TargetPosition) < distanceToGoal)
        {
            stateMachine.ChangeState(stateMachine.States["ShootGoal"]);
            return;
        }
    }
    public void ExitState(GameObject player)
    {
        
    }
}
