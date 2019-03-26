using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public float jumpForce = 1;
	[SerializeField] public float speed = 1;
	[SerializeField] public float minSpeed = 1;
	[SerializeField] public float maxSpeed = 1;
	[SerializeField] public float breakingForceMulitplier = 10;

	public bool IsGrounded { get; set; }
	private Rigidbody2D rb;
	public bool IsCrouching { get; set; }
	public Vector2 WindConstant { get; set; }


	public bool InGroupe { set; get; }

	private GameManager gameManager;


	// Start is called before the first frame update
	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		InGroupe = false;
	}



	void FixedUpdate()
	{
		rb.AddForce(WindConstant);
	}

	public void Jump()
	{
		if (!InGroupe && IsGrounded)
		{
			Vector2 movement = new Vector2(0.0f, jumpForce * 100);
			rb.AddForce(movement * Time.deltaTime, ForceMode2D.Impulse);
		}

	}

	private void BrakeMovement()
	{
		if ((rb.velocity.x > 0.3f)||(rb.velocity.x < 0.3f))
		{
			rb.AddForce(new Vector2(-rb.velocity.x * breakingForceMulitplier, 0.0f));
		}
		else
		{
			rb.velocity = new Vector2(0, 0);
		}
	}


	public void MoveHorizontal(Vector2 direction)
	{
		Vector2 movement = new Vector2(direction.x, 0.0f);

		if (direction.x < 0)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
		}
		else
		{
			transform.rotation = new Quaternion(0, 180, 0, 0);
		}

		//if ((rb.velocity.x < 0) && (rb.velocity.x > -1))
		//{
		//	rb.velocity = new Vector2(-minSpeed, rb.velocity.y);
		//}
		//else if ((rb.velocity.x > 0) && (rb.velocity.x < 1))
		//{
		//	rb.velocity = new Vector2(minSpeed, rb.velocity.y);
		//}

		if (((rb.velocity.x > 0) && (movement.x < 0)) || ((rb.velocity.x < 0) && (movement.x > 0)))
		{
			Debug.Log("Brake :" + ((rb.velocity.x > 0) && (movement.x < 0)));
			BrakeMovement();
		}
		else
		{
			rb.AddForce(movement * speed * 100 * Time.deltaTime);
		}


		if ((rb.velocity.x > maxSpeed) || (rb.velocity.x < -maxSpeed))
		{
			Vector2 movement2 = new Vector2(maxSpeed, rb.velocity.y);

			rb.velocity = movement2;
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
}
