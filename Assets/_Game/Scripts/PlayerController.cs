using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool IsGroupSelected;
	private LemmingMovement[] lemmings;
	public LemmingMovement ActiveLemming { get; set; }

    // Start is called before the first frame update
    void Start()
    {
	    lemmings = transform.GetComponentsInChildren<LemmingMovement>();
	    // DEBUG
	    ActiveLemming = GetComponent<LemmingMovement>();
	    Debug.Log("Active: " + ActiveLemming);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveLemmingFromGroup()
    {

    }

    public void KillGroup()
    {
	    foreach (var lemming in lemmings)
	    {

	    }
    }

    public void KillActiveLemming()
    {

    }
}
