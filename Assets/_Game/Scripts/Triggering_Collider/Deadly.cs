using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Deadly : MonoBehaviour
{
    // Start is called before the first frame update

    [FormerlySerializedAs("statusTriggerable")] public bool isActive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Collided");
        if (isActive)
        {
          
            ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {  
        //Debug.Log("Collided");
        if (isActive)
        {
          
            ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
        }
    }
}
