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
				//Debug.Log("Touch gestartet: " + start);
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
				//Debug.Log("Touch beendet: " + end);
				if (doubleTap == 0)
				{
					doubleTap++;
				}
				else if (doubleTap == 1 && Time.time - startZeit < 0.5f)
				{
					doubleTap = 0;
					//				Debug.Log("DoppelTap erfolgt");

					//=> Vector sollte übergeen werden, testen ob da LemmingGruppe ist
				}
				else
				{
					doubleTap = 0;
				}
				lemming.MoveHorizontal(0);
				richtungsVector = end - start;
				//		Debug.Log("Richtung: " + richtungsVector.y);
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
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				movement = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				//richtungsVector = movement - start; 
				//	Debug.Log("Touch movement: " + movement);
				if (Mathf.Abs(movement.x - start.x) >= 1) oldMovementX = movement.x - start.x;

				Debug.Log("Touch movement: " + oldMovementX);

				lemming.MoveHorizontal(oldMovementX);
			}
		}
	}


}//class

