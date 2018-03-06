using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	#region Player Parameters

	public float MoveForce { get; private set; }
	public float MaxVelocity {get; private set; }
	public float RotationForce { get; private set; }
	public float Stamina { get; private set; }
	public float MaxStamina { get; private set; }
	
	private bool _shouldMove = false;
	public bool ShouldMove 
	{ 
		get{ return _shouldMove; } 
		set
		{ 
			_shouldMove = value; 
			if(_body != null && !_shouldMove)
			{
				_body.velocity = Vector3.zero;
				_body.angularVelocity = Vector3.zero;
			}
		 }
	}

	#endregion

	// Field limit for good positionmovement
	public float TopLimit { get; set; }
	public float BottomLimit { get; set; }
	public float SideLimit { get; set; }

	public Vector3 TargetPosition {	get; set;}
	private GameObject _ball;
	private Rigidbody _body;

	void Start ()
	{
		MoveForce = 5f;
		RotationForce = 10f;
		MaxVelocity = 3f;
		MaxStamina = 100f;
		Stamina = MaxStamina;

		TargetPosition = transform.position;

		TopLimit = 3.7f;
        BottomLimit = 12.5f;
        SideLimit = 6f;

		_body = GetComponent<Rigidbody>();
		_ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () 
	{
		if(ShouldMove){ MoveToPosition(); }
		else
		{
			// Keep an eye at the ball
			RotateTowardsDirection(_ball.transform.position - transform.position);
		}
	}
	
	/// <summary>
	/// Moves the player towards a given position
	/// </summary>
	private void MoveToPosition()
	{
		if(Vector3.Distance(transform.position, TargetPosition) < 1f)
		{
			_body.velocity = Vector3.zero;
			return;
		}
		
		Vector3 direction = (TargetPosition - transform.position).normalized;
		_body.velocity = direction * MoveForce; //_body.AddForce(direction * MoveForce, ForceMode.Impulse);

		RotateTowardsDirection(direction);
	}

	/// <summary>
	/// Rotates towards given direction
	/// </summary>
	/// <param name="direction"></param>
	private void RotateTowardsDirection(Vector3 direction)
	{
		direction.y = 0f; // So we're only rotating around the y axis
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, RotationForce * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
	}

	/// <summary>
	/// Snaps towards given direction
	/// </summary>
	/// <param name="direction"></param>
	public void SnapToDirection(Vector3 direction)
	{
		direction.y = 0f; // So we're only rotating around the y axis
        transform.rotation = Quaternion.LookRotation(direction);
	}

	/// <summary>
	/// Returns true if the gven player has a good position with respect to the players role.
	/// </summary>
	/// <returns></returns>
	public bool HasGoodPosition()
	{
		// Player is to far out on the side
		if(transform.position.z > SideLimit || transform.position.z < -SideLimit ) 
		{
			return false;
		}

		PlayerInfo plyInfo = GetComponent<PlayerInfo>();
		//TODO: Write this part in a clever way
		if(plyInfo.Team == StaticData.SoccerTeam.BLUE)
		{
			if(plyInfo.Position == StaticData.FieldPosition.DEFENSE)
			{
				if(transform.position.x > -BottomLimit && transform.position.x < -TopLimit )
				{
					return true;
				}else
				{
					return false;
				}
			}else // Offense
			{
				if(transform.position.x < BottomLimit && transform.position.x > TopLimit )
				{
					return true;
				}else
				{
					return false;
				}
			}
		}else // Red team
		{
			if(plyInfo.Position == StaticData.FieldPosition.OFFENSE)
			{
				if(transform.position.x > -BottomLimit && transform.position.x < -TopLimit )
				{
					return true;
				}else
				{
					return false;
				}
			}else // Defense
			{
				if(transform.position.x < BottomLimit && transform.position.x > TopLimit )
				{
					return true;
				}else
				{
					return false;
				}
			}
		}
	}
}
