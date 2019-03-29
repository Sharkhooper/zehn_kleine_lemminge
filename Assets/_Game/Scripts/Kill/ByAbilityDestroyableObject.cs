using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByAbilityDestroyableObject : MonoBehaviour,IKillTarget
{
    // Start is called before the first frame update
   
    public void Die(GameObject other)
    {
        Transform otherGameObject= other.transform;
        Debug.Log("Collided");


        Debug.Log(other);
        Debug.Log(otherGameObject);
        if (gameObject.CompareTag("Burnable") && otherGameObject.CompareTag("Fire"))
        {




            Destroy(otherGameObject.gameObject);

        }
        else if (gameObject.CompareTag("Explosivable")&&otherGameObject.CompareTag("Bomb"))
        {
            
          //  Debug.Log("Collided if open");

            Destroy(otherGameObject.gameObject);
        }

    }
 }

