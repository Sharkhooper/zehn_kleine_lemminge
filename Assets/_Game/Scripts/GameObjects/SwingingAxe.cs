using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
	[SerializeField] private float speed = 30f;
	private float currentSpeed = 2f;
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
		rotAxis = new Vector3(0, 0, 1);

		speed = 1 / speed;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (disabled)
			return;

		transform.RotateAround(rotPoint, rotAxis, -currentSpeed);
		currAngle -= currentSpeed;

		if (currAngle > 0)
		{
			currentSpeed += speed;
		}
		else
		{
			currentSpeed -= speed;
		}
	}

	public void Toggle(bool a)
	{
		disabled = a;
	}
}
