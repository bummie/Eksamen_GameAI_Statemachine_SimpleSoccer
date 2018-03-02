using System.Collections;
using UnityEngine;
public class Wander : IState
{
    public void EnterState(GameObject player)
    {
        Debug.Log("Entering state Wander");
    }
    public void UpdateState(GameObject player)
    {
        Debug.Log("Updating Wander");
    }
    public void ExitState(GameObject player)
    {
        Debug.Log("Exiting Wander");
    }
}
