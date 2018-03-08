using System.Collections;
using UnityEngine;
public class ResetGame : IState
{
    public string GetStateName()
    {
        return "ResetGame";
    }

    public void EnterState(GameObject player)
    {
        PlayerMove plyMove = player.GetComponent<PlayerMove>();

		plyMove.ShouldMove = true;
        plyMove.MoveToOwnSide();
    }
    public void UpdateState(GameObject player)
    {   
        //TODO: If player is done movin, go into ReadyState
    }
    public void ExitState(GameObject player)
    {
    }
}
