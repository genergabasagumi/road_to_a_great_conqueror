using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class kingHp : MonoBehaviour {

	public Transform enemyFollow;
	public RectTransform enemyhpslider;
	public RectTransform canvasrect;

	public GameObject player;


	// Use this for initialization


	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		canvasrect = (RectTransform)GameObject.Find ("Canvas").transform;

		enemyhpslider = (RectTransform)player.GetComponent<playerAnimation2> ().enemyslider.transform;
		enemyFollow = this.transform;
		enemyhpslider.gameObject.SetActive (true);
	}

	
	// Update is called once per frame
	void Update () {

		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint (Camera.main, enemyFollow.position);
		if (this.GetComponent<Enemy> ().Right) {
			screenPoint.x += Screen.width/20;
		}
		else
			screenPoint.x -= Screen.width/20;
		screenPoint.y += Screen.height / 4;
		enemyhpslider.anchoredPosition = screenPoint - canvasrect.sizeDelta / 2f;
		enemyhpslider.gameObject.GetComponent<Slider>().value = this.gameObject.GetComponent<Enemy> ().MyHp;



	}
}
