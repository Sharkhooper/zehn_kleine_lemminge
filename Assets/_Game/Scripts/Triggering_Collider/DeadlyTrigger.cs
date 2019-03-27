using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyTrigger : MonoBehaviour,ITrigger
{
    // Start is called before the first frame update
   

    public void OnLemmingEnter()
    {
        //TODO
        
        Debug.Log("Test");
    }

    public void OnLemmingExit()
    {
        throw new System.NotImplementedException();
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
