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
        Debug.Log("Updating Idle");
        if(player.transform.position.y > 2f)
        {
            StateMachine stateMachine = player.GetComponent<StateMachine>();
            
            stateMachine.ChangeState(stateMachine.States["Wander"]);
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
