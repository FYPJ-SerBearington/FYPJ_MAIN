/// <summary>
///C# Code Copyright© 2015 by LOO CHUN TAT
/// </summary>
using UnityEngine;
using System.Collections;

public class MobAI : MonoBehaviour
{

	private Animator anim;

	enum MobState
	{
		IDLE = 1,
		PATROL,
		CHASE,
		ATTACK,
		DEATH
	}
	private MobState states;
	public string Statename;
	public float moveSpeed;
	public GameObject ChaseTarget;
	public Vector3 ChaseTargetDirection;
	public float ChaseDistance;
	public float MinChaseDistance,MaxChaseDistance;

	public float PatrolDistance;
	public Vector3 PatrolDirection;
	public Transform[] PatrolPoint;
	public int CurrentPatrolPoint = 0;
	public bool ResetPatrolPoint;


	public float AttackRange;
	public float AttackTimer=0;
	public int AttackDamage;

	public GameObject boomParticle;
	public GameObject Drops;

	public float RestTimer = 0;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		//states = MobState.CHASE;
		states = MobState.PATROL;
		ChaseTarget = GameObject.Find ("Player_Bear");
		moveSpeed = 1;
		MinChaseDistance = 5;
		MaxChaseDistance = 6;
		AttackRange = 1.5f;
		AttackDamage = 5;
	}
	void ChangeState()
	{
		ChaseDistance = Vector3.Distance(transform.position,ChaseTarget.transform.position);
		ChaseTargetDirection = ChaseTarget.transform.position - transform.position;
		switch(states)
		{
			case MobState.IDLE:
			{
				RestTimer+=Time.deltaTime;
				if(RestTimer>2)
				{
					if(ChaseDistance<MinChaseDistance)
					{
						states= MobState.CHASE;
						RestTimer = 0;
					}
					if(ChaseDistance>MaxChaseDistance)
					{
						states= MobState.PATROL;
						RestTimer = 0;
					}
				}
			}break;
			case MobState.PATROL:
			{
				if(PatrolDirection.magnitude < 1.0f)
				{
					states = MobState.IDLE;
				}
				if(ChaseDistance<MinChaseDistance)
				{
					states= MobState.CHASE;
				}
			}break;
			case MobState.CHASE:
			{
				if(ChaseDistance >MaxChaseDistance)
				{
					states = MobState.IDLE;
				}
				if(ChaseDistance < AttackRange)
				{
					states= MobState.ATTACK;
				}
			}break;
			case MobState.ATTACK:
			{
				//
				if(ChaseDistance > MaxChaseDistance)
				{
					states = MobState.IDLE;
				}
			}break;
			case MobState.DEATH:
			{
				
			}break;
		}
	}
	void Respond()
	{
		switch(states)
		{
			case MobState.IDLE:
			{
				anim.SetBool("Attack",false);
				Vector3 newDir = Vector3.RotateTowards(transform.forward, ChaseTargetDirection, moveSpeed*Time.deltaTime, 0.0F);
				Vector3 tempDir = new Vector3(newDir.x ,0,newDir.z);
				//Debug.DrawRay(transform.position, newDir, Color.red);
				ChaseDistance = Vector3.Distance(transform.position,ChaseTarget.transform.position);
				if(ChaseDistance < MaxChaseDistance)
				{
					transform.rotation = Quaternion.LookRotation(tempDir);
				}
			}break;
			case MobState.PATROL:
			{
				anim.SetBool("Attack",false);
				//if current patrol point is not the last point
				if(CurrentPatrolPoint < PatrolPoint.Length)
				{
					//continue patrol
					Vector3 target = PatrolPoint[CurrentPatrolPoint].position;
					PatrolDirection = target - transform.position;
					if(PatrolDirection.magnitude < 1.0f)
					{
						CurrentPatrolPoint++;
						
					}else
					{
						Vector3 newDir = Vector3.RotateTowards(transform.forward, PatrolDirection, moveSpeed*Time.deltaTime, 0.0F);
						//set a new direction that set dir y to 0 , so it won't rotate up and downward
						Vector3 tempDir = new Vector3(newDir.x ,0,newDir.z);
						transform.rotation = Quaternion.LookRotation(tempDir);
						transform.position = Vector3.MoveTowards(transform.position, target,moveSpeed*Time.deltaTime);
					}  
				}else
				{
					//if reset is set to true patrol back to first point
					if(ResetPatrolPoint)
					{
						CurrentPatrolPoint=0;
					}
					//else stop at last point;
				}
			}break;
			case MobState.CHASE:
			{
				anim.SetBool("Attack",false);
				Vector3 newDir = Vector3.RotateTowards(transform.forward, ChaseTargetDirection, moveSpeed*Time.deltaTime, 0.0F);
				//set a new direction that set dir y to 0 , so it won't rotate up and downward
				Vector3 tempDir = new Vector3(newDir.x ,0,newDir.z);
				//Debug.DrawRay(transform.position, newDir, Color.red);
				if(ChaseDistance <MaxChaseDistance && ChaseDistance >MinChaseDistance)
				{
					transform.rotation = Quaternion.LookRotation(tempDir);
				}
				if(ChaseDistance<MinChaseDistance)
				{
					transform.rotation = Quaternion.LookRotation(tempDir);
					transform.position = Vector3.MoveTowards(transform.position, ChaseTarget.transform.position,moveSpeed*Time.deltaTime);
				}
			}break;
			case MobState.ATTACK:
			{
				AttackTimer+=Time.deltaTime;
				anim.SetBool("Attack",true);
				if(AttackTimer>1.0f)
				{
					//if current distance is shorter than attack range
					if(ChaseDistance<AttackRange*2)
					{
						//play attack animation
						//and deal damage to player
						//if player is blocking reduce the damage done by 1/4;
						if(ChaseTarget.GetComponent<BearController>().isBlocking )
						{
							ChaseTarget.GetComponent<BearController>().health -= (AttackDamage*0.25f);
						}else
						{
							ChaseTarget.GetComponent<BearController>().health -=AttackDamage;
						}
						AttackTimer = 0;
					}
					AttackTimer = 0;
				}
			}break;
			case MobState.DEATH:
			{
				//destroy mob and do death animation
				Instantiate(boomParticle,new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
				dropStuff(10);
				Destroy(this.gameObject);
				//spawn essence and toy brick
			}break;
		}
	}

	void dropStuff(int count)
	{
		for(int i = 0 ; i<count ; i++)
		{
			Instantiate(Drops,new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			dropStuff(10);
		}
		Statename = states.ToString();
		if(GetComponent<MobStatus>().Health <0)
		{
			Debug.Log("die");
			states= MobState.DEATH;
		}
		switch(states)
		{
			case MobState.IDLE:
			{
				ChangeState();
				Respond();
			}break;
			case MobState.PATROL:
			{
				ChangeState();
				Respond();
			}break;
			case MobState.CHASE:
			{
				ChangeState();
				Respond();
			}break;
			case MobState.ATTACK:
			{
				ChangeState();
				Respond();
			}break;
			case MobState.DEATH:
			{
				ChangeState();
				Respond();
			}break;
		}
	}
}
