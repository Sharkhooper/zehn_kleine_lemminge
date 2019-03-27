﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
	private LemmingMovement move;

	private void Start()
	{
		move = transform.parent.GetComponent<LemmingMovement>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		move.IsGrounded = true;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		move.IsGrounded = true;
	}
}