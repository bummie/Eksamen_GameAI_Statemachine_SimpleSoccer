using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	#region Player Parameters

	public float MoveForce { get; private set; }
	public float Stamina {get; private set; }

	private GameObject _ball;

	#endregion
	void Start ()
	{
		MoveForce = 15f;
		_ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	void Update () 
	{
		MoveToPosition(_ball.transform.position);
	}
	
	/// <summary>
	/// Moves the player towards a given position
	/// </summary>
	/// <param name="newPos"></param>
	public void MoveToPosition(Vector3 newPos)
	{
		Vector3 direction = (newPos - transform.position).normalized;
		GetComponent<Rigidbody>().AddForce(direction * MoveForce, ForceMode.Impulse);

		// Rotate towards position moving towards
		direction.y = 0f;
        float step = 10f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}
