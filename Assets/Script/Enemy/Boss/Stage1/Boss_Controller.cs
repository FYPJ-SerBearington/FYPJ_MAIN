using UnityEngine;
using System.Collections;

public class Boss_Controller : MonoBehaviour
{
	//all the states boss have
	enum states
	{
		IDLE = 1,
		TELEPORTING,
		MOVElEFT,
		MOVERIGHT,
		ATTACKING,
		THROWING,
		SPECIAL,
		STUN,
		DEAD
	}
	public int BossHp =100;
	//theres 2 phrases of boss fight
	private int _phrases;

	//spawn of phrase 2
	public GameObject spawn2;
	//the boss
	public GameObject Boss;
	//block is projectile of the boss at phrase 1
	public GameObject projectileBlock;
	//ball is projectile of the boss at phrase 2
	public GameObject projectileBall;
	private Vector3[] _ballpos;
	//state of the boss
	private states _bossState;
	public string BcurState;
	//direction of boss phrase one
	public int dir;
	public float lastdir;
	//timer 
	public float bossTimer;
	public float delayTime = 3.0f;

	//testing
	public int counter;

	public int Rnum;
	public int Rnum2;
	// Use this for initialization
	void Start()
	{
		//starting phrases is 1
		_phrases = 1;
		_ballpos = new Vector3[3];
		_ballpos [0] = GameObject.Find ("P2_BossS1").transform.position;
		_ballpos [1] = GameObject.Find ("P2_BossS2").transform.position;
		_ballpos [2] = GameObject.Find ("P2_BossS3").transform.position;
		//blocks counter
		counter = 0;
		//starting direction
		dir = 0;
		lastdir = 1;
		//starting time
		bossTimer = 0;
		//starting boss state
		_bossState = states.ATTACKING;
		//bossState = states.TELEPORTING;
		Rnum = 0;
	}

	void OnTriggerEnter(Collider other)
	{
		if(_phrases == 1)
		{
			if(other.tag == "Tag_B1_rwall")
			{
				Boss.GetComponent<Renderer>().material.color = Color.red;
				_bossState = states.MOVERIGHT;
			}
			if(other.tag == "Tag_B1_lwall")
			{
				Boss.GetComponent<Renderer>().material.color = Color.red;
				_bossState = states.MOVElEFT;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(_phrases == 1)
		{
			if(other.tag == "Tag_B1_rwall")
			{
				Boss.GetComponent<Renderer>().material.color = Color.white;
			}
			if(other.tag == "Tag_B1_lwall")
			{
				Boss.GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}
	void ChangeState()
	{
		if(_phrases == 1)
		{
			switch(_bossState)
			{
				case states.MOVElEFT:
				{
					if(bossTimer>=delayTime)
					{
						bossTimer = 0.0f;
						_bossState = states.ATTACKING;
					}
				}break;
				case states.MOVERIGHT:
				{
					if(bossTimer>=delayTime)
					{
						bossTimer = 0.0f;
						_bossState = states.ATTACKING;
					}
				}break;
				case states.ATTACKING:
				{
					//need to check trigger
					if(counter>5)
					{
						counter = 0;
						_phrases = 2;
						dir =0;
						_bossState = states.TELEPORTING;
					}else
					{
						if(lastdir == 1)
						{
							_bossState = states.MOVElEFT;
						}
						if(lastdir == 2)
						{
							_bossState = states.MOVERIGHT;
						}
						dir = 0;
					}
				}break;
			}
		}
		if(_phrases == 2)
		{
			switch(_bossState)
			{
				case states.IDLE:
				{
					//if boss still have hp
					if(BossHp>0)
					{
						//check timer more than the delattime
						if(bossTimer>=delayTime)
						{
							//reset
							bossTimer = 0.0f;
							_bossState = states.THROWING;
							//if within range change state to attacking (melee)
							//else change state to throwing
						}
					}else
					{
						//if boss don't have any hp left e.g 0 or lesser
						//reset timer
						bossTimer = 0.0f;
						//change state to dead
						_bossState = states.DEAD;
					}
				}break;
				case states.TELEPORTING:
				{
					//if cinimatic end
					//teleport boss to the position at the top of the cupboard
					if(bossTimer>=delayTime)
					{
						bossTimer = 0.0f;
						_bossState = states.THROWING;
					}
					
				}break;
				case states.ATTACKING:
				{
					//
				}break;
				case states.THROWING:
				{
					//as long as counter is more than 1
					if(counter >=1)
					{
						//change state to idle
						_bossState = states.IDLE;
					}
				}break;
				case states.SPECIAL:
				{
					//
				}break;
				case states.DEAD:
				{
					//
				}break;
			}
		}
	}//end change state;
	void Respond()
	{
		if(_phrases == 1)
		{
			switch(_bossState)
			{
				case states.MOVElEFT:
				{
					lastdir = 1;
					Boss.transform.Translate(0,0,Time.deltaTime);
				}break;
				case states.MOVERIGHT:
				{
					lastdir = 2;
					Boss.transform.Translate(0,0,-Time.deltaTime);
				}break;
				case states.ATTACKING:
				{
					dir++;
					counter ++;
					projectileBlock = Instantiate(projectileBlock,new Vector3(Boss.transform.position.x,Boss.transform.position.y - 1,Boss.transform.position.z),Quaternion.identity) as GameObject;
					projectileBlock.name = "P_Tblock";
				}break;
				case states.DEAD:
				{
					
				}break;
			}
		}
		if(_phrases == 2)
		{
			switch(_bossState)
			{
				case states.IDLE:
				{
					
				}break;
				case states.TELEPORTING:
				{
					Boss.transform.position = spawn2.transform.position;
				}break;
				case states.ATTACKING:
				{
					//melee attack
					
				}break;
				case states.THROWING:
				{
					counter++;
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
				case states.SPECIAL:
				{
					//spawn all three lanes
				}break;
				case states.DEAD:
				{
					//do death animation
					//set stage1_bossCleared  to true(at game manager)
					
				}break;
			}
		}
	}//end respond

	// Update is called once per frame
	void Update ()
	{
		bossTimer += Time.deltaTime;
		BcurState = _bossState.ToString();
		if(_phrases == 1)
		{
			switch(_bossState)
			{
				case states.MOVElEFT:
				{
					ChangeState();
					Respond();
				}break;
				case states.MOVERIGHT:
				{
					ChangeState();
					Respond();
				}break;
				case states.ATTACKING:
				{
					ChangeState();
					Respond();
				}break;
				case states.DEAD:
				{
					ChangeState();
					Respond();
				}break;
			}
		}
		if(_phrases == 2)
		{
			switch(_bossState)
			{
				case states.IDLE:
				{
					ChangeState();
					Respond();
				}break;
				case states.TELEPORTING:
				{
					ChangeState();
					Respond();
				}break;
				case states.ATTACKING:
				{
					ChangeState();
					Respond();
				}break;
				case states.THROWING:
				{
					ChangeState();
					Respond();
				}break;
				case states.SPECIAL:
				{
					ChangeState();
					Respond();
				}break;
				case states.DEAD:
				{
					ChangeState();
					Respond();
				}break;
			}
		}
	}
}
