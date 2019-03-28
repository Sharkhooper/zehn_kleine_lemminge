using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public int level = 1;
	public int leben = 3;
	public int lemminge = 10;
	public bool existSingleLemming = false;

	public TextMeshProUGUI newGameText;
	public TextMeshProUGUI continueGameText;
	public TextMeshProUGUI optionText;
	public TextMeshProUGUI creditText;
	public TextMeshProUGUI backText;
	public TextMeshProUGUI groupText;

	public Button actionButton;
	private InteractebaleSwitch interactebaleSwitch;
	


	private void Start()
	{
		if (continueGameText != null)
		{
			if (level == 1) continueGameText.color = new Color32(142, 146, 183, 100);
		}

		actionButton.enabled = false;
	}



	public Dictionary<string, bool> UnlockedAbilities { get; private set; }
	public bool SuperJumpActivated { get; set; }

	private void Awake()
	{
		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};
	}

	public void ActionButtonEnable(bool b,InteractebaleSwitch switchScript)
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

	public void NewGameButton_Click()
	{
		newGameText.color = new Color32(255, 255, 255, 255);

		newGameText.text = "New Game";
		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		level = 1;
		leben = 3;
		lemminge = 10;
	}

	public void ContinueGame_Click()
	{

		if (level != 1)
		{
			SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
			continueGameText.text = "Continue";
			continueGameText.color = new Color32(255, 255, 255, 255);
		}//korrektes leben und lemming Anzahl laden
		}

		public void Option_Click()
	{
		optionText.text = "Options";
		optionText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("OptionScene", LoadSceneMode.Single);
	}

	public void Credit_Click()
	{
		creditText.text = "Menschen!";
		creditText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("CreditScene", LoadSceneMode.Single);
	}

	public void Back_Click()
	{
		backText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
	}

	public void MenuButton_Click()
	{
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
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


	public void GameOver()
	{
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		leben = 3;
		lemminge = 10;
	}
}
