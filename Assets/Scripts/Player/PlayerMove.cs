using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	#region Player Parameters

	public float MoveForce { get; private set; }
	public float MuscleForce { get; private set; }
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
			if(!_hasTimeOut)
			{
				_shouldMove = value; 
				if(_body != null && !_shouldMove)
				{
					_body.velocity = Vector3.zero;
					_body.angularVelocity = Vector3.zero;
				}
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

	//Timer
	private bool _hasTimeOut = false;
	private float _timeOutTime = 0.2f;
	private float _timeOutTimeLeft;

	void Start ()
	{
		MoveForce = Random.Range(4f, 7f);
		MuscleForce = Random.Range(50f, 250f);
		RotationForce = 10f;
		MaxVelocity = 3f;
		MaxStamina = 100f;
		Stamina = MaxStamina;

		_timeOutTimeLeft = _timeOutTime;

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

		if(_hasTimeOut)
		{
			_timeOutTimeLeft -= Time.deltaTime;
			if(_timeOutTimeLeft < 0)
			{
				_hasTimeOut = false;
				ShouldMove = true;
				_timeOutTimeLeft = _timeOutTime;
			}
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
	
	/// <summary>
	/// Hinders the given player from moving for a given time
	/// This because we move the players by changing their velocity
	/// rather than adding impulses. So we time them out so we can 
	/// apply nudging force to them.
	/// </summary>
	public void MovementTimeout()
	{
		_hasTimeOut = true;
		_shouldMove = false;
	}

	/// <summary>
	/// If two players from opposite teams meet, they push with their musclestrength 
	/// </summary>
	/// <param name="other"></param>
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Player")
		{
			if(other.transform.GetComponent<PlayerInfo>().Team != GetComponent<PlayerInfo>().Team)
			{
				other.transform.GetComponent<PlayerMove>().MovementTimeout();
				
				Vector3 direction = (other.transform.position - transform.position).normalized;
				other.transform.GetComponent<Rigidbody>().AddForce(direction * MuscleForce , ForceMode.Impulse);
			}
		}
	}

	/// <summary>
	/// Moves the players to their own half of the field
	/// </summary>
	public void MoveToOwnSide()
	{
		PlayerInfo plyInfo = transform.GetComponent<PlayerInfo>();

		Vector3 newPosition = new Vector3(Random.Range(TopLimit, BottomLimit), 0, Random.Range(-SideLimit, SideLimit) );

        if(plyInfo.Team == StaticData.SoccerTeam.BLUE )
        {
            newPosition.x *= -1;
        }
       
	   	TargetPosition = newPosition;
	}
}
