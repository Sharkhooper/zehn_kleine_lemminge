using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press  : MonoBehaviour, SwitchableObject
{
    // Start is called before the first frame update

    private GameObject player;
    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    [SerializeField] public float speed = 1.0f;
    
    
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
        
        
        Transform child = transform.GetChild(0);
        
        if (forward)
        {
           
            
            child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionEnd, Time.deltaTime * speed);
            
        }

        else
        {
            child.position = Vector3.MoveTowards(transform.GetChild(0).position, positionStart, Time.deltaTime * speed);
        }

        if ( child.position == positionStart)
        {
            forward = true;
        }

        if ( child.position==positionEnd)
        {
            forward = false;
        }
        
        


    }


    public void switchActivated()
    {
        throw new System.NotImplementedException();
    }

    public void switchDeactivated()
    {
        throw new System.NotImplementedException();
    }
}
