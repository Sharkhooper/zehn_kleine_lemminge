using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator  : MonoBehaviour,ITrigger
{
    // Start is called before the first frame update

    private GameObject player;
    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    private bool entered = false;
    private bool top = false;
    private Transform parent;
    private float timer;
    private bool left = true;
    [SerializeField] public float delay = 1f;
    [SerializeField] public float speed = 1.0f;
    
    
    void Start()
    {
        parent = transform.parent;

        parent.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        timer = delay;
        
        positionStart = parent.position;
        positionEnd = parent.GetChild(1).position;
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        
        if (forward && entered)
        {
           
            
            transform.position = Vector3.MoveTowards(transform.position, positionEnd, Time.deltaTime * speed);
            
        }

        else if(top)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionStart, Time.deltaTime * speed);
        }

        if ( transform.position == positionStart)
        {
            forward = true;
            top = false;
            timer = delay;

            if (left)
            {
                entered = false;
            }
        }

        if ( transform.position==positionEnd && timer > 0)
        {
            top = false;
            
            forward = false;
          
            Debug.Log(timer);

            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log(timer);
            top = true;
        }

    }


    public void OnLemmingEnter()
    {
        entered = true;
        left = false;
    }

    public void OnLemmingExit()
    {
       left = true;

    }

    public void OnGroupEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnGroupExit()
    {
        throw new System.NotImplementedException();
    }
}
