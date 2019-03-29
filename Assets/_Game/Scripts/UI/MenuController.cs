using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;
	[SerializeField] private GameObject optionUI;
	[SerializeField] private GameObject creditUI;


	public TextMeshProUGUI newGameText;
	public TextMeshProUGUI continueGameText;
	public TextMeshProUGUI optionText;
	public TextMeshProUGUI creditText;
	public TextMeshProUGUI returnText;


	private GameManager gm;
	private GameObject menuUI;

    private void Start()
    {
	    if (GameManager.instance == null)
	    {
		    gm = Instantiate(gameManager);
	    }
	    else
	    {
		    gm = GameManager.instance;
	    }

	    if (gm.level == 1)
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
		newGameText.color = new Color32(255, 255, 255, 255);
		newGameText.text = "New Game";

		gm.ResetProgress();
	    SceneManager.LoadScene("Level 1", LoadSceneMode.Single);

		//gm.EnableIngameUI(true);
    }

    public void ContinueGame_Click()
    {
		continueGameText.text = "Continue";
		
		SceneManager.LoadScene("Level " + gm.level, LoadSceneMode.Single);

//	    gm.EnableIngameUI(true);
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
		returnText.text = "Return";
		
		newGameText.text = "Neues Spiel";
		continueGameText.text = "Fortsetzen";
		optionText.text = "Optionen/Steuerung";
		creditText.text = "Credits";


		optionUI.SetActive(false);
	    creditUI.SetActive(false);;
	    menuUI.SetActive(true);
    }

    public void OnButtonPressed(int index)
    {
	    TextMeshProUGUI text = transform.GetChild(index).GetChild(0).GetComponent<TextMeshProUGUI>();
	    text.color = Color.white;
    }
}
