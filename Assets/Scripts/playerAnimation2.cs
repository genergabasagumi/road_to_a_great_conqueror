﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerAnimation2 : MonoBehaviour {
	Animator animator;
	bool _isPlaying_walk = false;
	
	const int STATE_IDLE = 0;
	const int STATE_ATTACK = 1;
	const int STATE_CRIT = 2;

	int _currentAnimationState = STATE_IDLE;
	
	string _currentDirection = "left";
	
	public float minSwipeDistY;
	public float minSwipeDistX;
	
	private Vector2 startPos;
	public GameObject Attack;
	//public float timeAttack;
	//private float count;
	public float MaxHP;
	
	//public Text typeText;
	public GameObject killObj;
	public float Crit;
	public int random;
	public float MyAttackDamage;
	public float currentDamage;

	
	public int killCount;
		
	public Text killText;
	public GameObject effectSlash;
	public GameObject effectCrit;
	public bool Right;
	private Vector3 SpawnPart;
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
	
	void Start()
	{
		killCount = 0;
		killText = killObj.GetComponent<Text> ();
		animator = this.GetComponent<Animator>();
		//damaged = false;
		//playerType = new characterType(10, 5, 5, 1);
		currentDamage = MyAttackDamage;
		//typeText = typeObj.GetComponent<Text>();
	}
	
	void FixedUpdate()
	{

	}
	
	void Update()
	{
		//touched ();
		touchControl ();
		killText.text = killCount.ToString();
	
		keycontrols ();

	}
	
	void keycontrols()
	{
		
		if (Input.GetKeyDown ("right")&&(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Critical")))
		//if (Input.GetKeyDown ("right")&& this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			changeDirection ("right");
			Right = true;
			AttackMode();

		}
		
		else if (Input.GetKeyDown("left")&&(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Critical")))
		//else if (Input.GetKeyDown ("left")&& this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{	
			changeDirection ("left");
			Right = false;
			AttackMode();
		}
		/*
		else if (Input.GetKeyDown("up"))
		{	
			playerType.changeType("up");
			//typeText.text = "up";
		}
		
		else if (Input.GetKeyDown("down"))
		{	
			playerType.changeType("down");
			//typeText.text = "down";
		}
		*/
		
		//else if()
			else
		{
			changeState(STATE_IDLE);

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
				
				break;
				
			case TouchPhase.Ended:
				
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
				if ((touch.position.x > Screen.width/2 && touch.position.y < Screen.height/2) &&(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Critical")))
				{
					changeDirection ("right");
					AttackMode();

				} 
				
				else if ((touch.position.x < Screen.width/2 && touch.position.y < Screen.height/2) &&(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Critical")))
				{
					changeDirection ("left");
					AttackMode();
				}
				else 
				{
					changeState (STATE_IDLE);

				}
				break;
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
		if (random >= 13) {
			changeState(STATE_CRIT);
			currentDamage = MyAttackDamage * (Crit / 100);
			//effectCrit.SetActive(true);

			GameObject parti= Instantiate (effectCrit,SpawnPart , Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);

		} else {
			changeState(STATE_ATTACK);
			GameObject parti= Instantiate (effectSlash, SpawnPart, Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);
			//effectSlash.SetActive(true);
			currentDamage = MyAttackDamage;
		}
	}
	public void Hurt(int damage)
	{
		MaxHP -= damage;
	}
	void changeState(int state)
	{
		if (_currentAnimationState == state)
			return;
		switch (state) {
			
		case STATE_ATTACK:
			animator.SetInteger ("state", STATE_ATTACK);
			break;
			
		case STATE_IDLE:
			animator.SetInteger ("state", STATE_IDLE);
			break;	

		case STATE_CRIT:
			animator.SetInteger ("state", STATE_CRIT);
			break;
		}
		_currentAnimationState = state;
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
}