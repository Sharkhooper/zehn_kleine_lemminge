using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
	[SerializeField] private float maxAngle = 45f;
	[SerializeField] private float speed = 5f;
	private Vector3 rotPoint;
	private Vector3 rotAxis;
	private bool swingsCClockwise;
	private bool disabled;
	private float currAngle;

	private void Start()
	{
		Vector3 pos = transform.position;
		SpriteRenderer chainRender = transform.GetChild(0).GetComponent<SpriteRenderer>();
		float y = (float) (-0.75 + chainRender.size.y);
		rotPoint = new Vector3(pos.x, pos.y + y, pos.z);
		rotAxis = new Vector3(0,0, 1);
	}

	// Update is called once per frame
    void FixedUpdate()
    {
	    if(disabled)
		    return;

	    if (swingsCClockwise)
	    {
		    transform.RotateAround(rotPoint, rotAxis, speed);
		    currAngle += speed;
		    if (currAngle >= maxAngle)
		    {
			    swingsCClockwise = false;
		    }
	    }
	    else
	    {
		    transform.RotateAround(rotPoint, rotAxis, -speed);
		    currAngle -= speed;
		    if (currAngle <= -maxAngle)
		    {
			    swingsCClockwise = true;
		    }
	    }
    }

    public void Toggle(bool a)
    {
	    disabled = a;
    }
}
