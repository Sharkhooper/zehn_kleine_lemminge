using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _Game.Scripts.GameObjects;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[SerializeField] public TextMeshProUGUI groupText;
	[SerializeField] private AudioSource playMusic;

	public int level = 1;
	public int currentLemmings = 7;
	public bool existSingleLemming = false;

	public Dictionary<string, bool> UnlockedAbilities { get; private set; }
	public bool SuperJumpActivated { get; set; }
	public int MaxLevelLemming { get; set; }

	public Button actionButton;
	public Button groupButton;
	public TextMeshProUGUI currentLemmingText;
	private IInteractible interactebaleSwitch;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (PlayerPrefs.HasKey("level"))
			level = PlayerPrefs.GetInt("level");
		if (PlayerPrefs.HasKey("currentLemmings"))
			currentLemmings = PlayerPrefs.GetInt("currentLemmings");
		if (PlayerPrefs.HasKey("maxLemminge"))
			MaxLevelLemming = PlayerPrefs.GetInt("maxLemminge");

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};

		MaxLevelLemming = 7;
	}

	private void Start()
	{
		groupButton.enabled = false;
		actionButton.enabled = false;
	}

	//because when you start a NewGame the UI would be active before you are in a Level Scene!
	public void Update()
	{
		if (SceneManager.GetActiveScene().name.Contains("Level"))
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
			case 1:
				MaxLevelLemming = 7;
				break;
			case 2:
				MaxLevelLemming = 4;
				break;
			case 3:
				MaxLevelLemming = 3;
				break;
			case 4:
				MaxLevelLemming = 3;
				break;
			default:
				break;
		}

		PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetInt("currentLemmings", currentLemmings);
		PlayerPrefs.SetInt("maxLemminge", MaxLevelLemming);
		PlayerPrefs.Save();

		currentLemmingText.text = "Leben: " + currentLemmings;
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
	}


	public void EnableIngameUI(bool enable)
	{
		transform.GetChild(0).gameObject.SetActive(enable);
	}

	public void ActionButtonEnable(bool b, IInteractible switchScript)
	{
		interactebaleSwitch = switchScript;
		actionButton.enabled = b;
		if(b == false)
		{

		}
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

	//public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	//{
	//	float startVolume = audioSource.volume;

	//	while (audioSource.volume > 0)
	//	{
	//		audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

	//		yield return null;
	//	}

	//	audioSource.Stop();
	//	audioSource.volume = startVolume;
	//}

	public void MenuButton_Click()
	{

		//FadeOut(playMusic, 0.5f);
		playMusic.Stop();


		/*GroupController groupController = FindObjectOfType<GroupController>();
		MenuController menu = FindObjectOfType<MenuController>();
		menu.ChangeContinueText("Zurück zum Spiel");

		if (existSingleLemming)
			menu.singlePosition = groupController.ActiveLemming.transform.position;
		menu.groupPosition = groupController.transform.position;
		*/
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);

		/*Button continueButton = menu.transform.GetChild(1).GetComponent<Button>();
		continueButton.enabled = true;
		*/
		EnableIngameUI(false);
	}
	/*
	public void BackToGame_Click(Vector3 gp, Vector3 sp)
	{
		Debug.Log("Back to Game");

		GroupController groupController = FindObjectOfType<GroupController>();
		if (sp != Vector3.zero)
			groupController.ActiveLemming.transform.position = sp;
		groupController.transform.position = gp;

		MenuController menu = FindObjectOfType<MenuController>();
		menu.ChangeContinueText("Zurück zum Spiel");
		menu.singlePosition = Vector3.zero;
		menu.groupPosition = Vector3.zero;
	}
	*/

	public void ActionButton_Click()
	{
		Debug.Log("Button clicked");
		interactebaleSwitch.ActionButtonPressed();
	}

	public void GroupButton_Click()
	{
		if (existSingleLemming)
		{
			GroupController groupController = FindObjectOfType<GroupController>();
			if (groupText.text.Equals("Group"))
			{
				Debug.Log("Single");

				groupController.IsGroupSelected = false;
				groupController.CamController.FocusChange = true;

				groupText.text = "Single";
			}
			else
			{
				Debug.Log("Group");

				groupController.IsGroupSelected = true;
				groupController.CamController.FocusChange = true;

				groupText.text = "Group";
			}
		}
	}

	public void ResetProgress()
	{
		
		PlayerPrefs.SetInt("level", level=1);
		PlayerPrefs.SetInt("currentLemmings", currentLemmings=7);
		PlayerPrefs.SetInt("maxLemminge", MaxLevelLemming = 7);
		PlayerPrefs.Save();

	}

	public void GameOver()
	{
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		currentLemmings = MaxLevelLemming;
	}
}
