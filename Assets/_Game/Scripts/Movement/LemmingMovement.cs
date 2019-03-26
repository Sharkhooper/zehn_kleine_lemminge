using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public float jumpForce = 1;
	[SerializeField] public float speed = 1;
	[SerializeField] public float maxSpeed = 5;
	[SerializeField] public float breakingForceMulitplier = 10;

	[SerializeField] public bool isGrounded;
	private Rigidbody2D rb;
	public bool IsCrouching { get; set; }
	public Vector2 WindConstant { get; set; }
	public bool InGroup { set; get; }

	private GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		InGroup = false;
	}

	void FixedUpdate()
	{
		rb.AddForce(WindConstant);
	}

	public void Jump()
	{
		if (!InGroup && isGrounded)
		{
			rb.AddForce(Vector2.up * jumpForce * 3); //, ForceMode2D.Impulse
		}

	}

	private void BrakeMovement()
	{
		if (rb.velocity.x < -0.3f && rb.velocity.x > 0.3f)
		{
			Vector2 velo = rb.velocity;
			rb.velocity = new Vector2(velo.x * 0.01f, velo.y);
		}
		else
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
	}

	public void MoveHorizontal(float direction)
	{
		if (direction < 0.1f && direction > -0.1f)
		{
			BrakeMovement();
			return;
		}

		if (direction < 0f)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
		}
		else if(direction > 0f)
		{
			transform.rotation = new Quaternion(0, 180, 0, 0);
		}

		if ((rb.velocity.x > 0 && direction < 0) ||(rb.velocity.x < 0 && direction > 0))
		{
			BrakeMovement();
		}
		else if(rb.velocity.x < maxSpeed && rb.velocity.x > -maxSpeed)
		{
			rb.AddForce(new Vector2(direction, 0f) * speed * 100 * Time.deltaTime);
		}

		/*
		if ((rb.velocity.x > maxSpeed) || (rb.velocity.x < -maxSpeed))
		{
			Vector2 movement2 = new Vector2(maxSpeed, rb.velocity.y);
			rb.velocity = movement2;
		}
		*/
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
		if (collidable != null)
		{
			collidable.OnCollisionWithLemming();
		}
		else if (other.gameObject.tag.Equals("Ground"))
		{
			isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag.Equals("Ground"))
		{
			isGrounded = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		ITrigger trigger = other.gameObject.GetComponent<ITrigger>();
		if (trigger != null)
		{
			trigger.OnLemmingEnter();
		}
		else if(other.tag.Equals("Ground"))
		{
			isGrounded = true;
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
