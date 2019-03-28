using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DeadlyTilemap : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die());
			// TODO: Exchange tile here
		}
	}
}
