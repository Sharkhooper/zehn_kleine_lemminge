using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press  : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    [SerializeField] public float speed = 1.0f;
    private Transform child;
    private bool moving=true;
    
    void Start()
    {
        

        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        
        
        child = transform.GetChild(0);
        positionStart = transform.position;
        positionEnd = transform.GetChild(1).position;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
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
        }

        if (child.position == positionEnd)
        {
            forward = false;
        }


    }
    else

    {

            if (child.position != positionStart)
            {
               

                child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionStart, Time.deltaTime * speed);



            }
        }

    }

    public void SwitchPressStatus(bool status)
    {
        moving = status;

        Debug.Log("status ist "+status);
        transform.GetChild(0).GetChild(1).GetComponent<Deadly>().isActive=status;
    }


}
