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
	public float AttackDelay;
	private float count;
	public bool Attack;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		//Destroy (gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveSpeed = speed * Time.deltaTime;
		Debug.DrawLine(this.transform.position,Pos.transform.position,Color.blue);
		Hit = Physics2D.Linecast (this.transform.position,Pos.transform.position, 1 << LayerMask.NameToLayer("Player"));

		if (!Hit) 
		{
			this.transform.position = Vector3.MoveTowards (transform.position, Player.transform.position, moveSpeed);
		}

		if (Hit)
		{
			count += 1 * Time.deltaTime;
			if (AttackDelay <= count) 
			{
				Attack = true;
				count = 0;
			}
		}

		if (Attack)
		{
			if(Player.GetComponent<playerAnimation> ().MaxHP >= 0)
			{
				Player.GetComponent<playerAnimation> ().MaxHP -= MyDamage;
				Attack = false;
			}
		}

		if (Player.transform.position.x >= this.transform.position.x)
		{
			Right = true;
		}

		else
		{
			Right = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == ("Attack")) 
		{
			DestroyObject(this.gameObject);
		}
	}
}
