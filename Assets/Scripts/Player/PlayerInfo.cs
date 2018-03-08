using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInfo : MonoBehaviour 
{
	public StaticData.SoccerTeam Team = StaticData.SoccerTeam.BLUE;
	public StaticData.FieldPosition Position = StaticData.FieldPosition.DEFENSE;

	private GameObject _teamObject;

	public Team TeamInfo { get; set; }

	void Start()
	{
		_teamObject = transform.parent.gameObject;
		TeamInfo = _teamObject.GetComponent<Team>();

		// Change to correct shirtcolor
		int shirtIndex = Position == StaticData.FieldPosition.GOALKEEPER ? ((int)Team)+2 : (int)Team; 
		gameObject.GetComponent<Cosmetics>().ChangeShirtColor(shirtIndex);	
	}
}
