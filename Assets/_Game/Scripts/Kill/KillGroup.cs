using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGroup : MonoBehaviour, IKillTarget
{

	// Start is called before the first frame update
	void Start()
    {
        
    }


	public void Die(GameObject other)
	{
		Debug.Log("Group gekillt");
	}
}
