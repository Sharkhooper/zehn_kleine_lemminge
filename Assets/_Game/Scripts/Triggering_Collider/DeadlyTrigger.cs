using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyTrigger : MonoBehaviour,ITrigger
{
    // Start is called before the first frame update

    public bool statusActive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    public void OnLemmingEnter()
    {
        //
        if (statusActive)
        {
        

        Debug.Log("Test");
    }
}

    public void OnLemmingExit()
    {
     //   throw new System.NotImplementedException();
    }

    public void OnGroupEnter()
    {
       // throw new System.NotImplementedException();
    }

    public void OnGroupExit()
    {
       // throw new System.NotImplementedException();
    }
}
