using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerAnimation : MonoBehaviour {
	Animator animator;
	bool _isPlaying_walk = false;

	const int STATE_IDLE = 0;
	const int STATE_ATTACK = 1;
	int _currentAnimationState = STATE_IDLE;

	string _currentDirection = "left";
	
	public float minSwipeDistY;
	public float minSwipeDistX;

	private Vector2 startPos;
	public GameObject Attack;
	public float timeAttack;
	private float count;
	public int MaxHP;

	public Text typeText;
	public GameObject typeObj;


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
		animator = this.GetComponent<Animator>();
		//damaged = false;
		playerType = new characterType(10, 5, 5, 1);
		typeText = typeObj.GetComponent<Text>();

	}

	void FixedUpdate()
	{
		keycontrols ();
	}
	
	void Update()
	{
		//touched ();
		touchControl ();
		Debug.Log (playerType.Type);
	}
	
	void keycontrols()
	{

		if (Input.GetKeyDown ("right"))
		{
			changeDirection ("right");
			changeState(STATE_ATTACK);
			if(!Attack.activeSelf)
			{
				Attack.SetActive (true);
			}
		}

		else if (Input.GetKeyDown("left"))
		{	
			changeDirection ("left");
			changeState(STATE_ATTACK);
			if(!Attack.activeSelf)
			{
				Attack.SetActive (true);
			}
		}

		else if (Input.GetKeyDown("up"))
		{	
			playerType.changeType("up");
			typeText.text = "up";
		}

		else if (Input.GetKeyDown("down"))
		{	
			playerType.changeType("down");
			typeText.text = "down";
		}

		else
		{
			changeState(STATE_IDLE);
			if(Attack.activeSelf)
			{
				count += 1 * Time.deltaTime;
				if(count >= timeAttack)
				{
					Attack.SetActive(false);
					count = 0;
				}
			}
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
						typeText.text = "up";
					}
					//Jump ();
					
						else if (swipeValue < 0)//down swipe
					{
						playerType.changeType("down");
						typeText.text = "down";
					}
					//Shrink ();
					break;
				}
				if (touch.position.x > Screen.width/2 && touch.position.y < Screen.height/2) 
				{
					changeDirection ("right");
					changeState (STATE_ATTACK);
					if(!Attack.activeSelf)
					{
						Attack.SetActive (true);
					}
				} 
				
				else if (touch.position.x < Screen.width/2 && touch.position.y < Screen.height/2) 
				{
					changeDirection ("left");
					changeState (STATE_ATTACK);
					if(!Attack.activeSelf)
					{
						Attack.SetActive (true);
					}
				}
				else 
				{
					changeState (STATE_IDLE);
					if(Attack.activeSelf)
					{
						count += 1 * Time.deltaTime;
						if(count >= timeAttack)
						{
							Attack.SetActive(false);
							count = 0;
						}
					}
				}
				break;
			}


		
		}
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