using System.Collections;
using UnityEngine;
public class Idle : IState
{
    public Idle()
    {
        
    }
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Idle");
    }
    public void UpdateState(GameObject player)
    {
        Debug.Log("Updating Idle");
        if(player.transform.position.y > 2f)
        {
            player.GetComponent<StateMachine>().ChangeState( player.GetComponent<StateMachine>().States["Wander"]);
        }
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Idle");
    }
}
