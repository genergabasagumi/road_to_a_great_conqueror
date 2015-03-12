using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Button startButton;

	public void ClickTest()
	{
		Application.LoadLevel (1);
	}

	public void ExitClick()
	{
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
