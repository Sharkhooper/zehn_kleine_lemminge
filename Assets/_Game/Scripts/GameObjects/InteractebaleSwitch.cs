using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractebaleSwitch : MonoBehaviour
{
    [System.Serializable]
    private class BoolEvent : UnityEvent<bool>
    {
    }

    [SerializeField] private BoolEvent onStateChange;
    private bool switchActiv=true;
    private bool groupIn = false;
    private bool playerIn = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        if (groupIn)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Group"))
        {
            groupIn = true;
            Debug.Log("Group Enter Trigger");
            playerIn = true;


        }
        else
        {
            if (!other.gameObject.CompareTag(("Player"))) return;

            Debug.Log("Lemming Enter Trigger");
            playerIn = true;


        }
        
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            groupIn = false;
            playerIn = false;
        }
        else
        {
            if (!other.gameObject.CompareTag(("Player"))) return;

            if (groupIn) return;
            playerIn = false;


        }
    }


    public void ActionButtonPressed()
    {
        if (playerIn)
        {


            switchActiv = !switchActiv;
            onStateChange.Invoke(switchActiv);
            Debug.Log(switchActiv);
        }
    }
}
