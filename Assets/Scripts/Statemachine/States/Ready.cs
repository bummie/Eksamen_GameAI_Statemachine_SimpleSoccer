using System.Collections;
using UnityEngine;
public class Ready : IState
{
    public string GetStateName()
    {
        return "Ready";
    }

    public void EnterState(GameObject player)
    {
    }
    public void UpdateState(GameObject player)
    {   
    }
    public void ExitState(GameObject player)
    {
    }
}
