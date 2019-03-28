using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
	[SerializeField] private float cooldown = 2f;

	private Abilities abilities;
	private float cooldownTimer;

	private void Start()
	{
		abilities = GetComponent<Abilities>();
	}

	private void FixedUpdate()
	{
		if (cooldownTimer <= 0)
		{
			if (Input.GetKey(KeyCode.A))
			{
				abilities.Fire();
			}

			if (Input.GetKey(KeyCode.S))
			{
				abilities.Punch();
			}

			cooldownTimer = cooldown;
		}
		else
		{
			cooldownTimer -= Time.deltaTime;
		}
	}
}
