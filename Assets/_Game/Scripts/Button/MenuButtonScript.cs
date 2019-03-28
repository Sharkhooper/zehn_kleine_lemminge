using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
	public int level = 1;
	public int leben = 3;
	public int lemminge = 10;

	public Text newGameText;
	public Text continueGameText;
	public Text optionText;
	public Text creditText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void newGameButton_Click()
	{
		//newGameText.text = "新しい遊";
		newGameText.color = Color.white;
		SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
		level = 1;
		leben = 3;
		lemminge = 10;
	}

	public void continueGame_Click()
	{
		continueGameText.color = Color.white;
		SceneManager.LoadScene("Level " + level, LoadSceneMode.Single);
		//korrektes leben und lemming Anzahl laden
	}

	public void option_Click()
	{
		optionText.color = Color.white;
		SceneManager.LoadScene("OptionScene", LoadSceneMode.Single);
	}

	public void credit_Click()
	{
		creditText.color = Color.white;
		SceneManager.LoadScene("CreditScene", LoadSceneMode.Single);
	}

	public void back_Click()
	{

		SceneManager.LoadScene("MeinScene", LoadSceneMode.Single);
			}
}
