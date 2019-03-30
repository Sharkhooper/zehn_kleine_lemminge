using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Explosion : Deadly
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Explosivable"))
        {



            //Debug.Log("Collided");
            if (isActive)
            {

                ExecuteEvents.Execute<IKillTarget>(other.gameObject, null, (x, y) => x.Die(gameObject));
            }
        }

    }
}
