using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour,IKillTarget
{
    // Start is called before the first frame update
   


    public void Die(GameObject other)
    {
        Transform thisGameObject = gameObject.transform;

        
        while (thisGameObject.parent!=null)
        {
            thisGameObject = thisGameObject.parent;
        }
       
        Destroy(thisGameObject.gameObject);
        
    }
}
