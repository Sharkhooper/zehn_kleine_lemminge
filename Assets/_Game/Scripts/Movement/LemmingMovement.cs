using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
     [SerializeField] public float speed;
    private Rigidbody rb;
	public bool isCrouching
	{ get; set; }

	public bool inGroupe { set; get; }

	private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
		inGroupe = false;
    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	}

	public void Jump(float vertical)
	{
		if (!inGroupe)
		{
			Vector3 movement = new Vector3( 0.0f, vertical, 0.0f);
			rb.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
		}

	}

	public void MoveHorizontal(float horizontal)
	{
		Vector3 movement = new Vector3(horizontal,0.0f, 0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);
	}


}
