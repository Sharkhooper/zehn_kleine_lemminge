using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLemming : MonoBehaviour, IKillTarget
{
	public GroupController groupController;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();

	}

	public void Die(GameObject other)
	{
		GetComponent<Rigidbody2D>().simulated = false;
		animator.SetBool("Life", false);
	}

	public void AnimEnds()
	{
		animator.SetBool("Dead", true);
		Debug.Log("Anim ends");
		groupController.RemoveLemmingFromGroup();
	}
}
