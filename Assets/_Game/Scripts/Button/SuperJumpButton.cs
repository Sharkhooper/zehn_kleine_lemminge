using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SuperJumpButton : MonoBehaviour
{
	public SuperJumpButton button;
	private GameManager manager;
	private bool active;

	public Button jumpButton;

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
		Debug.Log("Button On Action");
		active = !active;
		if (active)
		{
			ColorBlock cb = jumpButton.colors;
			cb.normalColor = Color.yellow;
			cb.highlightedColor = Color.yellow;
			jumpButton.colors = cb;
		}
		else
		{
			ColorBlock cb = jumpButton.colors;
			cb.normalColor = Color.white;
			cb.highlightedColor = Color.white;
			jumpButton.colors = cb;
		}
		manager.SuperJumpActivated = active;
		Debug.Log("Jump Activ: " + manager.SuperJumpActivated);
	}
}
