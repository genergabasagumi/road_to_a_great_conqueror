using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inGameMenu : MonoBehaviour {
	public GameObject optionPanel;
	public bool optionTrigger;
	// Use this for initialization
	bool isPause;
	public void optionShow()
	{
		optionTrigger = !optionTrigger;
		isPause = !isPause;
		optionPanel.SetActive (optionTrigger);
	
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

	void Start () {
		optionTrigger = false;
		isPause = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (isPause == true)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
	}
}
