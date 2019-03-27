using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{ // Start is called before the first frame update

    private Vector3 positionEnd;
    private Vector3 positionStart;
    private bool forward = true;
    [SerializeField] public float speed = 4.0f;
    [SerializeField] public Vector2[] vectorList;
    private int nextPoint;
    private int vecListLength;
    private Vector3[] vec3List;
    
    
    
    void Start()
    {

        
        positionStart = transform.position;

        if (vectorList.Length <= 1)
        {

            positionEnd = transform.GetChild(1).position;

        }
        else
        {
            vecListLength = vectorList.Length;
            nextPoint = 1;
            
            vec3List= new Vector3[vecListLength];

            for (int num = 0;num<vecListLength;num++) {
                
                vec3List[num]= new Vector3( vectorList[num].x, vectorList[num].y,0f);
                
            }

        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform child = transform.GetChild(0);
        if (vectorList.Length <= 1)
        {

            if (forward)
            {


                child.position =
                    Vector3.MoveTowards(transform.GetChild(0).position, positionEnd, Time.deltaTime * speed);

            }

            else
            {
                child.position =
                    Vector3.MoveTowards(transform.GetChild(0).position, positionStart, Time.deltaTime * speed);
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
            

            if (child.position == vec3List[nextPoint])
            {
                nextPoint = (nextPoint + 1) % vecListLength;

            }



        }


    }




}
