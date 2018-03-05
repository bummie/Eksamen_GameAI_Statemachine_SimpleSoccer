using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour 
{
	/// <summary>
	/// If the ball falls out of the field, we rescue it! 	
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Ball")
		{
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			other.transform.position = new Vector3(0f, 3f, 0f);
		}
	}

}
