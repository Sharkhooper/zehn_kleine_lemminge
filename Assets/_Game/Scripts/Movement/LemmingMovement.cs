using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public Rigidbody2D rb;
	[SerializeField] public float jumpForce = 1.1f;
	[SerializeField] public float speed = 2;
	[SerializeField] public float maxSpeed = 2;
	[SerializeField] public float landingDelay = 50;
	[SerializeField] public float superJumpForce = 2;

	[SerializeField] public bool IsGrounded;
	public bool IsCrouching { get; set; }
	public Vector2 WindConstant { get; set; }
	public Vector2 AdditionalVelocity { get; set; }

	[SerializeField] private float landingTimer;

	// Start is called before the first frame update
	void Start()
	{
		rb.freezeRotation = true;
	}

	void FixedUpdate()
	{
		rb.AddForce(WindConstant + AdditionalVelocity);

		if (IsGrounded && landingTimer >= 0f)
		{
			landingTimer -= Time.deltaTime;
		}
	}

	public void Jump(bool superJumpActivated)
	{
		Vector2 jump;
		if (superJumpActivated)
		{
			 jump = new Vector2(0, 1 * jumpForce * superJumpForce * 5f);
		}
		else
		{
			 jump = new Vector2(0, 1 * jumpForce * 5f);
		}

		if (landingTimer <= 0f && IsGrounded)
		{
			//rb.AddForce(jump, ForceMode2D.Impulse);
			rb.velocity = jump;
			IsGrounded = false;
			landingTimer = landingDelay / 1000;
		}
	}


	// Intensity on a scale from 1 to 10
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
		if (direction > 0) direction = 1;
		else if (direction < 0) direction = -1;

		// If no movement input exists, auto brake
		if (direction < 0.1f && direction > -0.1f)
		{
			return;
		}

		Vector2 velocity = rb.velocity;

		// Brake movement when changing direction
		if (velocity.x > 0 && direction < 0 || velocity.x < 0 && direction > 0)
		{
			BrakeMovement();
		}
		// Accelerate in movement direction
		else if (rb.velocity.x < maxSpeed && rb.velocity.x > -maxSpeed)
		{
			// Acceleration on ground is faster than in air
			if (IsGrounded)
			{
				rb.velocity = new Vector2(direction, 0f) * speed * 100 * Time.deltaTime;
			}
			else
			{
				rb.velocity = new Vector2(direction * speed * 40 * Time.deltaTime, rb.velocity.y);
			}
		}

		// Reduce velocity to maxSpeed if too fast
		if (velocity.x > maxSpeed || velocity.x < -maxSpeed)
		{
			rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
		}
	}
}
