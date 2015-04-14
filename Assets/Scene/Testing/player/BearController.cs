/// <summary>
/// Bear controller.
/// </summary>
using UnityEngine;
using System.Collections;

public class BearController : MonoBehaviour
{
	private Animator anim;
	private Rigidbody rb;
	/// <summary>
	/// health etc.
	/// </summary>
	public float health = 0;
	public int WeaponTier = 0;
	public int ShieldTier = 0;

	public bool ControllerActive = true;
	public bool CanMove = true;
	public bool canRun = false;
	public bool onGround = true;
	/// <summary>
	///for attacking and combo timer 
	/// </summary>
	public float AttactTimer = 0;
	public bool AttactTimerStart = false;
	public bool canAttack = true;
	public bool isAttacking = false;
	public bool canCombo = false;
	/// <summary>
	/// block.
	/// </summary>
	public bool canBlock = true;
	public bool UnBlock = false;
	public bool isBlocking = false;
	public float BlockTimer  =0; 
	/// <summary>
	/// sidestep / dash left right timer
	/// </summary>
	public float D_Timer = 0;
	public bool D_TimerStart= false;
	public float D_resetTime = 1;
	/// <summary>
	/// jumping timer
	/// </summary>
	public float J_Timer=0;
	public bool J_TimerStart= false;
	public float J_resetTime = 2;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		rb = GetComponent <Rigidbody>();
	}

	// Use this for initialization
	void Start ()
	{

	}
	void OnCollisionEnter(Collision theCollision)
	{
		if(theCollision.gameObject.tag == "Tag_Floor" || theCollision.gameObject.tag == "Tag_Climbable" ||theCollision.gameObject.tag == "Tag_B1_platform" )
		{
			onGround = true;
			Debug.Log("on_floor");
		}
	}
	void OnCollisionExit(Collision theCollision)
	{
		if(theCollision.gameObject.tag == "Tag_Floor" || theCollision.gameObject.tag == "Tag_Climbable" ||theCollision.gameObject.tag == "Tag_B1_platform" )
		{
			//onGround = false;
			Debug.Log("off_floor");
		}
	}
	void FixedUpdate()
	{
		if(CanMove)
		{
			if(D_Timer == 0)
			{
				//side dash
				if(Input.GetKeyDown(KeyCode.A))
				{
					rb.velocity -= transform.right * 4;
					rb.velocity += Vector3.up * 4;
					D_TimerStart = true;
				}
				if(Input.GetKeyDown(KeyCode.D))
				{
					rb.velocity += transform.right * 4;
					rb.velocity += Vector3.up * 4;
					D_TimerStart = true;
				}
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		//-------------------//
		//Health etc . etc . 
		//-------------------//
		if(health <=0)
		{
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
			{
				anim.SetBool("Dead",true);
				//anim.SetBool("Dead",false);
				health = 0;
				ControllerActive = false;
			}else
			{
				anim.SetBool("Dead",false);
			}
		}else
		{
			if(ControllerActive)
			{
				//-----------------//
				//Mouse
				//-----------------//
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
				{
					isBlocking = true;
				}else
				{
					isBlocking = false;
				}

				if(canBlock)
				{
					if(Input.GetMouseButtonDown(1))
					{
						anim.SetBool("Block",true);
					}
				}
				if(Input.GetMouseButtonUp(1))
				{
					anim.SetBool("Block",false);
				}

				if(isBlocking == false)
				{
					CanMove = true;

					if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
					{
						isAttacking = true;
					}else
					{
						isAttacking = false;
					}
					if(canCombo)
					{
						if(Input.GetMouseButtonDown(0))
						{
							if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
							{
								anim.SetBool("Attack2",true);
								AttactTimerStart = true;
								canCombo = false;
							}
						}
					}else
					{
						if(AttactTimer == 0)
						{
							if(Input.GetMouseButtonDown(0))
							{
								if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
								{
									anim.SetBool("Attack1",true);
									AttactTimerStart = true;
									canCombo = true;
								}
							}
						}
					}
					if(Input.GetMouseButtonUp(0))
					{
						if(canCombo)
						{
							anim.SetBool("Attack1",false);
							anim.SetBool("Attack2",false);
						}else
						{
							anim.SetBool("Attack1",false);
						}
					}
					//to reset 
					if(AttactTimerStart)
					{
						AttactTimer+=Time.deltaTime;
						if(AttactTimer>1)
						{
							AttactTimerStart = false;
							AttactTimer = 0;
							canCombo = false;
						}
					}
				}//end if isblocking is false
				else
				{
					CanMove = false;
				}
				//-----------------//
				//Keyboard
				//-----------------//
				if(CanMove)
				{
					//toggle running
					if(Input.GetKeyDown(KeyCode.LeftShift))
					{
						canRun = true;
					}
					if(Input.GetKeyUp(KeyCode.LeftShift))
					{
						canRun = false;
					}
					//move front
					if(Input.GetKeyDown(KeyCode.W))
					{
						if(!canRun)
						{
							anim.SetBool("Walking_Front",true);
						}
					}
					if(Input.GetKeyUp(KeyCode.W))
					{
						if(!canRun)
						{
							anim.SetBool("Walking_Front",false);
						}
					}
					if(Input.GetKey (KeyCode.W))
					{
						if(!canRun)
						{
							transform.Translate(Vector3.forward * 1 * Time.deltaTime);
							anim.SetBool("Running",false);
						}else
						{
							anim.SetBool("Running",true);
							transform.Translate(Vector3.forward * 5 * Time.deltaTime);
						}
					}
					//move back
					if(Input.GetKeyDown(KeyCode.S))
					{
						anim.SetBool("Walking_Back",true);
					} 
					if(Input.GetKeyUp(KeyCode.S))
					{
						anim.SetBool("Walking_Back",false);
					}
					if(Input.GetKey (KeyCode.S))
					{
						transform.Translate(Vector3.forward * -1 * Time.deltaTime);
					}
					//jump
					if(J_Timer == 0)
					{
						if(Input.GetKeyDown(KeyCode.Space))
						{
							anim.SetBool("Jumping",true);
							J_TimerStart = true;
							//rb.velocity += Vector3.up * 6;
							rb.velocity += transform.up * 6;
							rb.velocity += transform.forward * 2;
							//rb.velocity += Vector3.forward * 5;
						}
					}
					if(Input.GetKeyUp(KeyCode.Space))
					{
						anim.SetBool("Jumping",false);
					}
				}//end canMove

				//timer for limiting jumping once every 2 second
				if(J_TimerStart)
				{
					J_Timer+= Time.deltaTime;
					if(J_Timer >J_resetTime)
					{
						J_TimerStart = false;
						J_Timer= 0;
					}
				}//end timer

				//timer for limiting dashing once every 2 second
				if(D_TimerStart)
				{
					D_Timer+= Time.deltaTime;
					if(D_Timer >D_resetTime)
					{
						D_TimerStart = false;
						D_Timer= 0;
					}
				}//end timer
			}//end controlleractive
		}
	}//end update
}