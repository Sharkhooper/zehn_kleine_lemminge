using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public static MenuController instance = null;

	[SerializeField] private GameManager gameManager;
	[SerializeField] private GameObject optionUI;
	[SerializeField] private GameObject creditUI;


	public TextMeshProUGUI newGameText;
	public TextMeshProUGUI continueGameText;
	public TextMeshProUGUI optionText;
	public TextMeshProUGUI creditText;
	public TextMeshProUGUI returnText;
	
	private GameObject menuUI;

	/*void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}
	*/
	private void Start()
    {
		gameManager = gameManager.getInstance();

	    if (gameManager.level == 1 && gameManager.getInstance().groupPosition==Vector3.zero)
	    {
		    Button continueButton = transform.GetChild(1).GetComponent<Button>();
		    continueButton.enabled = false;
		    continueButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.grey;
	    }

	    menuUI = gameObject;
	    optionUI.SetActive(false);
	    creditUI.SetActive(false);

	  //  gm.EnableIngameUI(false);
    }


	public void NewGameButton_Click()
	{
		//menuUI.SetActive(false);
		newGameText.color = new Color32(255, 255, 255, 255);
		newGameText.text = "New Game";

		gameManager.ResetProgress();
		gameManager.currentLemmingText.text = "Leben: " + gameManager.currentLemmings;

		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		//gm.EnableIngameUI(true);
    }

    public void ContinueGame_Click()
    {
		//menuUI.SetActive(false);
		SceneManager.sceneLoaded += gameManager.OnLevelLoaded;
		SceneManager.LoadScene("Level " + gameManager.level, LoadSceneMode.Single);
		gameManager.currentLemmingText.text = "Leben: " + gameManager.currentLemmings;
		gameManager.playMusic.enabled = true;
		gameManager.playMusic.Play();

		if (gameManager.groupPosition != Vector3.zero)
			gameManager.getInstance().BackToGame_Click();
				
		//	    gm.EnableIngameUI(true);
	}

	public void ChangeContinueText(string continueGame)
	{
		continueGameText.text = continueGame;
		continueGameText.color = Color.white;
	}


	public void Option_Click()
    {
		optionText.text = "Options";
		

		optionUI.SetActive(true);
		menuUI.SetActive(false);
    }

    public void Credit_Click()
    {
		creditText.text = "Menschen!";
		
		creditUI.SetActive(true);
	    menuUI.SetActive(false);
    }

    public void Return_MainMenu()
    {
	//	returnText.text = "Return";
		
		newGameText.text = "Neues Spiel";
		continueGameText.text = "Fortsetzen";
		optionText.text = "Optionen/Steuerung";
		creditText.text = "Credits";


		optionUI.SetActive(false);
	    creditUI.SetActive(false);;
	    menuUI.SetActive(true);
    }


	public void Exit_Click()
	{
		Debug.Log("Exit");
		Application.Quit();
	}
    public void OnButtonPressed(int index)
    {
	    TextMeshProUGUI text = transform.GetChild(index).GetChild(0).GetComponent<TextMeshProUGUI>();
	    text.color = Color.white;
    }
}
