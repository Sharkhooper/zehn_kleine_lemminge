using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using _Game.Scripts.GameObjects;

public class InteractibleLadder : MonoBehaviour, IInteractible
{
	[System.Serializable]
	private class BoolEvent : UnityEvent<bool>
	{
	}

	[SerializeField] private BoolEvent onStateChange;
	[SerializeField] private float climbingSpeed = 2.0f;
	private Transform top;
	private BoxCollider2D topCollider;
	private bool switchActiv = true;
	private bool groupIn = false;
	private bool playerIn = false;
	private GameManager gameManagerScript;
	private Rigidbody2D lemmingRb;
	private GroupController groupController;
	private Vector3 upperEnd;

	private void Start()
	{
		gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

		groupController = GameObject.FindWithTag("Group").GetComponent<GroupController>();

		Vector3 pos = transform.position;
		SpriteRenderer render = GetComponent<SpriteRenderer>();
		float sizeY = render.size.y / 2;
		upperEnd = new Vector3(pos.x, pos.y + sizeY + 0.5f, pos.z);

		top = transform.GetChild(0);
		topCollider = top.GetComponent<BoxCollider2D>();
		top.position = new Vector3(upperEnd.x, upperEnd.y - 0.5f, upperEnd.z);
	}

	private void FixedUpdate()
	{
		if (lemmingRb != null)
		{
			Vector3 playerPos = lemmingRb.transform.position;
			lemmingRb.velocity = new Vector2(lemmingRb.velocity.x / 5f, climbingSpeed);
			lemmingRb.transform.position = new Vector2(transform.position.x, lemmingRb.transform.position.y);

			if (upperEnd.y - playerPos.y < 0.1f)
			{
				lemmingRb.velocity = new Vector2(lemmingRb.velocity.x, 0f);
				lemmingRb = null;
				topCollider.isTrigger = false;
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (groupIn)
		{
			return;
		}

		if (other.gameObject.CompareTag("Group"))
		{
			groupIn = true;
			Debug.Log("Group Enter Trigger");
			playerIn = true;
			gameManagerScript.ActionButtonEnable(playerIn, this);
		}
		else
		{
			if (!other.gameObject.CompareTag(("Player"))) return;

			Debug.Log("Lemming Enter Trigger");
			playerIn = true;
			gameManagerScript.ActionButtonEnable(playerIn, this);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Group"))
		{
			groupIn = false;
			playerIn = false;
			gameManagerScript.ActionButtonEnable(playerIn, this);
		}
		else
		{
			if (!other.gameObject.CompareTag(("Player"))) return;

			if (groupIn) return;
			playerIn = false;
			gameManagerScript.ActionButtonEnable(playerIn, this);
		}
	}

	public void ActionButtonPressed()
	{
		Debug.Log("Button pressed");

		if (playerIn)
		{
			lemmingRb = groupController.ActiveLemming.GetComponent<Rigidbody2D>();
			Vector3 playerPos = lemmingRb.transform.position;
		}
	}
}
