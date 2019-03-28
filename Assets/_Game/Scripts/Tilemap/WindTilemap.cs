using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WindTilemap : MonoBehaviour, ITrigger
{
	[SerializeField] private Vector2 windDirection;
	private GroupController groupController;

	private void Start()
	{
		groupController = GameObject.FindGameObjectWithTag("Player").GetComponent<GroupController>();
	}

	public void OnLemmingEnter()
	{
		LemmingMovement lemming = groupController.ActiveLemming;
		lemming.WindConstant = windDirection;
	}

	public void OnLemmingExit()
	{
		LemmingMovement lemming = groupController.ActiveLemming;
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
