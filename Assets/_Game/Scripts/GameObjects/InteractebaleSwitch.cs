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
    private GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }


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
            gameManagerScript.ActionButtonEnable(playerIn,this);

        }
        else
        {
            if (!other.gameObject.CompareTag(("Player"))) return;

            Debug.Log("Lemming Enter Trigger");
            playerIn = true;
            gameManagerScript.ActionButtonEnable(playerIn,this);


        }
        
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            groupIn = false;
            playerIn = false;
            gameManagerScript.ActionButtonEnable(playerIn,this);
        }
        else
        {
            if (!other.gameObject.CompareTag(("Player"))) return;

            if (groupIn) return;
            playerIn = false;
            gameManagerScript.ActionButtonEnable(playerIn,this);


        }
        
        
    }


    public void ActionButtonPressed()
    {
        Debug.Log("Button pressed");
        
        if (playerIn)
        {


            switchActiv = !switchActiv;
            onStateChange.Invoke(switchActiv);
            Debug.Log(switchActiv);
        }
    }
}
