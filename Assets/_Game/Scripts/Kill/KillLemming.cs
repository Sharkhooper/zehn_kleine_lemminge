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
		animator.SetBool("Life", false);
		groupController.RemoveLemmingFromGroup();

		Debug.Log("Lemming gekillt");
	}
}
