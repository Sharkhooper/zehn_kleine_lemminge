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

	    gm.EnableIngameUI(false);
    }

    public void NewGameButton_Click()
    {
	    gm.ResetProgress();
	    SceneManager.LoadScene("Level 1", LoadSceneMode.Single);

	    gm.EnableIngameUI(true);
    }

    public void ContinueGame_Click()
    {
	    SceneManager.LoadScene("Level " + gm.level, LoadSceneMode.Single);

	    gm.EnableIngameUI(true);
    }

    public void Option_Click()
    {
		optionUI.SetActive(true);
		menuUI.SetActive(false);
    }

    public void Credit_Click()
    {
	    creditUI.SetActive(true);
	    menuUI.SetActive(false);
    }

    public void Return_MainMenu()
    {
	    optionUI.SetActive(false);
	    creditUI.SetActive(false);;
	    menuUI.SetActive(true);
    }

    public void OnButtonPressed(int index)
    {
	    TextMeshProUGUI text = transform.GetChild(index).GetComponent<TextMeshProUGUI>();
	    text.color = Color.white;
    }
}
