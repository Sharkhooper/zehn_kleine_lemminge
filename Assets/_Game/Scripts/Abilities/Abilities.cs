using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
{
	[SerializeField] float fireSpeed = 10.0f;
	[SerializeField] float punchDistance = 0.5f;
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

	public void Punch()
	{
		if (cooldownTimer > 0f)
		{
			return;
		}

		bool punchLeft = (playerTransform.rotation != new Quaternion(0,0,0,0));

		RaycastHit2D hit;
		if (punchLeft)
		{
			hit = Physics2D.Raycast(playerTransform.position, Vector2.left, punchDistance);
		}
		else
		{
			hit = Physics2D.Raycast(playerTransform.position, Vector2.right, punchDistance);
		}

		if (hit.collider != null)
		{
			GameObject other = hit.collider.gameObject;

			/*
			if (other.CompareTag("Punchable"))
			{
				ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die());
			}
			*/
		}
	}
}
