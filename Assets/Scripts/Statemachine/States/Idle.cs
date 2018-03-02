using System.Collections;
using UnityEngine;
public class Idle : IState
{
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Idle");
    }
    public void UpdateState(GameObject player)
    {

        // Transition into wanderingmode
        if(GameObject.ReferenceEquals(player.GetComponent<BallController>().Ball.GetComponent<Ball>().CurrentHolder, player))
        {
            StateMachine stateMachine = player.GetComponent<StateMachine>();

            stateMachine.ChangeState(stateMachine.States["Wander"]);
        }else
        {
            player.GetComponent<PlayerMove>().TargetPosition = player.GetComponent<BallController>().Ball.transform.position;
            player.GetComponent<PlayerMove>().ShouldMove = true;
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
