using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
     [SerializeField] public float speed;
    private Rigidbody rb;
	public bool isCrouching
	{ get { return isCrouching; } set { isCrouching = value; } }

	public bool inGroupe { set { inGroupe = value; } }

	private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	}

	public void MoveVertical(float vertical)
	{
		Vector3 movement = new Vector3(0.0f, 0.0f, vertical);
		rb.AddForce(movement * speed * Time.deltaTime);
	}


}
