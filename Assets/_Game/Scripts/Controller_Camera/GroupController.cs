using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour, IKillTarget
{
	[SerializeField] public LemmingMovement groupMovement;

	private GameManager gameManager;
	public bool IsGroupSelected;
	public GameObject[] PlayableLemmings { get; set; }
	public GameObject[] DummyLemmings { get; set; }
	public Animator[] AllLemmingAnimator { get; set; }
	public int ActiveLemmingIndex { get; set; }
	public LemmingMovement ActiveLemming { get; set; }
	public Animator ActiveLemmingAnimator { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		AllLemmingAnimator = GetComponentsInChildren<Animator>();

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
		if(ActiveLemmingIndex +1 <= PlayableLemmings.Length)
		{
			ActiveLemmingIndex++;
		}
		else
		{
			Debug.Log("Verloren");
		}
	}

	public void ToggleMovementGroup()
	{

	}

	public void MoveHorizontal(float direction)
	{
		if (IsGroupSelected)
		{
			groupMovement.MoveHorizontal(direction);
			foreach(var animator in AllLemmingAnimator)
			{
				animator.SetFloat("Speed", Mathf.Abs(direction));
			}
		}
		else
		{
			ActiveLemming.MoveHorizontal(direction);
			PlayableLemmings[ActiveLemmingIndex].GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(direction));
		}
	}

	public void Jump()
	{
		if (!IsGroupSelected)
		{
			ActiveLemming.Jump(gameManager.SuperJumpActivated);
		}
	}

	public void Die()
	{
		Debug.Log("Group gekillt");
	}
}
