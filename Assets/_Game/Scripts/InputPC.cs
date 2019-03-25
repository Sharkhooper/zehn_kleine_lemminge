
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : MonoBehaviour
{
	private LemmingMovement lemmingMovement;

	void Awake()
	{
		lemmingMovement = GetComponent<LemmingMovement>();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float horizontal = Input.GetAxis("Horizontal");
		if(horizontal != 0)
		{
			lemmingMovement.MoveHorizontal(horizontal);
		}

		float vertical = Input.GetAxis("Vertical");
		if(vertical != 0)
		{
			lemmingMovement.Jump(vertical);
		}
        
    }
}
