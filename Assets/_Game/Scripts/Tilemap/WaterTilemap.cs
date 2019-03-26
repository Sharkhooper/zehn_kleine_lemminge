using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTilemap : MonoBehaviour, ITrigger
{
	private LemmingMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
		playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<LemmingMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerEnter()
    {
		// TODO: Modify air resistance in LemmingMovement
    }

    public void OnPlayerExit()
    {
	   // TODO: Reset air resistance in LemmingMovement
    }
}
