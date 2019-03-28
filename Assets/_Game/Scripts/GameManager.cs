using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public int level = 1;
	public int leben = 3;
	public int lemminge = 10;

	public TextMeshProUGUI newGameText;
	public TextMeshProUGUI continueGameText;
	public TextMeshProUGUI optionText;
	public TextMeshProUGUI creditText;
	public TextMeshProUGUI backText;


	private void Start()
	{
		if (continueGameText != null)
		{
			if (level == 1) continueGameText.color = new Color32(142, 146, 183, 100);
		}
	}



	public Dictionary<string, bool> UnlockedAbilities { get; private set; }
	public bool SuperJumpActivated { get; set; }

	private void Awake()
	{
		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};
	}

	public void UnlockAbility(string ability)
	{
		UnlockedAbilities[ability] = true;
	}

	public void LockAbility(string ability)
	{
		UnlockedAbilities[ability] = false;
	}

	public void newGameButton_Click()
	{
		newGameText.color = new Color32(255, 255, 255, 255);

		newGameText.text = "New Game";
		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		level = 1;
		leben = 3;
		lemminge = 10;
	}

	public void continueGame_Click()
	{

		if (level != 1)
		{
			SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
			continueGameText.text = "Continue";
			continueGameText.color = new Color32(255, 255, 255, 255);
		}//korrektes leben und lemming Anzahl laden
		}

		public void option_Click()
	{
		optionText.text = "Options";
		optionText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("OptionScene", LoadSceneMode.Single);
	}

	public void credit_Click()
	{
		creditText.text = "Menschen!";
		creditText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("CreditScene", LoadSceneMode.Single);
	}

	public void back_Click()
	{
		backText.color = new Color32(255, 255, 255, 255);
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
	}

	public void menuButton_Click()
	{
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
	}





	public void gameOver()
	{
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		leben = 3;
		lemminge = 10;
	}
}
