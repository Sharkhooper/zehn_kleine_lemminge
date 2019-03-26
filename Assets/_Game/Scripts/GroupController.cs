using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour
{
	public bool IsGroupSelected;
	private LemmingMovement[] lemmings;
	private LemmingMovement activeLemming;

    // Start is called before the first frame update
    void Start()
    {
	    lemmings = transform.GetComponentsInChildren<LemmingMovement>();
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
