using System.Collections;
using UnityEngine;
public class Wander : IState
{
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Wander");
        player.GetComponent<PlayerMove>().TargetPosition = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 5));
        player.GetComponent<PlayerMove>().ShouldMove = true;

    }
    public void UpdateState(GameObject player)
    {
        if(!GameObject.ReferenceEquals(player.GetComponent<BallController>().Ball.GetComponent<Ball>().CurrentHolder, player))
        {
            StateMachine stateMachine = player.GetComponent<StateMachine>();

            stateMachine.ChangeState(stateMachine.States["Idle"]);
            return;
        }

        if(Vector3.Distance(player.transform.position, player.GetComponent<PlayerMove>().TargetPosition) < 1f)
        {
            player.GetComponent<PlayerMove>().TargetPosition = new Vector3(Random.Range(0, 5), 0, Random.Range(0, 5));
            //player.GetComponent<BallController>().Ball.GetComponent<Ball>().DropBall();
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Wander");
    }
}
