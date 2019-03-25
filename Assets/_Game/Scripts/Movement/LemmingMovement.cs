using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
     [SerializeField] public float speed;
    private Rigidbody rb;
	public bool isCrouching
	{ get { return isCrouching; } set { isCrouching = value; } }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    void FixedUpdate()
    {
        
    }

	public void MoveHorizontal(float horizontal)
	{
		Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);
	}

	public void MoveVertical(float vertical)
	{
		Vector3 movement = new Vector3(0.0f, 0.0f, vertical);
		rb.AddForce(movement * speed * Time.deltaTime);
	}


}
