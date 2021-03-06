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
	[SerializeField] public AudioSource playMusic;

	public int level = 1;
	public int currentLemmings = 7;
	public bool existSingleLemming = false;
	public int maxLemming = 7;

	public Dictionary<string, bool> UnlockedAbilities { get; private set; }
	public bool SuperJumpActivated { get; set; }
	public int MaxLevelLemming { get; set; }


	public Button actionButton;
	public Button groupButton;
	public Button gameOverContinue;
	public Button gameOverMainMenue;

	public TextMeshProUGUI currentLemmingText;
	private IInteractible interactebaleSwitch;
	private GroupController groupControllerUseable;

	[SerializeField] private Button fireButton;
	[SerializeField] private Button bombButton;
	[SerializeField] private Button jumpButton;

	public Vector3 groupPosition;
	public Vector3 singlePosition;

	//Awake is always called before any Start functions
	void Awake()
	{
		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};

		MaxLevelLemming = maxLemming;
		if (instance == null)
		{
			instance = this;
			if (PlayerPrefs.HasKey("level"))
				level = PlayerPrefs.GetInt("level");
			if (PlayerPrefs.HasKey("currentLemmings"))
				currentLemmings = PlayerPrefs.GetInt("currentLemmings");
			if (PlayerPrefs.HasKey("maxLemminge"))
				MaxLevelLemming = PlayerPrefs.GetInt("maxLemminge");
			if (PlayerPrefs.HasKey("FireP"))
				UnlockedAbilities["Fire"] = IntToBoolForDic(PlayerPrefs.GetInt("FireP"));
			if (PlayerPrefs.HasKey("JumpP"))
				UnlockedAbilities["SuperJump"] = IntToBoolForDic(PlayerPrefs.GetInt("JumpP"));
			if (PlayerPrefs.HasKey("BombP"))
				UnlockedAbilities["Power"] = IntToBoolForDic(PlayerPrefs.GetInt("BombP"));

			if (level == 1)
			{
				FireButtonDisable();
			}
			else if (level==2)
			{
				BombButtonDisable();
			}
		}
		else if (instance != this)
		{
			if (PlayerPrefs.HasKey("level"))
				instance.level = PlayerPrefs.GetInt("level");
			if (PlayerPrefs.HasKey("currentLemmings"))
				instance.currentLemmings = PlayerPrefs.GetInt("currentLemmings");
			if (PlayerPrefs.HasKey("maxLemminge"))
				instance.MaxLevelLemming = PlayerPrefs.GetInt("maxLemminge");
			if (PlayerPrefs.HasKey("FireP"))
				instance.UnlockedAbilities["Fire"] = IntToBoolForDic(PlayerPrefs.GetInt("FireP"));
			if (PlayerPrefs.HasKey("JumpP"))
				instance.UnlockedAbilities["SuperJump"] = IntToBoolForDic(PlayerPrefs.GetInt("JumpP"));
			if (PlayerPrefs.HasKey("BombP"))
				instance.UnlockedAbilities["Power"] = IntToBoolForDic(PlayerPrefs.GetInt("BombP"));


			if (instance.level == 1)
			{
			//Debug.Log(level);
			instance.FireButtonDisable();
			
			}
			else if (instance.level==2)
			{
				instance.BombButtonDisable();
			}
			instance.gameOverContinue = gameOverContinue;
			instance.gameOverMainMenue = gameOverMainMenue;

			Destroy(gameObject);
		}
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		if (SceneManager.GetActiveScene().name.Contains("Level"))
			playMusic.Play();

		maxLemming = MaxLevelLemming;
	}

	private void Start()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Screen.SetResolution(800, 480, true);
		}

		groupButton.enabled = false;
		actionButton.enabled = false;
		fireButton.enabled= UnlockedAbilities["Fire"];
		bombButton.enabled = UnlockedAbilities["Power"];
		jumpButton.enabled = UnlockedAbilities["SuperJump"];
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
				getInstance().MaxLevelLemming = 7;
				break;
			case 2:
				getInstance().MaxLevelLemming = 4;
				break;
			case 3:
				getInstance().MaxLevelLemming = 3;
				break;
			case 4:
				SceneManager.LoadScene("Ending", LoadSceneMode.Single);
				ResetProgress();
				break;
			default:
				break;
		}

		getInstance().maxLemming = getInstance().MaxLevelLemming;

		//speichern überprüfen auf Fehler??
		PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetInt("currentLemmings", currentLemmings);
		PlayerPrefs.SetInt("maxLemminge", MaxLevelLemming);
		PlayerPrefs.SetInt("FireP",BoolToIntFromDic(UnlockedAbilities["Fire"]));
		PlayerPrefs.SetInt("BombP",BoolToIntFromDic(UnlockedAbilities["Power"]));
		PlayerPrefs.SetInt("JumpP",BoolToIntFromDic(UnlockedAbilities["SuperJump"]));
		PlayerPrefs.Save();

		currentLemmingText.text = "Leben: " + currentLemmings;
		if (level < 4)
		{
			SceneManager.sceneLoaded += OnLevelLoaded;
			SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		}

		SceneManager.sceneLoaded += OnLevelLoaded;
	}

	//Entfernen zu vieler Lemminge.
	public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		GroupController groupController = FindObjectOfType<GroupController>();
		if (SceneManager.GetActiveScene().name.Equals("Level " + level))
		{
			if (currentLemmings > MaxLevelLemming)
			{
				currentLemmings = 7;
				while (currentLemmings > MaxLevelLemming)
					groupController.RemoveLemmingFromGroup();
			}
			else
			{
				int lastLemminglevel = currentLemmings;
				currentLemmings = 7;
				while (lastLemminglevel >= currentLemmings)
					groupController.RemoveLemmingFromGroup();
				
			}


		}

		SceneManager.sceneLoaded -= OnLevelLoaded;
	}

	public void EnableIngameUI(bool enable)
	{
		transform.GetChild(0).gameObject.SetActive(enable);
	}

	public void ActionButtonEnable(bool b, IInteractible switchScript)
	{
		interactebaleSwitch = switchScript;
		if(b == false)
		{
			actionButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}
		else
		{
			actionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Aktion";
		}
		actionButton.enabled = b;
	}

	public void FireButtonEnabled()
	{
		fireButton.enabled = true;
		fireButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fire";
	}

	public void FireButtonDisable()
	{
		fireButton.enabled = false;
		fireButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
	}

	public void BombButtonEnabled()
	{
		bombButton.enabled = true;
		bombButton.GetComponentInChildren<TextMeshProUGUI>().text = "Bomb";
	}
	public void BombButtonDisable()
	{
		bombButton.enabled = false;
		bombButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
	}

	public void JumpButtonEnabled()
	{
		jumpButton.enabled = true;
		jumpButton.GetComponentInChildren<TextMeshProUGUI>().text = "Jump";
	}
	
	public void JumpButtonDisable()
	{
		jumpButton.enabled = false;
		jumpButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
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

	public void CreditScene()
	{
		playMusic.Stop();
		EnableIngameUI(false);

		//SceneManager.sceneLoaded += MenueLoad;
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);

	}

	void MenueLoad(Scene scene, LoadSceneMode mode)
	{

		if (SceneManager.GetActiveScene().name.Equals("TitleMenu"))
		{
			Debug.Log("LoadCredit");
			MenuController mC = FindObjectOfType<MenuController>();
			mC.Credit_Click();

		}
		SceneManager.sceneLoaded -= MenueLoad;
	}

	public void MenuButton_Click()
	{

		//FadeOut(playMusic, 0.5f);

		//aktuellePosition der Zeit
		//playMusic.time;
		playMusic.Stop();


		GroupController groupController = FindObjectOfType<GroupController>();
		if (existSingleLemming)
		{
			instance.singlePosition = groupController.ActiveLemming.transform.position;
			Debug.Log("SinglePosition beim Speichern: " + instance.singlePosition.x + " - " + instance.singlePosition.y);
		}
		if (groupController != null)
		{
			instance.groupPosition = groupController.transform.position;
			Debug.Log("GroupPosition beim Speichern: " + instance.groupPosition.x + " - " + instance.groupPosition.y);
			currentLemmings = groupController.ActiveLemmingIndex;
		}
		EnableIngameUI(false);
		SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);


	}

	public void BackToGame_Click()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		GroupController groupController = FindObjectOfType<GroupController>();
		if (SceneManager.GetActiveScene().name.Equals("Level " + level))
		{
			groupController.transform.position = groupPosition;

			if (singlePosition != Vector3.zero)
			{
				currentLemmings = instance.currentLemmings;
				Debug.Log(currentLemmings + " - " + groupController.ActiveLemmingIndex);
				while (groupController.ActiveLemmingIndex > currentLemmings)
				{
					groupController.RemoveLemmingFromGroup();
				}
				groupController.SetActiveLemming(currentLemmings);
				groupController.LemmingExitGroup(0);
				groupController.ActiveLemming.transform.position = singlePosition;

			}

		
			instance.singlePosition = Vector3.zero;
			instance.groupPosition = Vector3.zero;
		}

		//Geht wohl doch noch nicht ganz
		if (SceneManager.GetActiveScene().name.Equals("GameOver"))
		{
			gameOverContinue.onClick.AddListener(ContinueGame_Click);
			gameOverMainMenue.onClick.AddListener(MenuButton_Click);
		}
		
		SceneManager.sceneLoaded -= OnSceneLoaded;
}

	public GameManager getInstance()
	{
		if (instance == null)
			instance = this;
		return instance;
	}

	public void ActionButton_Click()
	{
		//Debug.Log("Button clicked");
		interactebaleSwitch.ActionButtonPressed();
	}

	public void GroupButton_Click()
	{
		if (existSingleLemming)
		{
			GroupController groupController = FindObjectOfType<GroupController>();
			if (groupText.text.Equals("Single"))
			{
				Debug.Log("Single");

				groupController.IsGroupSelected = false;
				groupController.CamController.FocusChange = true;

				groupText.text = "Group";
			}
			else
			{
				Debug.Log("Group");

				groupController.IsGroupSelected = true;
				groupController.CamController.FocusChange = true;

				groupText.text = "Single";
			}
		}
	}

	public void ResetProgress()
	{
		instance.currentLemmings = 7;
		instance.MaxLevelLemming = 7;
		instance.level = 1;
		instance.maxLemming = 7;
		PlayerPrefs.SetInt("level", instance.level = 1);
		PlayerPrefs.SetInt("currentLemmings", instance.currentLemmings =7);
		PlayerPrefs.SetInt("maxLemminge", instance.MaxLevelLemming = 7);
		PlayerPrefs.SetInt("FireP",0);
		PlayerPrefs.SetInt("BombP",0);
		PlayerPrefs.SetInt("JumpP",0);

		FireButtonDisable();
		BombButtonDisable();
		JumpButtonDisable();
		PlayerPrefs.Save();

		playMusic.Play();
	}

	public void GameOver()
	{
		EnableIngameUI(false);
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
		SceneManager.sceneLoaded += OnSceneLoaded;
		currentLemmings = MaxLevelLemming;
	}

	public void ContinueGame_Click()
	{
		//menuUI.SetActive(false);
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		currentLemmingText.text = "Leben: " + MaxLevelLemming;
		//playMusic.enabled = true;
		//playMusic.Play();

		//	    gm.EnableIngameUI(true);
	}


	public void useFire()
	{
		if (existSingleLemming)
		{


			if (groupControllerUseable == null)
			{
				groupControllerUseable = FindObjectOfType<GroupController>();
				
			}
			groupControllerUseable.useFire();
		}
	}

	public void useBomb()
	{
		if (existSingleLemming)
		{

			if (groupControllerUseable == null)
			{
				groupControllerUseable = FindObjectOfType<GroupController>();
				
			}

			groupControllerUseable.useBomb();
		}
	}


	public int BoolToIntFromDic(bool a)
	{
		if (a)
		{
			return 1;
		}
		else
		{
			return 0;
		}
		
	}

	public bool IntToBoolForDic(int a)
	{
		if (a==1)
		{
			return true;

		}
		else
		{
			return false;
		}
	}
}
