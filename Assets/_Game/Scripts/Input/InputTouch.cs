using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour
{
	int doubleTap = 0;
	float startZeit = 0;
	float oldMovementX = 0;
	Vector3 start, end, movement, richtungsVector, touchStart, originStart;
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
			touch = Input.GetTouch(0);

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				start = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				originStart = start;
				//start = touch.position;

				//Debug.Log("Input START: " + start);
				if (doubleTap == 0)
				{
					startZeit = Time.time;
					touchStart = start;
				}
				else if (!(Mathf.Abs(touchStart.x - start.x) <= 1f && Mathf.Abs(touchStart.y - start.y) <= 1f))
				{
					//		Debug.Log("DoppelTap Fail");
					doubleTap = 0;
				}
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				end = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				//end = touch.position;
				//Debug.Log("Input END: " + end);

				//DoubleTap
				{
					if (doubleTap == 0)
					{
						doubleTap++;
					}
					else if (doubleTap == 1 && Time.time - startZeit < 0.5f)
					{
						doubleTap = 0;
						//				Debug.Log("DoppelTap erfolgt");

						//=> Vector sollte übergeen werden, testen ob da LemmingGruppe ist
						//RichtigeMethode
						groupController.DoubleTab(touch);
					}
					else
					{
						doubleTap = 0;
						groupController.MoveHorizontal(0);
					}
				}


				richtungsVector = end - originStart;
				//		Debug.Log("Richtung: " + richtungsVector.y);

				//Jump/Croach

				{
					if (richtungsVector.y > 3)
					{
						float tan = richtungsVector.y / richtungsVector.x;
						if (!(tan < 1 && tan > -1 && tan != 0))
						{
							//Debug.Log("Tan: " + tan + "   x-Wert: " + richtungsVector.x);
							groupController.Jump();
							//lemming.MoveHorizontal(richtungsVector.x);

							//Aufstehen
						}
					}
					else if (richtungsVector.y < -3)
					{
						float tan = richtungsVector.y / richtungsVector.x;
						if (!(tan < 1 && tan > -1 && tan != 0))
						{
							Debug.Log("Bück dich du Luder!");
							//bücken
						}
					}
					//Vlt nicht auf nem Button möglich
					else if (Mathf.Abs(richtungsVector.x) <= 2 && Mathf.Abs(richtungsVector.y) <= 2)
					{
						//Debug.Log("Input Punkt Richtung: " + -((Camera.main.gameObject.transform.position - end).x));
						groupController.MoveHorizontal(-((Camera.main.gameObject.transform.position - end).x));
					}
				}
				groupController.MoveHorizontal(0);

			}

			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if ((Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)) - movement).x > 0) start = movement;

				movement = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				groupController.MoveHorizontal((movement.x - start.x));


				if (movement.y-originStart.y > 3)
				{
					float tan = movement.y - originStart.y / movement.x - originStart.x;
					if (!(tan < 1 && tan > -1 && tan != 0))
					{
						//Debug.Log("Tan: " + tan + "   x-Wert: " + richtungsVector.x);
						groupController.Jump();
						//lemming.MoveHorizontal(movement.y - originStart.y);

						//Aufstehen
					}
				}

			}
		}
	}


}//class

