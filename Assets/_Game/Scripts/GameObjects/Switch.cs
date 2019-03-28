using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [System.Serializable]
    private class BoolEvent : UnityEvent<bool>
    {
    }

    [SerializeField] private BoolEvent onStateChange;
    private bool statusActive=true;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(("Player"))) return;
       
        Debug.Log("Enter Trigger");
          
                statusActive = !statusActive;
                onStateChange.Invoke(statusActive);
        Debug.Log(statusActive);
    }

}
