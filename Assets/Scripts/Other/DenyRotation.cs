using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenyRotation : MonoBehaviour 
{
	/// <summary>
	/// Denies rotation of the StateText above the players
	/// </summary>
	void Update ()
	{
		if(transform.rotation.eulerAngles != Vector3.zero)
		{
			transform.rotation = Quaternion.Euler(Vector3.zero);
		}
	}
}
