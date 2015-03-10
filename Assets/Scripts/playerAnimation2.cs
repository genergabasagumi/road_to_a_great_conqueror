using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerAnimation2 : MonoBehaviour {

	public AudioClip playerDeathAudio;
	public AudioClip playerAttackAudio;
	public AudioClip playerCritAudio;
	bool Dead = false;

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

	public bool isEnemyHit;	


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

		InvokeRepeating("DeathCheck", 0, 0.0001f);
	}
	


	void DeathCheck()
	{
		if (MaxHP <= 0 && Dead == false) 
		{
			changeState(STATE_DEAD);
			//GetComponent<AudioSource> ().PlayOneShot (playerDeathAudio, 1);
			Dead = true;
		}
		else if(!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dead") && Dead)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	void FixedUpdate()
	{
		//touched ();

		killText.text = killCount.ToString();
		if (!Dead) 
		{
			touchControl ();
			//keycontrols ();
		}
		DeathCheck ();
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
				if ((touch.position.x > Screen.width/2 ) && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
				{
					changeDirection ("right");
					Right = true;
					AttackMode();
				} 
				
				else if ((touch.position.x < Screen.width/2) && this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
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
		if (random >= 13) {
			changeState(STATE_CRIT);
			//animator.SetInteger ("state", 2);
			currentDamage = MyAttackDamage * (Crit / 100);
			//effectCrit.SetActive(true);


			//GetComponent<AudioSource>().PlayOneShot(playerCritAudio, 1.0f);
			GameObject parti= Instantiate (effectCrit,SpawnPart , Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);


		} else {
			changeState(STATE_ATTACK);
			//animator.SetInteger ("state", 1);
			//GetComponent<AudioSource>().PlayOneShot(playerAttackAudio, 1.0f);
			//if(isEnemyHit)
			currentDamage = MyAttackDamage;
			GameObject parti= Instantiate (effectSlash, SpawnPart, Quaternion.identity) as GameObject;
			Destroy (parti, 1.5f);
			//effectSlash.SetActive(true);

		}
	}

	public void Hurt(int damage)
	{
		MaxHP -= damage;
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
}