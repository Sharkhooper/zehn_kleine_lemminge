using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	private Transform explosion;
	private CircleCollider2D explosionCollider;
	private SpriteRenderer explosionSprite;

	private SpriteRenderer bombSprite;

	private void Awake()
	{
		explosion = transform.GetChild(0);

		explosionCollider = explosion.GetComponent<CircleCollider2D>();
		explosionCollider.enabled = false;

		explosionSprite = explosion.GetComponent<SpriteRenderer>();
		explosionSprite.enabled = false;

		bombSprite = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player"))
		{
			explosionCollider.enabled = true;
			explosionSprite.enabled = true;

			bombSprite.enabled = false;
		}
	}
}
