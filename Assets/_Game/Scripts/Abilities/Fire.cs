using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fire : Deadly
{
	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		//Debug.Log("Collided");
		if (isActive)
		{
			ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
		}

		Destroy(gameObject);
	}
}
