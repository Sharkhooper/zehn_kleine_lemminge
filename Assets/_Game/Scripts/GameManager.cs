using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[SerializeField] private TextMeshProUGUI groupText;

	public int level = 1;
	public int maxLemminge = 10;
	public int currentLemmings = 7;
	public bool existSingleLemming = false;

	public Dictionary<string, bool> UnlockedAbilities { get; private set; }
	public bool SuperJumpActivated { get; set; }
	public int MaxLevelLemming { get; set; }

	public Button actionButton;
	public Button GroupButton;
	public TextMeshProUGUI currentLemmingText;
	private InteractebaleSwitch interactebaleSwitch;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};

		MaxLevelLemming = 10;
	}

	private void Start()
	{
		actionButton.enabled = false;
	}

	//because when you start a NewGame the UI would be active before you are in a Level Scene!
	public void Update()
	{
		if (SceneManager.GetActiveScene().name.Equals("Level " + level))
			EnableIngameUI(true);
		else
			EnableIngameUI(false);
	}

	public void LoadNextLevel()
	{
		if (level < 4)
			level++;
		switch (level)
		{
			case 1: maxLemminge = 10;
				break;
			case 2: maxLemminge = 7;
				break;
			case 3: maxLemminge = 6;
				break;
			case 4: maxLemminge = 6;
				break;
			default:
				break;
		}
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
	}


	public void EnableIngameUI(bool enable)
	{
		transform.GetChild(0).gameObject.SetActive(enable);
	}

	public void ActionButtonEnable(bool b, InteractebaleSwitch switchScript)
	{
		interactebaleSwitch = switchScript;
		actionButton.enabled = b;
	}

	public void UnlockAbility(string ability)
	{
		UnlockedAbilities[ability] = true;
	}

	public void LockAbility(string ability)
	{
		UnlockedAbilities[ability] = false;
	}

	public void Back_Click()
	{
		//backText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
		EnableIngameUI(false);
	}

	public void MenuButton_Click()
	{
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
		EnableIngameUI(false);
	}


	public void ActionButton_Click()
	{
		Debug.Log("Button clicked");
		interactebaleSwitch.ActionButtonPressed();
	}

	public void GroupButton_Click()
	{
		if (existSingleLemming)
		{
			if (groupText.text.Equals("Group"))
			{
				Debug.Log("Single");
				groupText.text = "Single";
			}
			else
			{
				Debug.Log("Group");
				groupText.text = "Group";
			}
		}
	}

	public void ResetProgress()
	{
		level = 1;
		maxLemminge = 10;
		currentLemmings = 10;
	}

	public void GameOver()
	{
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		currentLemmings = maxLemminge;
	}
}
