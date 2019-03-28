using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
	private Abilities abilities;

	private void Start()
	{
		abilities = GetComponent<Abilities>();
	}

	private void FixedUpdate()
	{
			if (Input.GetKey(KeyCode.A))
			{
				abilities.Fire();
			}

			if (Input.GetKey(KeyCode.S))
			{
				abilities.Bomb();
			}
	}
}
