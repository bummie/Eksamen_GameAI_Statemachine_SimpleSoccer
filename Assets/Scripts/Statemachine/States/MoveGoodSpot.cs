using System.Collections;
using UnityEngine;
public class MoveGoodSpot : IState
{
    public string GetStateName()
    {
        return "MoveGoodSpot";
    }

    public void EnterState(GameObject player)
    {
        // Borders to stay within
        // TODO: Change to gameobjectpositions
        float topLimit = 3.7f;
        float bottomLimit = 12.5f;
        float sideLimit = 6f;

        Vector3 newPosition = new Vector3(Random.Range(topLimit, bottomLimit), 0, Random.Range(-sideLimit, sideLimit) );
        PlayerInfo plyInfo = player.GetComponent<PlayerInfo>();
        PlayerMove plymove = player.GetComponent<PlayerMove>();

        if(plyInfo.Team == StaticData.SoccerTeam.BLUE && plyInfo.Position == StaticData.FieldPosition.DEFENSE )
        {
            newPosition.x *= -1;
        }
        else if(plyInfo.Team == StaticData.SoccerTeam.RED && plyInfo.Position == StaticData.FieldPosition.OFFENSE )
        {
            newPosition.x *= -1;
        }

        plymove.TargetPosition = newPosition;
        plymove.ShouldMove = true;
    }
    public void UpdateState(GameObject player)
    {
        StateMachine stateMachine = player.GetComponent<StateMachine>();
        GameObject currentHolder = player.GetComponent<BallController>().CurrentBallHolder();
        if(currentHolder != null)
        {
            // We caught the ball!
            if(GameObject.ReferenceEquals(currentHolder, player))
            {

                stateMachine.ChangeState(stateMachine.States["Idle"]);
                return;
            }
        }

        // We have found our position!
        if(Vector3.Distance(player.transform.position, player.GetComponent<PlayerMove>().TargetPosition) < 3f)
        {
            stateMachine.ChangeState(stateMachine.States["Idle"]);
            return;
        }
        
    }
    public void ExitState(GameObject player)
    {

    }
}
