using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
{
	[SerializeField] float fireSpeed = 10.0f;
	[SerializeField] float bombSpeed = 0.5f;
	[SerializeField] private float cooldown = 3f;

	private Transform playerTransform;
	[SerializeField] private float cooldownTimer;

	private void Awake()
	{
		// Debug Code
		playerTransform = transform;

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

		fireObject.transform.position = playerTransform.position;
		//fireObject.transform.rotation = playerTransform.rotation;

		fireObject.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed, 0f);
	}

	public void Bomb()
	{
		if (cooldownTimer > 0f)
        {
        	return;
        }

        GameObject bombPrefab = Resources.Load<GameObject>("Bomb");
        GameObject bombObject = Instantiate(bombPrefab);

        bombObject.transform.position = playerTransform.position;
        //bombObject.transform.rotation = playerTransform.rotation;

        bombObject.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed, 0f);
	}
}
