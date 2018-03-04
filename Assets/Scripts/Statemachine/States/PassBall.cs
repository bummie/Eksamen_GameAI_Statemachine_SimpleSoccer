using UnityEngine;

public class PassBall : IState
{
   	public void EnterState(GameObject player)
    {
      	player.GetComponent<PlayerMove>().ShouldMove = false;
    }
    public void UpdateState(GameObject player)
    {
        StateMachine stateMachine = player.GetComponent<StateMachine>();

		RaycastHit hit;

		// Shuffle the teamplayerarray
		player.GetComponent<PlayerInfo>().TeamInfo.ShuffleTeam(); 
		// Loops through the teamplayers and finds one that is not obstructed and passes the ball
		foreach(GameObject teamPlayer in player.GetComponent<PlayerInfo>().TeamInfo.TeamPlayers)
		{
			if(GameObject.ReferenceEquals(teamPlayer, player)) {continue;} // Dont include self

			Vector3 direction = (teamPlayer.transform.position - player.transform.position).normalized;
			player.GetComponent<PlayerMove>().RotateTowardsDirection(direction);
			
			//Debug.DrawRay(player.transform.position + player.transform.forward, player.transform.forward * 50, Color.blue, .5f);

			if (Physics.Raycast(player.transform.position + player.transform.forward, player.transform.forward, out hit, 50.0f))
			{
				if(hit.transform.tag.Equals(player.transform.tag))
				{
					if(hit.transform.gameObject.GetComponent<PlayerInfo>().Team == player.GetComponent<PlayerInfo>().Team)
					{
						player.GetComponent<BallController>().KickBall(10f);
            			stateMachine.ChangeState(stateMachine.States["Idle"]);
						return;
					}
				}
			}
		}

		// If we cant find anyone to pass the ball to
		stateMachine.ChangeState(stateMachine.States["Idle"]);
    }
    public void ExitState(GameObject player)
    {
    
	}
}
