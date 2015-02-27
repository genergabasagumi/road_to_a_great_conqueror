using UnityEngine;
using System.Collections;

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
	
	void Start()
	{
		animator = this.GetComponent<Animator>();
		damaged = false;
	}

	void FixedUpdate()
	{
		keycontrols ();
	}
	
	void Update()
	{
		//touched ();
		touchControl ();
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