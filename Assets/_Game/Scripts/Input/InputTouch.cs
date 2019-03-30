using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputTouch : MonoBehaviour
{
	int doubleTap = 0;
	float startZeit = 0;
	float tempTime = 0;
	Vector3 start, end, movement, richtungsVector, touchStart, tempStart, originStart;
	Touch touch;
	[SerializeField] public GroupController groupController;



	private void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (Input.touchCount > 0)
		{
	/*		Ray ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit2D vHit = Physics2D.Raycast(ray.origin, ray.direction);
			if (vHit.collider != null)
			{
				if (vHit.transform.tag == "Button")
				{
					vHit.transform.GetComponent<GroupController>().ActivateGroup(false);
				}
			}

	*/



			touch = Input.GetTouch(0);
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{

				start = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				originStart = start;
				groupController.MoveHorizontal(start);

				if (doubleTap == 0)
				{
					startZeit = Time.time;
					touchStart = start;
				}
				else if (!(Mathf.Abs(touchStart.x - start.x) <= 1f && Mathf.Abs(touchStart.y - start.y) <= 1f))
				{
					doubleTap = 0;
					touchStart = start;
					startZeit = Time.time;
				}
				else
				{
					tempTime = Time.time;
					tempStart = start;
				}
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				end = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

				//DoubleTap
				{
					if (doubleTap == 0)
					{
						doubleTap++;
					}
					else if (doubleTap == 1 && Time.time - startZeit < 1f)
					{
						doubleTap = 0;
						groupController.DoubleTab(touch);
					}
					else
					{
						doubleTap = 0;
						//groupController.MoveHorizontal(end);
						startZeit = tempTime;
						touchStart = tempStart;
					}
				}

				richtungsVector = end - originStart;

				//Jump
				{
					if (richtungsVector.y > 3)
					{
						float tan = richtungsVector.y / richtungsVector.x;
						if (!(tan < 1 && tan > -1 && tan != 0))
						{
							groupController.ActiveLemmingMovement.BrakeMovement();
							groupController.Jump();
							groupController.MoveHorizontal(richtungsVector);
						}
					}
					//Vlt nicht auf nem Button möglich sein
					else if (Mathf.Abs(richtungsVector.x) <= 2 && Mathf.Abs(richtungsVector.y) <= 2)
					{
						groupController.MoveHorizontal(end);
					}
				}
				//groupController.MoveHorizontal(0);

			}

			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if ((Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)) - movement).x > 0.5) start = movement;

				else if ((Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)) - movement).x < -0.5) start = movement;


				movement = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				groupController.MoveHorizontal((movement.x - start.x));


				if (movement.y - originStart.y > 3)
				{
					float tan = movement.y - originStart.y / movement.x - originStart.x;
					if (!(tan < 1 && tan > -1 && tan != 0))
					{
						groupController.ActiveLemmingMovement.BrakeMovement();
						groupController.Jump();
						groupController.MoveHorizontal(movement);
					}
				}
			}
		}
		else
		{
			groupController.MoveHorizontal(0);
		}

		if(Input.touchCount==4)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}

	}//Update


}//class

