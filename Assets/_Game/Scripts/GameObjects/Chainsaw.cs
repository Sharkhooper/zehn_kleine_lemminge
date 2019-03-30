using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
	// Start is called before the first frame update

	private Vector3 positionEnd;
	private Vector3 positionStart;
	private bool forward = true;
	[SerializeField] public float speed = 5.0f;
	[SerializeField] public bool circle;
	[SerializeField] public Vector2[] vectorList;
	[SerializeField] private float rotationSpeed = 1.0f;
	private int nextPoint;
	private int vecListLength;
	private Vector3[] vec3List;
	private Transform chainsawBlade;

	public bool Moving { get; set; } = true;


	void Start()
	{
		positionStart = transform.position;

		vecListLength = vectorList.Length;
		nextPoint = 1;

		vec3List = new Vector3[vecListLength];

		for (int num = 0; num < vecListLength; num++)
		{
			vec3List[num] = new Vector3(vectorList[num].x + transform.GetChild(0).position.x,
				vectorList[num].y + transform.GetChild(0).position.y, 0f);
		}

		chainsawBlade = transform.GetChild(0);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		chainsawBlade.Rotate(new Vector3(0f, 0f, 20f * rotationSpeed * Time.deltaTime));

		if (Moving)
		{
			Transform child = transform.GetChild(0);
			if (!circle)
			{
				child.position =
					Vector3.MoveTowards(child.position, vec3List[nextPoint], Time.deltaTime * speed);


				if (child.position == vec3List[0])
				{
					forward = true;
				}

				if (child.position == vec3List[vecListLength - 1])
				{
					forward = false;
				}

				if (child.position == vec3List[nextPoint])
				{
					if (forward)
					{
						nextPoint += 1;
					}

					else
					{
						nextPoint -= 1;
					}
				}
			}

			else
			{
				if (child.position == vec3List[nextPoint])
				{
					nextPoint = (nextPoint + 1) % vecListLength;
				}

				child.position =
					Vector3.MoveTowards(child.position, vec3List[nextPoint], Time.deltaTime * speed);
			}
		}
		else
		{
			Transform child = transform.GetChild(0);

			if (child.position != vec3List[0])
			{
				if (child.position == vec3List[nextPoint])
				{
					nextPoint -= 1;
				}

				forward = false;
				child.position =
					Vector3.MoveTowards(child.position, vec3List[nextPoint], Time.deltaTime * speed);
			}
		}
	}
}
