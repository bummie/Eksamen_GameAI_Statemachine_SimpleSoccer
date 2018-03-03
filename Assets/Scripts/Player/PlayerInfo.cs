using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour 
{
	public enum SoccerTeam {BLUE, RED};
	public enum FieldPosition {GOALKEEPER, DEFENSE, OFFENSE }
	public SoccerTeam Team = SoccerTeam.BLUE;
	public FieldPosition Position = FieldPosition.DEFENSE;

	public GameObject TeamObject;

	void Start()
	{
		gameObject.GetComponent<Cosmetics>().ChangeShirtColor((int)Team);	
	}

	

}
