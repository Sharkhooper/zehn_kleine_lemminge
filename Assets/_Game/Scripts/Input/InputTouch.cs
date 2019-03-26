using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour
{
	int i = 0;
	int doubleTap = 0;
	float startZeit = 0;
	Vector3 start, end, movement, richtungsvector;
	Touch touch;

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				touch = Input.GetTouch(0);
				start = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				Debug.Log("Touch gestartet: " + start);
				if (doubleTap == 0)
				{
					startZeit = Time.time;
				}
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				//	end = Camera.main.ScreenToWorldPoint(touch.position);
				end = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				Debug.Log("Touch beendet: " + end);
				if (doubleTap == 0)
				{
					doubleTap++;
				}
				else if (doubleTap == 1 && Time.time - startZeit < 0.5f)
				{
					doubleTap = 0;
					Debug.Log("DoppelKlick erfolgt");
					//=> Vector sollte übergeen werden
				}
				else
				{
					doubleTap = 0;
				}
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				//movement = Camera.main.ScreenToWorldPoint(touch.position);
				movement = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				Vector3 richtungsVector = movement - start; //new Vector3(movement.x - start.x, movement.y - start.y,0)
				Debug.Log("Touch movement: " + movement);
				transform.position = movement;
			}


		}


	}


}//class

