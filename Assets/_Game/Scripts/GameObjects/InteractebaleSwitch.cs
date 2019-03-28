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
    private bool statusActive=true;
    private bool groupIn = false;
    
    
}
