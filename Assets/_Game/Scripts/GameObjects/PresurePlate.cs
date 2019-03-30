using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Events;

public class PresurePlate : MonoBehaviour
{
    [System.Serializable]
    private class BoolEvent : UnityEvent<bool>
    {
    }

    [SerializeField] private BoolEvent onStateChange;
    private bool statusActive=true;
    private bool groupIn = false;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
           // Debug.Log("Group Enter Trigger");
          
            statusActive = !statusActive;
            onStateChange.Invoke(statusActive);
          //  Debug.Log(statusActive);
            
          if (!statusActive)
          {
              // SpriteOn
              spriteRenderer.sprite = spriteOn;
          }
          else
          {
              // SpriteOff
              spriteRenderer.sprite = spriteOff;
          }
        }
        else
        {
            if (!other.gameObject.CompareTag(("Player"))) return;

            Debug.Log("Lemming Enter Trigger");

            statusActive = !statusActive;
            onStateChange.Invoke(statusActive);
            Debug.Log(statusActive);
            
            if (!statusActive)
            {
                // SpriteOn
                spriteRenderer.sprite = spriteOn;
            }
            else
            {
                // SpriteOff
                spriteRenderer.sprite = spriteOff;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            groupIn = false;
        }
    }
}
