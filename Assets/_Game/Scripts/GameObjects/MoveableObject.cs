using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Vector3 positionEnd;
    private Vector3 positionStart;
    [SerializeField] public float speed = 1.0f;
    private Transform child;
    private bool forward = true;
    private Deadly deadly;
    private bool moveObject =false;
    
    void Start()
    {
        

        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        
        child = transform.GetChild(0);
        positionStart = transform.position;
        positionEnd = transform.GetChild(1).position;
        
        deadly=transform.GetChild(0).GetChild(1).GetComponent<Deadly>();


    }
    
    
    void FixedUpdate()
    {
        if (moveObject)
        {

            if (forward)
            {


                child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionEnd, Time.deltaTime * speed);

            }

            else
            {
                child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionStart, Time.deltaTime * speed);
            }

            if (child.position == positionStart)
            {
                forward = true;
                moveObject = false;
                deadly.isActive=moveObject;
            }

            if (child.position == positionEnd)
            {
                forward = false;
                moveObject = false;
                deadly.isActive=moveObject;
            }

        }

    }


    public void SwitchMoveObjectStatus(bool a)
    {
        if (moveObject == false)
        {
            moveObject = !moveObject;


            deadly.isActive = moveObject;
        }

    }


}
