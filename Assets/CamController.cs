using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
	public Cinemachine.CinemachineTargetGroup targetGroup;
	private GroupController groupController;

	public float moveTime = 1f;

	private float MoveWeightActive { get; set; }
	private float MoveWeightGroup { get; set; }

	public bool FocusChange { get; set; }

	// Start is called before the first frame update
	void Start()
	{
		

	}

	public void initTargets(GroupController group)
	{
		groupController = group;

		targetGroup.m_Targets = new Cinemachine.CinemachineTargetGroup.Target[groupController.PlayableLemmings.Length + 1];

		targetGroup.m_Targets[0].target = groupController.gameObject.transform;
		targetGroup.m_Targets[0].weight = 1;
		targetGroup.m_Targets[0].radius = 5;

		for (int i = 1; i < targetGroup.m_Targets.Length; i++)
		{
			targetGroup.m_Targets[i].target = groupController.PlayableLemmings[i - 1].transform;
			targetGroup.m_Targets[i].radius = 5;
			targetGroup.m_Targets[i].weight = 0;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (groupController.IsGroupSelected && FocusChange)
		{
			FocusGroup();
		}
		else if(!groupController.IsGroupSelected && FocusChange)
		{
			FocusActive();
		}
	}

	void FocusGroup()
	{
		targetGroup.m_Targets[0].weight += moveTime / 10;
		targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight -= moveTime / 10;

		if (targetGroup.m_Targets[0].weight >= 1 && targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight <= 0)
		{
			FocusChange = false;
			targetGroup.m_Targets[0].weight = 1;
			targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight = 0;
		}
	}

	void FocusActive()
	{
		targetGroup.m_Targets[0].weight -= moveTime / 10;
		targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight += moveTime / 10;

		if (targetGroup.m_Targets[0].weight <= 1 && targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight >= 0)
		{
			FocusChange = false;
			targetGroup.m_Targets[0].weight = 0;
			targetGroup.m_Targets[groupController.ActiveLemmingIndex + 1].weight = 1;
		}
	}
}
