using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ByAbilityDestroyableObject : MonoBehaviour,IKillTarget
{
    // Start is called before the first frame update
   
    [System.Serializable]
    private class BoolEvent : UnityEvent<bool>
    {
    }

    [SerializeField] private BoolEvent onStateChange;
    private bool statusActive=true;
    
    
    public void Die(GameObject other)
    {
        Transform otherGameObject= other.transform;
        //Debug.Log("Collided");


        //Debug.Log(other);
      //  Debug.Log(otherGameObject);
        if (gameObject.CompareTag("Burnable") && otherGameObject.CompareTag("Fire"))
        {



            

            Destroy(gameObject);
            
            Destroy(otherGameObject.gameObject);

        }
        else if (gameObject.CompareTag("Explosivable")&&otherGameObject.CompareTag("Explosion"))
        {
            Debug.Log("Destroy is with bomb");
          //  Debug.Log("Collided if open");

          onStateChange.Invoke(true);
            Destroy(gameObject);
        }

    }
 }

