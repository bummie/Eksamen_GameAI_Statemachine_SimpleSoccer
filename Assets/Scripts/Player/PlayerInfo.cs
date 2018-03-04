using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInfo : MonoBehaviour 
{
	public StaticData.SoccerTeam Team = StaticData.SoccerTeam.BLUE;
	public StaticData.FieldPosition Position = StaticData.FieldPosition.DEFENSE;

	public GameObject TeamObject;

	public Team TeamInfo { get; set; }

	void Start()
	{
		gameObject.GetComponent<Cosmetics>().ChangeShirtColor((int)Team);	
		TeamInfo = TeamObject.GetComponent<Team>();
	}

	//TODO: Get array of closest players except oneself

}
