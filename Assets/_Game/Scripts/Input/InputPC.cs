
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : MonoBehaviour
{
	[SerializeField] public GroupController groupController;
	private Rigidbody2D rb;
	private bool nonZeroHorizontal;

	void Awake()
	{

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

		bool groupAction = Input.GetButton("Group Action");
		if (groupAction)
		{
			groupController.ActivateGroup();
		}

		if (horizontal != 0)
		{
			nonZeroHorizontal = true;
			groupController.MoveHorizontal(horizontal);
		}
		else if (nonZeroHorizontal)
		{
			groupController.MoveHorizontal(horizontal);
			nonZeroHorizontal = false;
		}

		float vertical = Input.GetAxis("Vertical");
		if(vertical > 0)
		{
			groupController.Jump();
		}

    }
}
