
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPC : MonoBehaviour
{
	[SerializeField] public GroupController groupController;
	private GameManager gameManager;
	private Rigidbody2D rb;
	private bool nonZeroHorizontal;
	private float ActionCoolDown { get; set; }


	void Awake()
	{

	}

    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
		ActionCoolDown = 0;
		gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//Debug.Log(ActionCoolDown);
		if (ActionCoolDown > 0)
		{
			ActionCoolDown -= 0.1f;
		}
		float horizontal = Input.GetAxis("Horizontal");

		bool groupAction = Input.GetButton("Group Action");
		if (groupAction && ActionCoolDown <= 0)
		{
			groupController.ActivateGroup(true);
			ActionCoolDown = 1.0f;
		}

		/*
		if (horizontal != 0)
		{
			nonZeroHorizontal = true;
			groupController.MoveHorizontal(horizontal);
		}
		else if (nonZeroHorizontal)
		{
			groupController.MoveHorizontal(horizontal);
			nonZeroHorizontal = false;
		}
		*/
		if (horizontal > 0.1f || horizontal < -0.1f)
		{
			groupController.MoveHorizontal(horizontal);
		}
		else
		{
			groupController.MoveHorizontal(0f);
		}

		float vertical = Input.GetAxis("Vertical");
		if(vertical > 0)
		{
			groupController.Jump();
		}

		if (Input.GetButton("Reset")) {
			if (!SceneManager.GetActiveScene().name.Equals("GameOver"))
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
			else
				SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
		}

		if (Input.GetButton("Action"))
		{
			gameManager.getInstance().ActionButton_Click();
		}

		if (Input.GetButton("Bomb"))
		{
			gameManager.getInstance().useBomb();
		}

		if (Input.GetButton("Fire"))
		{
			gameManager.getInstance().useFire();
		}

		if(Input.GetButton("Super Jump"))
		{
			gameManager.getInstance().SuperJumpActivated = !gameManager.getInstance().SuperJumpActivated;
		}

		if (Input.GetButton("Cancel"))
		{
			gameManager.MenuButton_Click();
		}

		if(Input.GetButton("Jump to Group"))
		{
			gameManager.GroupButton_Click();
		}

	}
}
