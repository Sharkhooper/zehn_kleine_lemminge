using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : MonoBehaviour
{
	private BoxCollider2D topCollider;

	private void Start()
	{
		topCollider = GetComponent<BoxCollider2D>();
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		topCollider.isTrigger = true;
	}
}
