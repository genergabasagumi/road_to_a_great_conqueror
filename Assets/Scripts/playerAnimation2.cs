﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerAnimation2 : MonoBehaviour {

	public AudioClip playerDeathAudio;
	public AudioClip playerAttackAudio;
	public AudioClip playerCritAudio;
	public AudioClip HighScoreAudio;



	public bool Dead = false;

	Animator animator;
	bool _isPlaying_walk = false;
	
	const int STATE_DEAD = 0;
	const int STATE_ATTACK = 1;
	const int STATE_CRIT = 2;

	//int _currentAnimationState = STATE_IDLE;
	
	string _currentDirection = "left";
	
	public float minSwipeDistY;
	public float minSwipeDistX;
	
	private Vector2 startPos;
	public GameObject Attack;
	//public float timeAttack;
	//private float count;
	public float MaxHP;
	
	//public Text typeText;

	public float Crit;
	public int random;
	public float MyAttackDamage;
	public float currentDamage;

	public GameObject effectEnemyDefeat;
	public GameObject effectSlash;
	public GameObject effectCrit;
	public bool Right;
	private Vector3 SpawnPart;

	public float SlashDelay;
	public float CritDelay;
	private float tempDelay;
	private float count;
	public bool Colliding;

	public int killCount;

	//UI
	public Text killText;
	public GameObject alertObj;
	public Slider hpBar;
	public GameObject mainMenu;
	public Text deathText;
	public Text highScoreText;
	
	public static int highScore;
	public GameObject enemyslider;
	public int hpMaxTemp;


	public class characterType
	{
		public int HP;
		public int Attack;
		public int Defence;
		public int Type;
		public int currentHp;
		
		public characterType(int hp, int attack, int defence, int type)
		{
			HP = hp;
			Attack = attack;
			Defence = defence;
			Type = type;
		}
		
		public void changeType(string direction)
		{
			if (direction == "up")
				if (Type != 3)
					Type ++;
			else
				Type = 1;
			
			if (direction == "down")
				if (Type != 1)
					Type --;
			else
				Type = 3;
		}
	}
	
	public characterType playerType;

	void Awake()
	{
		highScore = PlayerPrefs.GetInt("High Score");
		highScoreText.text = highScore.ToString();
		alertObj.SetActive (false);
	}
	void Start()
	{
		//HeyzapAds.start("7b5c65f011a2c2abdf907fc31bcb8ef1", HeyzapAds.FLAG_NO_OPTIONS);
		HeyzapAds.start("652394e91a2dae688f5afff67ef9ec5e", HeyzapAds.FLAG_NO_OPTIONS);
		HZInterstitialAd.show();
		hpMaxTemp = (int)MaxHP;
		hpBar.maxValue = hpMaxTemp;
		hpBar.value = hpMaxTemp;

		killCount = 0;

		animator = this.GetComponent<Animator>();
		//damaged = false;
		//playerType = new characterType(10, 5, 5, 1);
		currentDamage = MyAttackDamage;
		//typeText = typeObj.GetComponent<Text>();
		deathText.gameObject.SetActive(false);

		InvokeRepeating("DeathCheck", 0, 0.0001f);
	}

	void DeathCheck()
	{
		if (MaxHP <= 0 && Dead == false) 
		{
			changeState(STATE_DEAD);
			GetComponent<AudioSource> ().PlayOneShot (playerDeathAudio, 1);
			Dead = true;
			//mainMenu.GetComponent<inGameMenu>().optionShow();
			//deathText.gameObject.SetActive(true);

			highScore = PlayerPrefs.GetInt("High Score");

			if(highScore < killCount)
			{
				PlayerPrefs.SetInt("High Score", killCount);
				highScore = PlayerPrefs.GetInt("High Score");
				GetComponent<AudioSource> ().PlayOneShot (HighScoreAudio, 1);
			}

			highScoreText.text = highScore.ToString();


		}

		/*
		//application.load on death
		else if(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") && Dead)
		{
			StartCoroutine(DeathDelay());
		}
		*/

	}

	void FixedUpdate()
	{
		//touched ();
		killText.text = killCount.ToString();
		if (!Dead) 
		{
			touchControl ();
			//keycontrols ();`
		}
		DeathCheck ();
	}
	void Update()
	{

//		Debug.Log (Screen.height);
		if (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Wait"))
		    alertObj.SetActive(true);
		else 
			alertObj.SetActive(false);
	}
	void keycontrols()
	{
		if (Input.GetKeyDown ("right") && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			changeDirection ("right");
			Right = true;
			AttackMode ();


		} else if (Input.GetKeyDown ("left") && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			changeDirection ("left");
			Right = false;
			AttackMode ();
		} 



	}
	
	void touchControl()
	{
		if (Input.touchCount > 0) 
		{
			
			Touch touch = Input.touches[0];
			
			switch (touch.phase) 	
			{
				
			case TouchPhase.Began:
				
				startPos = touch.position;
				Colliding = false;
				break;
				
			case TouchPhase.Ended:
			{
				/*
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

				if (swipeDistVertical > minSwipeDistY) 
					
				{
					
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					
					if (swipeValue > 0)//up swipe
					{
						playerType.changeType("up");
						//typeText.text = "up";
					}
					//Jump ();
					
					else if (swipeValue < 0)//down swipe
					{
						playerType.changeType("down");
						//typeText.text = "down";
					}
					//Shrink ();
					break;
				}
				*/
				if ((touch.position.x > Screen.width/2 && touch.position.y < Screen.height- Screen.height/8) && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
				{
					changeDirection ("right");
					Right = true;
					AttackMode();
				} 
				
				else if ((touch.position.x < Screen.width/2 && touch.position.y < Screen.height- Screen.height/8) && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
				{
					changeDirection ("left");
					Right = false;
					AttackMode();
				}

				break;
			}
			}	
		}
	}

	void AttackMode()
	{

		random = Random.Range (1, 15);

		if(Right)
			SpawnPart = Vector2.right * 2;
		else
			SpawnPart = -Vector2.right * 2;
		SpawnPart.y = 1;

		if (random >= 13)
		{
			changeState(STATE_CRIT);
			tempDelay = CritDelay;
			StartCoroutine (Counting ());
			currentDamage = MyAttackDamage * (Crit / 100);
			GetComponent<AudioSource>().PlayOneShot(playerCritAudio, 1.0f);
			GameObject parti= Instantiate (effectCrit,SpawnPart , Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);


		} 

		else {
			changeState(STATE_ATTACK);
			GetComponent<AudioSource>().PlayOneShot(playerAttackAudio, 1.0f);
			tempDelay = SlashDelay;
			StartCoroutine (Counting ());
			currentDamage = MyAttackDamage;
			GameObject parti= Instantiate (effectSlash, SpawnPart, Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);
		}
	}

	public void Hurt(int damage)
	{
		MaxHP -= damage;
		hpBar.value = MaxHP;
	}

	void changeState(int state)
	{
		switch (state) {
			
		case STATE_ATTACK:
			animator.SetTrigger ("Attack");
			break;
			
		//case STATE_IDLE:
			//animator.SetInteger ("state", STATE_IDLE);
			//break;
		case STATE_DEAD:
			animator.SetTrigger ("Dead");
			break;
		case STATE_CRIT:
			animator.SetTrigger ("Critical");
			break;
		}
	}
	
	void changeDirection(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				transform.Rotate (0, 180, 0);
				_currentDirection = "right";
			}
			else if (direction == "left")
			{
				transform.Rotate (0, -180, 0);
				_currentDirection = "left";
			}
		}
	}

	IEnumerator Counting()
	{
		yield return new WaitForSeconds (tempDelay);
		if (!Colliding) {
			animator.SetTrigger ("Wait");
		}
	}

	IEnumerator DeathDelay()
	{
		yield return new WaitForSeconds (3);
		Application.LoadLevel (Application.loadedLevel);
	}
}