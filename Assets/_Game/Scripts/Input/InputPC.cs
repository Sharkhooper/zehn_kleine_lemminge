
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : MonoBehaviour
{
	private LemmingMovement lemmingMovement;
	private Rigidbody2D rb;

	void Awake()
	{
		lemmingMovement = GetComponent<LemmingMovement>();
	}

    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		float horizontal = Input.GetAxis("Horizontal");
		lemmingMovement.MoveHorizontal(horizontal);

		float vertical = Input.GetAxis("Vertical");
		if(vertical != 0)
		{
			lemmingMovement.Jump();
		}

    }
}
