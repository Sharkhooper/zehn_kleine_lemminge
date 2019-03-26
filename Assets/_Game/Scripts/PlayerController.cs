using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool IsGroupSelected;
	private GameObject[] lemmings;
	public LemmingMovement ActiveLemming { get; set; }

    // Start is called before the first frame update
    void Start()
    {
	    lemmings = new GameObject[transform.childCount];
	    for (int i = 0; i < transform.childCount; i++)
	    {
		    lemmings[i] = transform.GetChild(i).gameObject;
	    }

	    ActiveLemming = GetComponent<LemmingMovement>();
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

    public void ToggleMovementGroup()
    {

    }
}
