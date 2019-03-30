using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour, IKillTarget
{
	[SerializeField] public LemmingMovement groupMovement;

	private GameManager gameManager;
	public bool IsGroupSelected;
	private bool blockedInput;
	private Rigidbody2D rbGroup;
	bool isDirectionPositiv;
	public GameObject[] PlayableLemmings { get; set; }
	public GameObject[] DummyLemmings { get; set; }
	public Animator[] AllLemmingAnimator { get; set; }
	public SpriteRenderer[] AllLemmingSpriteRenderer { get; set; }

	public Animator CoffinAnimator;
	public SpriteRenderer CoffinSprite;

	public CamController CamController { get; set; }

	public int ActiveLemmingIndex { get; set; }
	public GameObject ActiveLemming { get; set; }
	public LemmingMovement ActiveLemmingMovement { get; set; }
	public Animator ActiveLemmingAnimator { get; set; }
	private Vector3 ActiveLemmingGroupPosition { get; set; }
	private Color ActiveLemmingColor { get; set; }
	private Rigidbody2D ActiveLemmingRb { get; set; }

	private Abilities ActiveLemmingAbilities { get; set; }

	private bool LemmingEnter { get; set; }
	private bool LemmingExit { get; set; }
	[SerializeField] public GameObject[] Waypoints;

	// Start is called before the first frame update

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		gameManager.getInstance().currentLemmings = 7;

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
		SetActiveLemming(ActiveLemmingIndex);

		while (gameManager.getInstance().currentLemmings > gameManager.getInstance().MaxLevelLemming)
		{
			RemoveLemmingFromGroup();
		}

		rbGroup = GetComponent<Rigidbody2D>();

		CamController = GetComponent<CamController>();
		CamController.initTargets(this);
	}

	private void FixedUpdate()
	{
		if (!LemmingExit && !LemmingEnter)
		{
			foreach (var animator in AllLemmingAnimator)
			{
				if (animator != null)
				{
					//animator.SetFloat("Speed", Mathf.Abs(direction));
					animator.SetFloat("Speed", Mathf.Abs(rbGroup.velocity.x));
					animator.SetBool("Walke", rbGroup.velocity.x != 0);
				}
			}

			CoffinAnimator.SetFloat("Speed", Mathf.Abs(rbGroup.velocity.x));
			CoffinAnimator.SetBool("Walke", rbGroup.velocity.x != 0);

			if (IsGroupSelected)
			{
				foreach (var sprite in AllLemmingSpriteRenderer)
				{
					if (sprite != null)
					{
						sprite.flipX = isDirectionPositiv;
					}
				}

				CoffinSprite.flipX = isDirectionPositiv;
			}
			else
			{
				ActiveLemmingAnimator.SetFloat("Speed", Mathf.Abs(ActiveLemmingRb.velocity.x));
				ActiveLemmingAnimator.SetBool("Walke", ActiveLemmingRb.velocity.x != 0);
				ActiveLemmingAnimator.SetFloat("JumpDirection", ActiveLemmingRb.velocity.y);
				ActiveLemmingAnimator.SetBool("IsGrounded", ActiveLemmingMovement.IsGrounded);

				if (ActiveLemmingRb.velocity.y <= 0.1f)
				{
					//ActiveLemmingAnimator.SetBool("Jumping", false);
				}
			}
		}
		else
		{
			if (LemmingEnter)
			{
				MoveActiveEnter();
			}
			else
			{
				MoveActiveExit();
			}
		}
	}

	public void SetActiveLemming(float index)
	{
		ActiveLemming = PlayableLemmings[ActiveLemmingIndex];
		ActiveLemmingMovement = ActiveLemming.GetComponent<LemmingMovement>();
		ActiveLemmingGroupPosition = ActiveLemming.transform.localPosition;
		ActiveLemmingColor = ActiveLemming.GetComponent<SpriteRenderer>().color;
		ActiveLemmingAnimator = ActiveLemming.GetComponent<Animator>();
		ActiveLemmingRb = ActiveLemming.GetComponent<Rigidbody2D>();
		ActiveLemmingAbilities = ActiveLemming.GetComponent<Abilities>();
	}

	public void RemoveLemmingFromGroup()
	{
		gameManager.getInstance().currentLemmingText.text = "Leben: " + --gameManager.getInstance().currentLemmings;
		if (ActiveLemmingIndex + 1 < PlayableLemmings.Length)
		{
			Destroy(ActiveLemming);

			SetActiveLemming(++ActiveLemmingIndex);
			//Debug.Log((ActiveLemming));
			ActiveLemmingStatus(false);
		}
		else
		{
			gameManager.getInstance().GameOver();
		}
	}

	public void ActivateGroup(bool pcButton)
	{
		blockedInput = true;

		float zCoordinate = ActiveLemming.transform.position.z;
		bool activeHitGroup = false;
		foreach (var collider in GetComponents<BoxCollider2D>())
		{
			activeHitGroup = collider.bounds.Contains(ActiveLemming.transform.position);
		}

		if (zCoordinate == 0 && activeHitGroup)
		{
			LemmingEnterGroup();
		}
		else if (zCoordinate != 0)
		{
			LemmingExitGroup(zCoordinate);
		}

		blockedInput = false;
	}

	public void LemmingEnterGroup()
	{
		gameManager.getInstance().groupButton.enabled = false;


		//LemmingEnter = true;
		//MoveActiveEnter();

		ActiveLemming.transform.localPosition = ActiveLemmingGroupPosition;
		ActiveLemming.GetComponent<SpriteRenderer>().color = ActiveLemmingColor;

		ActiveLemmingStatus(false);
	}

	public void LemmingExitGroup(float z)
	{
		gameManager.getInstance().groupButton.enabled = true;

		ActiveLemming.transform.localPosition = new Vector3(ActiveLemming.transform.localPosition.x,
			ActiveLemming.transform.localPosition.y, 0);
		ActiveLemming.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);

		ActiveLemmingStatus(true);
	}

	private void ActiveLemmingStatus(bool status)
	{
		ActiveLemming.GetComponent<Collider2D>().enabled = status;
		ActiveLemming.GetComponent<Rigidbody2D>().simulated = status;
		ActiveLemming.GetComponentInChildren<GroundTrigger>(true).ChangeFootStatus(status);

		IsGroupSelected = !status;
		ActiveLemmingAnimator.SetBool("InGroup", !status);
		CoffinAnimator.SetBool("InGroup", !status);
		gameManager.getInstance().existSingleLemming = status;

		if (CamController == null)
		{
			CamController = GetComponent<CamController>();
		}
		CamController.FocusChange = true;

		if (status)
		{
			gameManager.getInstance().groupText.text = "Single";
		}
		else
		{
			gameManager.getInstance().groupText.text = "";
		}
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
			if (gameManager.getInstance().existSingleLemming)
			{
				ActiveLemmingMovement.MoveHorizontal(direction);
				ActiveLemmingAnimator.SetFloat("Speed", Mathf.Abs(direction));
				ActiveLemmingAnimator.SetBool("Walke", ActiveLemmingRb.velocity.x != 0);
				ActiveLemming.GetComponent<SpriteRenderer>().flipX = isDirectionPositiv;
			}
		}
		else if (!blockedInput)
		{
			ActiveLemmingMovement.MoveHorizontal(direction);
			ActiveLemmingAnimator.SetFloat("Speed", Mathf.Abs(direction));
			ActiveLemmingAnimator.SetBool("Walke", ActiveLemmingRb.velocity.x != 0);
			ActiveLemming.GetComponent<SpriteRenderer>().flipX = isDirectionPositiv;
		}
	}

	public void MoveHorizontal(Vector3 position)
	{
		if (IsGroupSelected && !blockedInput)
		{
			float direction = -((transform.position - position).x);
			if (direction != 0)
			{
				isDirectionPositiv = direction > 0;
			}

			groupMovement.MoveHorizontal(direction);
		}
		else if (!blockedInput)
		{
			float direction = -((ActiveLemmingMovement.transform.position - position).x);
			if (direction != 0)
			{
				isDirectionPositiv = direction > 0;
			}

			ActiveLemmingMovement.MoveHorizontal(direction);
			//ActiveLemmingAnimator.SetFloat("Speed", Mathf.Abs(direction));
			ActiveLemmingAnimator.SetBool("Walke", ActiveLemmingRb.velocity.x != 0);
			ActiveLemming.GetComponent<SpriteRenderer>().flipX = isDirectionPositiv;
		}
	}

	public void Jump()
	{
		if (!IsGroupSelected && !blockedInput)
		{
			ActiveLemmingMovement.Jump(gameManager.getInstance().SuperJumpActivated);
			ActiveLemmingAnimator.SetBool("IsGrounded", ActiveLemmingMovement.IsGrounded);
			//ActiveLemmingAnimator.SetBool("Jumping", true);
		}
	}

	public void DoubleTab(Touch touch)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		RaycastHit2D vHit = Physics2D.Raycast(ray.origin, ray.direction);
		if (vHit.collider != null)
		{
			if (vHit.transform.tag == "Group")
			{
				vHit.transform.GetComponent<GroupController>().ActivateGroup(false);
			}
		}
	}

	public void MoveActiveExit()
	{
		GameObject waypoint;
		if (ActiveLemmingGroupPosition.x > 0)
		{
			waypoint = Waypoints[1];
		}
		else
		{
			waypoint = Waypoints[0];
		}

		if (Vector3.Distance(waypoint.transform.position, ActiveLemming.transform.position) < 0.2f)
		{
			ActiveLemmingAnimator.SetBool("Walke", false);
			ActiveLemmingAnimator.SetFloat("Speed", 0);
			LemmingExit = false;
		}
		else
		{
			ActiveLemming.transform.position = Vector3.MoveTowards(ActiveLemming.transform.position,
				waypoint.transform.position, 1f * Time.deltaTime);
			ActiveLemmingAnimator.SetBool("Walke", true);
			ActiveLemmingAnimator.SetBool("IsGrounded", true);
			ActiveLemmingAnimator.SetFloat("Speed", 1);
			ActiveLemming.GetComponent<SpriteRenderer>().flipX = ActiveLemmingRb.velocity.x >= 0;
		}
	}

	public void MoveActiveEnter()
	{
		if (Vector3.Distance(ActiveLemmingGroupPosition, ActiveLemming.transform.localPosition) == 0)
		{
			ActiveLemmingAnimator.SetBool("Walke", false);
			ActiveLemmingAnimator.SetFloat("Speed", 0);
			LemmingEnter = false;
		}
		else
		{
			ActiveLemming.transform.localPosition = Vector3.MoveTowards(ActiveLemming.transform.localPosition,
				ActiveLemmingGroupPosition, 1f * Time.deltaTime);
			ActiveLemmingAnimator.SetBool("Walke", true);
			ActiveLemmingAnimator.SetBool("IsGrounded", true);
			ActiveLemmingAnimator.SetFloat("Speed", 1);
			ActiveLemming.GetComponent<SpriteRenderer>().flipX = ActiveLemmingRb.velocity.x >= 0;
		}
	}

	public void Die(GameObject other)
	{
		gameManager.getInstance().GameOver();
	}


	public void useFire()
	{
		ActiveLemmingAbilities.Fire();
	}

	public void useBomb()
	{
		ActiveLemmingAbilities.Bomb();
	}
}
