using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public Rigidbody2D rb;
	[SerializeField] public float jumpForce = 1.1f;
	[SerializeField] public float speed = 5;
	[SerializeField] public float landingDelay = 50;
	[SerializeField] public float superJumpForce = 2;

	[SerializeField] public bool IsGrounded;
	public bool IsCrouching { get; set; }
	public Vector2 WindConstant { get; set; }
	private Animator anim;

	[SerializeField] private float landingTimer;

	// Start is called before the first frame update
	void Start()
	{
		rb.freezeRotation = true;
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		//rb.AddForce(WindConstant + AdditionalVelocity);

		if (IsGrounded)
		{
			if (landingTimer >= 0f)
			{
				landingTimer -= Time.deltaTime;
			}

			//if (rb.velocity < )
		}
	}

	public void Jump(bool superJumpActivated)
	{
		Vector2 jump;
		if (superJumpActivated)
		{
			jump = new Vector2(0, jumpForce * superJumpForce * 5f);
		}
		else
		{
			jump = new Vector2(0, jumpForce * 5f);
		}

		if (landingTimer <= 0f && IsGrounded)
		{
			rb.velocity = jump;
			IsGrounded = false;
			landingTimer = landingDelay / 1000;
		}
	}

	private void BrakeMovement()
	{
		if (rb.velocity.x < -0.1f && rb.velocity.x > 0.1f)
		{
			Vector2 velocity = rb.velocity;
			rb.velocity = new Vector2(velocity.x * 0.1f, velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
	}

	public void MoveHorizontal(float direction)
	{
		// Clamp input to -1 / 1
		if (direction > 0.1f)
		{
			direction = 1;
		}
		else if(direction < -0.1f)
		{
			direction = -1;
		}
		else
		{
			return;
		}

		Vector2 velocity = rb.velocity;

		// Brake movement when changing direction
		if (velocity.x > 0 && direction < 0 || velocity.x < 0 && direction > 0)
		{
			BrakeMovement();
		}

		if (IsGrounded)
		{
			rb.velocity = new Vector2(direction * speed, 0f);
		}
		else
		{
			rb.velocity = new Vector2(direction * speed * 0.7f, rb.velocity.y);
		}
	}
}
