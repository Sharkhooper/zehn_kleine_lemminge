using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press  : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    [SerializeField] public float speed = 1.0f;

    private bool moving=true;
    
    void Start()
    {
        

        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        
        
        positionStart = transform.position;
        positionEnd = transform.GetChild(1).position;
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {

        Transform child = transform.GetChild(0);

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
            Transform child = transform.GetChild(0);

            if (child.position != positionStart)
            {
               

                child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionStart, Time.deltaTime * speed);



            }
        }

    }

    public void switchPressStatus(bool status)
    {
        moving = status;

        Debug.Log("status ist "+status);
        transform.GetChild(0).GetChild(1).GetComponent<DeadlyTrigger>().statusActive=status;
    }


}
