using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
     [SerializeField] public float speed = 1;
	[SerializeField] public float maxSpeed = 100;
	private bool IsGrounded;
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
		InGroupe = false;
    }

	

	void FixedUpdate()
	{
		
		//IsGrounded = GetComponentInChildren<CapsuleCollider2D>().
		Debug.Log((GetComponentInChildren<CapsuleCollider2D>()));

		rb.AddForce(WindConstant);

		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	public void Jump(float vertical)
	{
		if (!InGroupe)
		{
			Vector2 movement = new Vector2( 0.0f, vertical);
			rb.AddForce(movement * Time.deltaTime, ForceMode2D.Impulse);
		}

	}

	public void MoveHorizontal(float horizontal)
	{
		Vector2 movement = new Vector2(horizontal,0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);
	}

	public void TouchVector(Vector2 movement)
	{
		Jump(movement.y);
		MoveHorizontal(movement.x);
	}
}
