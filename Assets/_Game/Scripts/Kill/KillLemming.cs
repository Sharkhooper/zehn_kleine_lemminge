using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLemming : MonoBehaviour, IKillTarget
{
	private bool life;

	// Start is called before the first frame update
	void Start()
    {
		life = true;
        
    }

	public void Die()
	{
		life = false;
		Debug.Log("Lemming gekillt");
	}
}
