using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour, IKillTarget
{
	[SerializeField] public LemmingMovement groupMovement;

	private GameManager gameManager;
	public bool IsGroupSelected;
	private bool blockedInput;
	bool isDirectionPositiv;
	public GameObject[] PlayableLemmings { get; set; }
	public GameObject[] DummyLemmings { get; set; }
	public Animator[] AllLemmingAnimator { get; set; }
	public SpriteRenderer[] AllLemmingSpriteRenderer { get; set; }

	public int ActiveLemmingIndex { get; set; }
	public GameObject ActiveLemming { get; set; }
	public LemmingMovement ActiveLemmingMovement { get; set; }
	public Animator ActiveLemmingAnimator { get; set; }
	private Vector3 ActiveLemmingGroupPosition { get; set; }
	private Color ActiveLemmingColor { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		AllLemmingAnimator = GetComponentsInChildren<Animator>();
		AllLemmingSpriteRenderer = GetComponentsInChildren<SpriteRenderer>();

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
		ActiveLemming = PlayableLemmings[ActiveLemmingIndex];
		ActiveLemmingMovement = ActiveLemming.GetComponent<LemmingMovement>();
		ActiveLemmingGroupPosition = ActiveLemming.transform.localPosition;
		ActiveLemmingColor = ActiveLemming.GetComponent<SpriteRenderer>().color;
		ActiveLemmingAnimator = ActiveLemming.GetComponent<Animator>();
	}

	public void RemoveLemmingFromGroup()
	{
		if (ActiveLemmingIndex + 1 <= PlayableLemmings.Length)
		{
			Destroy(ActiveLemming);

			ActiveLemming = PlayableLemmings[ActiveLemmingIndex];
			ActiveLemmingStatus(false);
		}
		else
		{
			Debug.Log("Verloren");
		}
	}

	public void ActivateGroup(bool pcButton)
	{
		blockedInput = true;

		float zCoordinate = ActiveLemming.transform.position.z;
		bool activeHitGroup = false;
		foreach(var collider in GetComponents<Collider2D>())
		{
			if (collider.isTrigger)
			{
				activeHitGroup = ActiveLemming.GetComponent<Collider2D>().IsTouching(collider);
			}
		}

		if (pcButton)
		{
			activeHitGroup = true;
		}

		if (zCoordinate == 0 && activeHitGroup)
		{
			LemmingEnterGroup();
		}
		else
		{
			LemmingExitGroup(zCoordinate);
		}

		blockedInput = false;
	}

	public void LemmingEnterGroup()
	{
		ActiveLemming.transform.localPosition = ActiveLemmingGroupPosition;
		ActiveLemming.GetComponent<SpriteRenderer>().color = ActiveLemmingColor;

		ActiveLemmingStatus(false);
		ActiveLemmingAnimator.SetBool("InGroup", true);
		IsGroupSelected = true;
	}

	private void LemmingExitGroup(float z)
	{
		ActiveLemming.transform.localPosition = new Vector3(2, 0, 0);
		ActiveLemming.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);

		ActiveLemmingStatus(true);
		ActiveLemmingAnimator.SetBool("InGroup", false);
		IsGroupSelected = false;
	}

	private void ActiveLemmingStatus(bool status)
	{
		ActiveLemming.GetComponent<Collider2D>().enabled = status;
		ActiveLemming.GetComponent<Rigidbody2D>().simulated = status;
		ActiveLemming.GetComponentInChildren<GroundTrigger>(true).ChangeFootStatus(status);

		gameManager.existSingleLemming = status;
	}

	public void MoveHorizontal(float direction)
	{
		if (direction != 0)
		{
			isDirectionPositiv = direction > 0;
		}
		if (IsGroupSelected && !blockedInput)
		{
			groupMovement.MoveHorizontal(direction);
			foreach (var animator in AllLemmingAnimator)
			{
				animator.SetFloat("Speed", Mathf.Abs(direction));
			}

			foreach (var sprite in AllLemmingSpriteRenderer)
			{
				sprite.flipX = isDirectionPositiv;
			}
		}
		else if (!blockedInput)
		{
			ActiveLemmingMovement.MoveHorizontal(direction);
			ActiveLemmingAnimator.SetFloat("Speed", Mathf.Abs(direction));
			ActiveLemming.GetComponent<SpriteRenderer>().flipX = isDirectionPositiv;
		}
	}

	public void Jump()
	{
		if (!IsGroupSelected && !blockedInput)
		{
			ActiveLemmingMovement.Jump(gameManager.SuperJumpActivated);
		}
	}

	public void DoubleTab(Touch touch)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);

		RaycastHit vHit;
		if (Physics.Raycast(ray.origin, ray.direction, out vHit))
		{
			if (vHit.transform.tag == "Group")
			{
				vHit.transform.GetComponent<GroupController>().ActivateGroup(false);
			}

		}
	}

	public void Die(GameObject other)
	{
		Debug.Log("Group gekillt");
	}
}
