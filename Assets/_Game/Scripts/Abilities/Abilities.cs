using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
{
	[SerializeField] float fireSpeed = 1.5f;
	[SerializeField] float bombSpeed = 1.5f;
	[SerializeField] private float cooldown = 1f;

	private SpriteRenderer playerSpriteRenderer;
	private float cooldownTimer;

	private void Awake()
	{
		// Debug Code
		playerSpriteRenderer = GetComponent<SpriteRenderer>();

		cooldownTimer = cooldown;
	}

	private void FixedUpdate()
	{
		if (cooldownTimer > 0f)
		{
			cooldownTimer -= Time.deltaTime;
		}
	}

	public void Fire()
	{
		if (cooldownTimer > 0f)
		{
			return;
		}

		GameObject firePrefab = Resources.Load<GameObject>("Fireball");
		GameObject fireObject = Instantiate(firePrefab);
		SpriteRenderer fireRenderer = fireObject.GetComponent<SpriteRenderer>();

		fireObject.transform.position = transform.position;
		fireRenderer.flipX = playerSpriteRenderer.flipX;

		int flipDir = 1;
		if (!fireRenderer.flipX)
		{
			flipDir = -1;
		}

		fireObject.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed * flipDir, 0f);

		cooldownTimer = cooldown;
	}

	public void Bomb()
	{
		if (cooldownTimer > 0f)
        {
        	return;
        }

        GameObject bombPrefab = Resources.Load<GameObject>("Bomb");
        GameObject bombObject = Instantiate(bombPrefab);

        SpriteRenderer bombRenderer = bombObject.GetComponent<SpriteRenderer>();

        bombObject.transform.position = transform.position;
        bombRenderer.flipX = playerSpriteRenderer.flipX;

        int flipDir = 1;
        if (!bombRenderer.flipX)
        {
	        flipDir = -1;
        }

        bombObject.GetComponent<Rigidbody2D>().velocity = new Vector2(bombSpeed * flipDir, bombSpeed * 2f);

        cooldownTimer = cooldown;
	}
}
