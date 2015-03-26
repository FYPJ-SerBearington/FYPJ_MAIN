using UnityEngine;
using System.Collections;

public class Boss_AI : MonoBehaviour
{
	enum States
	{
		IDLE,
		MOVING,
		ATTACKING,
		STUNNED,
		DEAD
	}
	enum Attacks
	{
		NONE,
		DROPPING,
		THROWING,
		SMACKING
	}
	enum Movement
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		TELEPORT,
		REST
	}
	//
	public int _phrases;
	//
	private States _bossMainState;
	private Attacks _bossAttackState;
	private Movement _bossMovementState;
	//string to print out states
	public string S_bossMainState;
	public string S_bossAttackState;
	public string S_bossMovementState;
	//

	//spawn of phrase 2
	public GameObject spawn2;
	//block is projectile of the boss at phrase 1
	public GameObject projectileBlock;
	//ball is projectile of the boss at phrase 2
	public GameObject projectileBall;
	//
	private bool canMove;
	//
	public int _dropCounter;
	//timer for attack
	public float _Timer;

	//random number for throwing 2 lanes at once
	private int Rnum ;
	private int Rnum2;
	private Vector3[] _ballpos;
	// Use this for initialization
	void Start ()
	{
		_phrases = 1;
		_bossMainState = States.MOVING;
		_bossAttackState = Attacks.NONE;
		_bossMovementState = Movement.REST;
		canMove = false;
		_Timer = 0;
		_dropCounter = 0;
		Rnum = 0;
		Rnum2 = 1;
		_ballpos = new Vector3[3];
		_ballpos [0] = GameObject.Find ("P2_BossS1").transform.position;
		_ballpos [1] = GameObject.Find ("P2_BossS2").transform.position;
		_ballpos [2] = GameObject.Find ("P2_BossS3").transform.position;
	}
	void OnTriggerEnter(Collider other)
	{
		if(_phrases == 1)
		{
			if(other.tag == "Tag_B1_rwall")
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				_bossMovementState = Movement.RIGHT;
				_bossMainState = States.MOVING;
			}
			if(other.tag == "Tag_B1_lwall")
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				_bossMovementState = Movement.LEFT;
				_bossMainState = States.MOVING;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(_phrases == 1)
		{
			if(other.tag == "Tag_B1_rwall")
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
			if(other.tag == "Tag_B1_lwall")
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}
	void ChangeState()
	{
		switch(_bossMainState)
		{
			case States.IDLE:
			{
				if(_Timer>=3)
				{
					_bossMainState = States.ATTACKING;
					_bossAttackState = Attacks.THROWING;
					_Timer = 0;
				}
			}break;
			case States.MOVING:
			{
				if(_phrases == 1 && canMove == false)
				{
					_bossMainState = States.MOVING;
					_bossMovementState = Movement.LEFT;
					canMove = true;
				}
				if(_phrases == 2)
				{
					_bossMainState = States.MOVING;
					_bossMovementState = Movement.TELEPORT;
				}
			}break;
			case States.ATTACKING:
			{
				if(_phrases == 1)
				{
					if(_Timer>=3)
					{
						_bossMainState = States.ATTACKING;
						_bossAttackState = Attacks.DROPPING;
						_Timer = 0;
					}
				}
				if(_phrases == 2)
				{
					//if very near use smacking
					//else use throwing
					_bossMainState = States.ATTACKING;
					_bossAttackState = Attacks.THROWING;
				}
			}break;
			case States.STUNNED:
			{

			}break;
			case States.DEAD:
			{
				
			}break;
		}
		//main state is moving
		if(_bossMainState == States.MOVING)
		{
			switch(_bossMovementState)
			{
				case Movement.UP:
				{
					
				}break;
				case Movement.DOWN:
				{
					
				}break;
				case Movement.LEFT:
				{
					//
					if(_Timer>=3)
					{
						_bossMainState = States.ATTACKING;
						_bossAttackState = Attacks.DROPPING;
						_Timer = 0;
					}
				}break;
				case Movement.RIGHT:
				{
					//
					if(_Timer>=3)
					{
						_bossMainState = States.ATTACKING;
						_bossAttackState = Attacks.DROPPING;
						_Timer = 0;
					}
				}break;
				case Movement.TELEPORT:
				{
					if(_phrases == 2)
					{
						if(_Timer>=3)
						{
							_bossMainState = States.ATTACKING;
							_bossAttackState = Attacks.THROWING;
							_Timer = 0;
						}
					}
				}break;
				case Movement.REST:
				{

				}break;
			}
		}
		if(_bossMainState == States.ATTACKING)
		{
			switch(_bossAttackState)
			{
				case Attacks.NONE:
				{
					
				}break;
				case Attacks.DROPPING:
				{
					//_bossMainState = States.MOVING;
					if(_dropCounter >=1)
					{
						_bossMainState = States.MOVING;
						_dropCounter = 0;
					}
				}break;
				case Attacks.THROWING:
				{
					if(_dropCounter >=1)
					{
						_bossMainState = States.IDLE;
						_dropCounter = 0;
					}
				}break;
				case Attacks.SMACKING:
				{

				}break;
			}
		}
	}
	
	/// <summary>
	/// respond to the current state 
	/// </summary>
	void Respond()
	{
		switch(_bossMainState)
		{
			case States.IDLE:
			{
				
			}break;
			case States.MOVING:
			{
				
			}break;
			case States.ATTACKING:
			{
				
			}break;
			case States.STUNNED:
			{
				
			}break;
			case States.DEAD:
			{
				//destroy boss and spawn explosion particle
				Destroy(this.gameObject);
				
			}break;
		}
		//main state is moving
		if(_bossMainState == States.MOVING)
		{
			switch(_bossMovementState)
			{
				case Movement.UP:
				{
					
				}break;
				case Movement.DOWN:
				{
					
				}break;
				case Movement.LEFT:
				{
					this.transform.Translate(0,0,-Time.deltaTime);
				}break;
				case Movement.RIGHT:
				{
					this.transform.Translate(0,0,Time.deltaTime);
				}break;
				case Movement.TELEPORT:
				{
					this.transform.position = spawn2.transform.position;
				}break;
				case Movement.REST:
				{
					//
				}break;
			}
		}
		if(_bossMainState == States.ATTACKING)
		{
			switch(_bossAttackState)
			{
				case Attacks.NONE:
				{
					
				}break;
				case Attacks.DROPPING:
				{
					_dropCounter++;
					projectileBlock = Instantiate(projectileBlock,new Vector3(this.transform.position.x,this.transform.position.y - 1,this.transform.position.z),Quaternion.identity) as GameObject;
					projectileBlock.name = "Blocky";
				}break;
				case Attacks.THROWING:
				{
					_dropCounter++;
					Rnum = Random.Range(0,3);
					Rnum2 = Random.Range(0,3);
					projectileBall = Instantiate(projectileBall,new Vector3(_ballpos[Rnum].x,_ballpos[Rnum].y,_ballpos[Rnum].z),Quaternion.identity) as GameObject;
					projectileBall.name = "P_Ball";
					//if rnum2 is same as rnum
					if(Rnum2 == Rnum)
					{
						//random rnum2 again
						Rnum2 = Random.Range(0,3);
					}else
					{
						//spawn rnum2
						projectileBall = Instantiate(projectileBall,new Vector3(_ballpos[Rnum2].x,_ballpos[Rnum2].y,_ballpos[Rnum2].z),Quaternion.identity) as GameObject;
						projectileBall.name = "P_Ball2";
					}
				}break;
				case Attacks.SMACKING:
				{
					
				}break;
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		S_bossMainState = _bossMainState.ToString();
		S_bossAttackState = _bossAttackState.ToString();
		S_bossMovementState = _bossMovementState.ToString();
		_Timer += Time.deltaTime;
		//main state update
		switch(_bossMainState)
		{
			case States.IDLE:
			{
				ChangeState();
				Respond();
			}break;
			case States.MOVING:
			{
				ChangeState();
				Respond();
			}break;
			case States.ATTACKING:
			{
				ChangeState();
				Respond();
			}break;
			case States.STUNNED:
			{
				ChangeState();
				Respond();
			}break;
			case States.DEAD:
			{
				ChangeState();
				Respond();
			}break;
		}
		//movement update
		switch(_bossMovementState)
		{
			case Movement.UP:
			{
				ChangeState();
				Respond();
			}break;
			case Movement.DOWN:
			{
				ChangeState();
				Respond();
			}break;
			case Movement.LEFT:
			{
				ChangeState();
				Respond();
			}break;
			case Movement.RIGHT:
			{
				ChangeState();
				Respond();
			}break;
			case Movement.TELEPORT:
			{
				ChangeState();
				Respond();
			}break;
			case Movement.REST:
			{
				ChangeState();
				Respond();
			}break;
		}
		//attacking update
		if(_bossMainState == States.ATTACKING)
		{
			switch(_bossAttackState)
			{
				case Attacks.NONE:
				{
					ChangeState();
					Respond();
				}break;
				case Attacks.DROPPING:
				{
					ChangeState();
					Respond();
				}break;
				case Attacks.THROWING:
				{
					ChangeState();
					Respond();
				}break;
				case Attacks.SMACKING:
				{
					ChangeState();
					Respond();
				}break;
			}
		}
	}
}
