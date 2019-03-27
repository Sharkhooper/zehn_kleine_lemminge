using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public float jumpForce = 1;
	[SerializeField] public float speed = 1;
	[SerializeField] public float maxSpeed = 5;
	[SerializeField] public float landingDelay = 100;
	[SerializeField] public float superJumpForce = 2;
	[SerializeField] public bool InGroup = true;

	public bool IsGrounded { get; set; }
	public bool IsCrouching { get; set; }
	public Vector2 WindConstant { get; set; }
	public Vector2 AdditionalVelocity { get; set; }

	private GameManager manager;
	private Rigidbody2D rb;
	[SerializeField] private float landingTimer;

	[SerializeField] public Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		manager = FindObjectOfType<GameManager>();

		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		
		InGroup = false;
	}

	void FixedUpdate()
	{
		rb.AddForce(WindConstant + AdditionalVelocity);

		if (IsGrounded && landingTimer >= 0f)
		{
			landingTimer -= Time.deltaTime;
		}
	}

	public void Jump()
	{
		Vector2 jump;
		/*if (manager.SuperJumpActivated)
		{
			 jump = new Vector2(0, 1 * jumpForce * superJumpForce * 5f);
		}
		else*/
		{
			 jump = new Vector2(0, 1 * jumpForce * 5f);
		}

		if (!InGroup && landingTimer <= 0f && IsGrounded)
		{
			//rb.AddForce(jump, ForceMode2D.Impulse);
			rb.velocity = jump;
			IsGrounded = false;
			landingTimer = landingDelay / 1000;
		}
	}

	public void JumpWithHorizontal(float direction)
	{
		Vector2 jump;
		if (manager.SuperJumpActivated)
		{
			jump = new Vector2(direction, 1 * jumpForce * superJumpForce * 5f);
		}
		else
		{
			jump = new Vector2(direction, 1 * jumpForce * 5f);
		}

		if (!InGroup && landingTimer <= 0f && IsGrounded)
		{
			rb.AddForce(jump, ForceMode2D.Impulse);
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

		Debug.Log(direction);
		animator.SetFloat("Speed", Mathf.Abs(direction));

		// If no movement input exists, auto brake
		if (direction < 0.1f && direction > -0.1f)
		{
			return;
		}

		// Rotates character to face direction it's moving
		if (direction < 0f)
		{
			transform.rotation = Quaternion.identity;
		}
		else if (direction > 0f)
		{
			transform.rotation = Quaternion.Euler(0,180,0);
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
				//rb.AddForce(new Vector2(direction, 0f) * speed * 100 * Time.deltaTime);
				rb.velocity = new Vector2(direction, 0f) * speed * 100 * Time.deltaTime;
			}
			else
			{
				//rb.AddForce(new Vector2(direction, 0f) * speed * 40 * Time.deltaTime);
				rb.velocity = new Vector2(direction * speed * 40 * Time.deltaTime, rb.velocity.y);
			}
		}

		// Reduce velocity to maxSpeed if too fast
		if (velocity.x > maxSpeed || velocity.x < -maxSpeed)
		{
			rb.velocity = new Vector2(direction * maxSpeed, rb.velocity.y);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
		if (collidable != null)
		{
			collidable.OnCollisionWithLemming();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		ITrigger trigger = other.gameObject.GetComponent<ITrigger>();
		if (trigger != null)
		{
			trigger.OnLemmingEnter();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		ITrigger trigger = other.gameObject.GetComponent<ITrigger>();
		if (trigger != null)
		{
			trigger.OnLemmingExit();
		}
	}
}
