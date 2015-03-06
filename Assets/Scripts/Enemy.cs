using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public GameObject Player;
	public float speed;
	private float moveSpeed;
	public bool Right;
	public int MyDamage;
	public bool Hit;
	public GameObject Pos;
	public GameObject StartPos;
	public float AttackDelay;
	public float count;
	public bool Attack;
	public bool Imhurt;
	public float MyHp;

	Animator animator;
	const int STATE_IDLE = 0;
	const int STATE_WALK= 1;
	const int STATE_ATTACK = 2;
	int _currentAnimationState = STATE_IDLE;

	void Start () 
	{
		animator = this.GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");
		count = (AttackDelay - 0.7f);

		//Destroy (gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveSpeed = speed * Time.deltaTime;
		Debug.DrawLine(StartPos.transform.position,Pos.transform.position,Color.blue);
		Hit = Physics.Linecast (StartPos.transform.position,Pos.transform.position, 1 << LayerMask.NameToLayer("Player"));

		if (!Hit) {
			this.transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, moveSpeed);
			changeState (STATE_WALK);
		} 

		if(Hit)
		{	
			if(Player.GetComponent<playerAnimation2> ().MaxHP > 0)
				count += 1 * Time.deltaTime;
			if (AttackDelay <= count) 
			{
				Player.GetComponent<playerAnimation2> ().Hurt(MyDamage);
				changeState(STATE_ATTACK);
				count = 0;
			}
			else
				changeState(STATE_IDLE);
		}

	//	if (Imhurt) {
			//MyHp -= Player.GetComponent<playerAnimation2> ().currentDamage;
			//Imhurt = false;
	//	}

		if (Player.transform.position.x >= this.transform.position.x)
			Right = true;

		else
			Right = false;
		if (MyHp <= 0) {
			Player.GetComponent<playerAnimation2>().killCount += 1;
			DestroyObject (this.gameObject);
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
			
		case STATE_WALK:
			animator.SetInteger ("state", STATE_WALK);
			break;
		}
		_currentAnimationState = state;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == ("Attack")) 
		{
		//	Imhurt = true;
			MyHp -= Player.GetComponent<playerAnimation2> ().currentDamage;
			Imhurt = false;
		}
	}
}
