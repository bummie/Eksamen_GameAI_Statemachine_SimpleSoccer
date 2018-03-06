﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour 
{
	public GameObject GoalPostLeft, GoalPostRight;

	public Vector3 RandomPointBetweenPosts()
	{
		Vector3 leftPost = GoalPostLeft.transform.position;
		leftPost.y = 0f;
		Vector3 rightPost = GoalPostRight.transform.position;
		rightPost.y = 0f;
		
		return Vector3.Lerp(leftPost, rightPost, Random.value);
	}
}
