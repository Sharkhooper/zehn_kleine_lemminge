using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour
{
	int doubleTap = 0;
	float startZeit = 0;
	float oldMovementX = 0;
	Vector3 start, end, movement, richtungsVector, touchStart;
	Touch touch;
	private LemmingMovement lemming;

	private void Start()
	{
		lemming = GetComponent<LemmingMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				start = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				Debug.Log("Input START: " + start);
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
				Debug.Log("Input END: " + end);

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

						lemming.MoveHorizontal(0);
					}
					else
					{
						doubleTap = 0;
						lemming.MoveHorizontal(0);
					}
				}


				richtungsVector = end - start;
				//		Debug.Log("Richtung: " + richtungsVector.y);

				//Jump/Croach
				{
					if (richtungsVector.y > 3)
					{
						float tan = richtungsVector.y / richtungsVector.x;
						if (!(tan < 1 && tan > -1 && tan != 0))
						{
							//Debug.Log("Tan: " + tan + "   x-Wert: " + richtungsVector.x);
							lemming.Jump();
							lemming.MoveHorizontal(richtungsVector.x);

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
					else if (Mathf.Abs(richtungsVector.x) <= 1 && Mathf.Abs(richtungsVector.y) <= 1)
					{
						lemming.MoveHorizontal(end.x * 5);
					}
				}
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				movement = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				Debug.Log("Input MOVEMENT: " + movement);
				richtungsVector = movement - start;
				Debug.Log("Input RICHTUNGSVECTOR: " + richtungsVector);
				//lemming.MoveHorizontal(richtungsVector.x);
				//	Debug.Log("Touch movement: " + movement);
				//if (movement.x < 0 && start.x > 0)
				//{
				//	start = movement;
				//	lemming.MoveHorizontal(-1);
				//}
				//else if (movement.x > 0 && start.x < 0)
				//{
				//	start = movement;
				//	lemming.MoveHorizontal(1);
				//}
				if (Mathf.Abs(movement.x - start.x) > 1) oldMovementX = movement.x - start.x;
				//if (oldMovementX == 1 || oldMovementX == -1) oldMovementX = 0;
				lemming.MoveHorizontal(oldMovementX*5);
				//Debug.Log(oldMovementX);

			}
		}
	}


}//class

