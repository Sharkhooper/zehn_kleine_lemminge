using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTrigger : MonoBehaviour
{
	private LemmingMovement lemmingMovement;
    // Start is called before the first frame update
    void Start()
    {
		lemmingMovement = GetComponentInParent<LemmingMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		lemmingMovement.IsGrounded = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		lemmingMovement.IsGrounded = false;
	}
}
