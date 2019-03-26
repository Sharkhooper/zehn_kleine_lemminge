using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeadlyTilemap : MonoBehaviour, ICollidable
{
	public void OnCollisionWithLemming()
	{
		Debug.Log("This is so great");
	}

	public void OnCollisionWithGroup()
	{
		throw new System.NotImplementedException();
	}
}
