using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WindTilemap : MonoBehaviour
{
	[SerializeField] private Vector2 windDirection;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Player"))
		{
			LemmingMovement move = other.gameObject.GetComponent<LemmingMovement>();
			move.WindConstant = windDirection;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag.Equals("Player"))
		{
			LemmingMovement move = other.gameObject.GetComponent<LemmingMovement>();
			move.WindConstant = Vector2.zero;
		}
	}
}
