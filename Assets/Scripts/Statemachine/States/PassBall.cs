using UnityEngine;

public class PassBall : IState
{
   	public void EnterState(GameObject player)
    {
		RaycastHit hit;

        if (Physics.Raycast(player.transform.position + player.transform.forward, player.transform.forward, out hit, 50.0f))
		{
			Debug.Log("hit" + hit.transform.gameObject.tag);
		}
		player.GetComponent<PlayerMove>().ShouldMove = false;
    }
    public void UpdateState(GameObject player)
    {
		Debug.DrawRay(player.transform.position + player.transform.forward, player.transform.forward * 50, Color.blue, .5f);
    }
    public void ExitState(GameObject player)
    {
    
    }

}
