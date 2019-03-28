using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour
{
	[SerializeField] GroupMovement groupMovement;

	public bool IsGroupSelected;
	public GameObject[] PlayableLemmings { get; set; }
	public GameObject[] DummyLemmings { get; set; }
	public int ActiveLemmingIndex { get; set; }
	public LemmingMovement ActiveLemming { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		int lemmingsCount = transform.GetChild(0).transform.childCount;
		PlayableLemmings = new GameObject[lemmingsCount];

		for (int i = 0; i < lemmingsCount; i++)
		{
			PlayableLemmings[i] = transform.GetChild(0).transform.GetChild(i).gameObject;
		}

		lemmingsCount = transform.GetChild(1).transform.childCount;
		DummyLemmings = new GameObject[lemmingsCount];

		for (int i = 0; i < lemmingsCount; i++)
		{
			DummyLemmings[i] = transform.GetChild(1).transform.GetChild(i).gameObject;
		}

		ActiveLemmingIndex = 0;
		ActiveLemming = PlayableLemmings[ActiveLemmingIndex].GetComponent<LemmingMovement>();
	}

	public void RemoveLemmingFromGroup()
	{

	}

	public void KillGroup()
	{
		foreach (var lemming in PlayableLemmings)
		{

		}
	}

	public void KillActiveLemming()
	{

	}

	public void ToggleMovementGroup()
	{

	}

	public void MoveHorizontal(float direction)
	{
		if (IsGroupSelected)
		{
			
		}
		else
		{
			ActiveLemming.MoveHorizontal(direction);
		}
	}

	public void Jump()
	{
		if (!IsGroupSelected)
		{
			ActiveLemming.Jump();
		}
	}

}
