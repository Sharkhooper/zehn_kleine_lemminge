using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalacnite : MonoBehaviour
{
    // Start is called before the first frame update
    private bool active=false;
    [SerializeField] private int timer=10;

    public void SwitchFallingStatus(bool a)
    {
        
        
        if (active)
        {
         return;   
        }

        Debug.Log("Testus");
        active = true;
        Rigidbody2D rigibod =gameObject.GetComponent<Rigidbody2D>();
        
        Debug.Log(rigibod);
        Debug.Log(rigibod.simulated);
        rigibod.simulated = true;
        Debug.Log(rigibod.simulated);


        //new WaitForSeconds(timer);
        //rigibod.simulated = false;

    }
}
