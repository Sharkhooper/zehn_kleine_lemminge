using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Elevator  : MonoBehaviour,ITrigger
{
    // Start is called before the first frame update

    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    private bool entered = false;
    private bool top = false;
    private Transform parent;
    private float timer;
    private bool left = true;
    [SerializeField] public float delay = 1f;
   // [SerializeField] public float speed = 1f;
    [SerializeField] public float duration = 3.0f;
    private Rigidbody2D rigiBody;
    private float eTime = 0;
   // private bool isMoving = false;
    
    
    void Start()
    {
        parent = transform.parent;

        parent.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        timer = delay;
        //eTime = duration;
        positionStart = parent.position;
        positionEnd = parent.GetChild(1).position;
        rigiBody = GetComponent<Rigidbody2D>();


    }

   

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(isMoving) return;
        
        if (forward && entered)
        {/*
            isMoving = true;
            rigiBody.DOMove(positionEnd, duration).onComplete += () => { isMoving = false; };
            */
           if ( eTime>3)
           {
               rigiBody.position = positionEnd;
           }
           else
           {
               rigiBody.MovePosition(Vector3.Lerp(positionStart, positionEnd, eTime / duration));
               eTime += Time.fixedDeltaTime;
           }
           //transform.position = Vector3.MoveTowards(transform.position, positionEnd, Time.deltaTime * speed);

        }

        else if(top)
        {
           /* isMoving = true;
            rigiBody.DOMove(positionStart, duration).onComplete += () => { isMoving = false; };
            /**/
           // transform.position = Vector3.MoveTowards(transform.position, positionStart, Time.deltaTime * speed);

           if (eTime<0)
           {
               rigiBody.position=positionStart;
           }
           else
           {

               rigiBody.MovePosition(Vector3.Lerp(positionStart, positionEnd, eTime / duration));
               eTime -= Time.fixedDeltaTime;
           }

        }

        
        if ( transform.position == positionStart)
        {
            forward = true;
            top = false;
           // eTime = 0;
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
            //eTime = 0;

            timer -= Time.deltaTime;
        }
        else
        {
            
            top = true;
        }

      
     

       
        


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
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
