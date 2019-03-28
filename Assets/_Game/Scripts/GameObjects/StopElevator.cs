using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopElevator : MonoBehaviour
{
    // Start is called before the first frame update


    private Elevator parentScript;
    private void Start()
    {
       parentScript= transform.GetComponentInParent<Elevator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        parentScript.stopMoving = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        parentScript.stopMoving = false;
    }
}
