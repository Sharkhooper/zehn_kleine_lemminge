using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	private Transform explosion;
	private CircleCollider2D explosionCollider;
	private SpriteRenderer explosionSprite;

	private void Awake()
	{
		explosion = transform.GetChild(0);

		explosionCollider = explosion.GetComponent<CircleCollider2D>();
		explosionCollider.enabled = false;

		explosionSprite = explosion.GetComponent<SpriteRenderer>();
		explosionSprite.enabled = false;
	}

	private void FixedUpdate()
	{
		if (explosionSprite.enabled)
		{
			if (explosion.localScale.x < 2.0f)
			{
				explosion.localScale *= 1.001f;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player"))
		{
			explosionCollider.enabled = true;
			explosionSprite.enabled = true;
		}
	}
}
