using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
     [SerializeField] public float speed = 1;
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
		rb.AddForce(WindConstant);
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


}
