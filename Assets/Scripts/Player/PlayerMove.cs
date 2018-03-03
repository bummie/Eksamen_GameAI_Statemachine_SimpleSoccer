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

	public Vector3 TargetPosition {	get; set;}
	private GameObject _ball;
	private Rigidbody _body;

	void Start ()
	{
		MoveForce = 10f;
		RotationForce = 10f;
		MaxVelocity = 3f;
		MaxStamina = 100f;
		Stamina = MaxStamina;

		TargetPosition = transform.position;

		_body = GetComponent<Rigidbody>();
		_ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () 
	{
		if(ShouldMove){ MoveToPosition(); }
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
		_body.AddForce(direction * MoveForce, ForceMode.Impulse);

		RotateTowardsDirection(direction);
	}

	/// <summary>
	/// Rotates towards given direction
	/// </summary>
	/// <param name="direction"></param>
	private void RotateTowardsDirection(Vector3 direction)
	{
		direction.y = 0f; // So we're only rotating around the y axis
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, RotationForce * Time.deltaTime, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}
