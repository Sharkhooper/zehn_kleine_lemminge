using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField] private float bombDelay = 1f;
	[SerializeField] private float explosionTime = 0.5f;

	private Transform explosion;
	private CircleCollider2D explosionCollider;
	private SpriteRenderer explosionSprite;

	private SpriteRenderer bombSprite;
	private Rigidbody2D bombRb;

	private float bombTimer;
	private float explosionTimer;

	private void Awake()
	{
		explosion = transform.GetChild(0);

		explosionCollider = explosion.GetComponent<CircleCollider2D>();
		explosionCollider.enabled = false;

		explosionSprite = explosion.GetComponent<SpriteRenderer>();
		explosionSprite.enabled = false;

		bombSprite = GetComponent<SpriteRenderer>();
		bombRb = GetComponent<Rigidbody2D>();

		bombTimer = bombDelay;
	}

	private void FixedUpdate()
	{
		if (explosionTimer > 0f)
		{
			bombRb.velocity = Vector2.zero;
			explosionTimer -= Time.deltaTime;

			if (explosionTimer <= 0f)
			{
				Destroy(gameObject);
			}
		}
		else if (bombTimer <= 0f)
		{
			explosionCollider.enabled = true;
			explosionSprite.enabled = true;

			bombSprite.enabled = false;
			explosionTimer = explosionTime;
		}
		else
		{
			bombTimer -= Time.deltaTime;
		}
	}
}
