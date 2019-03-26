using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.WSA.Tile;

public class WindTilemap : MonoBehaviour, ITrigger
{
	[SerializeField] private Vector2 windDirection;
	private PlayerController playerController;

	private void Start()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void OnLemmingEnter()
	{
		LemmingMovement lemming = playerController.ActiveLemming;
		lemming.WindConstant = windDirection;
	}

	public void OnLemmingExit()
	{
		LemmingMovement lemming = playerController.ActiveLemming;
		lemming.WindConstant = Vector2.zero;
	}

	public void OnGroupEnter()
	{
		throw new System.NotImplementedException();
	}

	public void OnGroupExit()
	{
		throw new System.NotImplementedException();
	}
}
