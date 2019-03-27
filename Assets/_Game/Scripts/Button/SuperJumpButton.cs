using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpButton : MonoBehaviour
{
	public SuperJumpButton button;
	private GameManager manager;
	private bool active;

	// Start is called before the first frame update
	void Start()
	{
		manager = FindObjectOfType<GameManager>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void EnableBuntton()
	{
		button.EnableBuntton();
	}

	public void DisableBuntton()
	{
		button.DisableBuntton();
	}

	public void OnAction()
	{
		active = !active;

		Debug.Log("Super Jump: " + active);
		manager.SuperJumpActivated = active;
	}
}
