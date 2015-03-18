using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inGameMenu : MonoBehaviour {
	public GameObject optionPanel;
	public bool optionTrigger;


	public GameObject gemPanel;
	public bool gemTrigger;


	// Use this for initialization
	bool isPause;
	public void optionShow()
	{
		optionTrigger = !optionTrigger;
		isPause = !isPause;
		optionPanel.SetActive (optionTrigger);
	
	}

	public void gemPanelShow()
	{
		gemTrigger = !gemTrigger;
		isPause = !isPause;
		gemPanel.SetActive (gemPanel);
	}

	public void restartLevel()
	{
		Application.LoadLevel (1);
	}

	public void applicationExit()
	{
		Application.Quit ();
	}

	public void goMainMenu()
	{
		Application.LoadLevel (0);
	}

	public void BuyItem(string item)
	{
		StoreManager.instance.BuyItem (item);
	}

	void Start () {
		optionTrigger = false;
		isPause = false;
		gemTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (isPause == true || optionTrigger || gemTrigger)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;

		if (!gemTrigger) {
			gemPanel.SetActive(false);
		}
	}
}
