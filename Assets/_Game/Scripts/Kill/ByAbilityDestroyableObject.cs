using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByAbilityDestroyableObject : MonoBehaviour,IKillTarget
{
    // Start is called before the first frame update
   
    public void Die(GameObject other)
    {
        Transform thisGameObject = gameObject.transform;



        if (gameObject.CompareTag("Burnable") && thisGameObject.CompareTag("Fire"))
        {




            Destroy(thisGameObject.gameObject);

        }
        else if (gameObject.CompareTag("Explosivable")&&thisGameObject.CompareTag("Bomb"))
        {
            
           

            Destroy(thisGameObject.gameObject);
        }

    }
 }

