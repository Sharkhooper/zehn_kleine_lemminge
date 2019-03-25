using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 start, movement, end;
				start = Camera.main.ScreenToWorldPoint(touch.position);
				Debug.Log("Touch gestartet: " + start);
			
			 if (touch.phase == TouchPhase.Ended)
			{
				end = Camera.main.ScreenToWorldPoint(touch.position);
				Debug.Log("Touch beendet: " + end);
			}
			else if(touch.phase == TouchPhase.Moved)
			{
				movement = Camera.main.ScreenToWorldPoint(touch.position);
				Vector3 richtungsVector = movement-start; //new Vector3(movement.x - start.x, movement.y - start.y,0)
				Debug.Log("Touch movement: " + richtungsVector);
			}


		}


    }


}//class
