
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : MonoBehaviour
{
	[SerializeField] public GroupController groupController;
	private Rigidbody2D rb;
	private bool nonZeroHorizontal;
	private float ActionCoolDown { get; set; }

	void Awake()
	{

	}

    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
		ActionCoolDown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (ActionCoolDown > 0)
		{
			ActionCoolDown -= 0.1f;
		}
		float horizontal = Input.GetAxis("Horizontal");

		bool groupAction = Input.GetButton("Group Action");
		if (groupAction && ActionCoolDown <= 0)
		{
			groupController.ActivateGroup(true);
			ActionCoolDown = 1.0f;
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
