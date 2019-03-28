using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTilemap : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			LemmingMovement move = other.gameObject.GetComponent<LemmingMovement>();
			// TODO: Change player physics here
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			LemmingMovement move = other.gameObject.GetComponent<LemmingMovement>();
			// TODO: Reset player physics here
		}
	}
}
