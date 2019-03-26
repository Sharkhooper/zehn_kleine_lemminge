using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
	[SerializeField] public float jumpForce = 1;
     [SerializeField] public float speed = 1;
	[SerializeField] public float maxSpeed = 1;
	public bool IsGrounded
	{get;set;}
    private Rigidbody2D rb;
	public bool IsCrouching
	{ get; set; }
	public Vector2 WindConstant
	{ get; set; }


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

	public void MoveHorizontal(float horizontal)
	{
		Vector2 movement = new Vector2(horizontal,0.0f);
		rb.AddForce(movement * speed*100 * Time.deltaTime);

		if ((rb.velocity.x > maxSpeed) || (rb.velocity.x < -maxSpeed))
		{
			Vector2 movement2 = new Vector2(maxSpeed, rb.velocity.y);


			rb.velocity.Set(movement2.x, rb.velocity.y);
			//Debug.Log(rb.velocity);
		}
	}

	public void TouchVector(Vector2 movement)
	{
		Jump();
		MoveHorizontal(movement.x);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
		if(collidable != null)
		{
			collidable.OnCollisionWithLemming();
		}
	}
}
