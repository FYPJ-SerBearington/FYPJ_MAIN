/// <summary>
/// Advance movement.cs
/// Gibbie Chairul
/// 3/24/2015 - 3/25/2015
/// 
/// This script is responsinble for getting the players movement inputs and adjusting the characters animations accordindly
/// 
/// This will be automatically attached to your player or mob with the use of the PlayerInput.cs and AI.cs scripts
///
/// This script assumes that you have these animation with the following names:
/// Player:
/// walk - a walk animation
/// run  - run animation
/// side - strafing animation
/// jump - jumping animation
/// fall - falling animation
/// idle - idle animation	
/// 
/// mob
/// run - run animation
/// jump - jump animation
/// fall - fall animation
/// idle - idle animation
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class AdvanceMovement : MonoBehaviour
{
	//FSM
	public enum State
	{
		Init,
		Idle,
		Setup,
		Run,
	}
	//enumerations for moving our characters
	public enum Turn
	{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward
	{
		back = -1,
		none = 0,
		forward = 1
	}
	public float walkSpeed = 0.5f;			//the speed our character walks at
	public float runMultiplier = 2; 	//how fast the player can run
	public float rotateSpeed = 250;		//the rotation of our player
	public float strafeSpeed = 2.5f; 		//the speed of our character strafes at 
	public float gravity = 20;				//the setting for gravity
	public float airTime = 0;				//how long have we been in the air since the last time we touch the ground
	public float fallTime = 0.5f;			//the length of time we have to be falling before the system knows its a fall
	public float jumpHeight = 5;			//how height your player jump
	public float jumpTime = 1.5f;			//this will allows us to compare our airTime and jumpTime to see if playing the jump animation			

	private CollisionFlags _collisionFlags; 	//The collisionFlags we have from the last frame.
	private Vector3 _moveDirection;				//This is the direction our character is moving	
	private Transform _myTransform;				//our cached transform
	private CharacterController _controller;	//our cached CharacterController
	private Animation _anim;						//Animations components


	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;
	private bool _jump;

	private State _state;

	public void Awake()
	{
		_myTransform = transform;
		_controller = GetComponent<CharacterController> ();

		_state = AdvanceMovement.State.Init;
	}

	// Use this for initialization
	IEnumerator Start ()
	{
		while (true)
		{
			yield return null;
			switch (_state)
			{
				case State.Init:
				{
					Init();
				}break;
				case State.Setup:
				{
					SetUp();
				}break;
				case State.Run:
				{
					ActionPicker();
				}break;
			}
		}
	}
	private void Init()
	{
		//check if there is CharacterController component
		if (!GetComponent<CharacterController> ())return;
		//check if there is Animation Component
		if (!GetComponent<Animation> ())return;

		_state = AdvanceMovement.State.Setup;
	}

	private void SetUp()
	{
		_moveDirection = Vector3.zero; 			//Zero our the vector3 we will use for moving our player
		_anim = GetComponent<Animation>();
		
		_anim.Stop ();
		_anim.wrapMode = WrapMode.Loop;
//		_anim ["jump"].layer = 1; // more weight to use for jump animation. The heigher the layer the more weight used for that animation compare to the one that's lower that it.  
//		_anim ["jump"].wrapMode = WrapMode.Once; //we do not want to jump animation in the air to be looping

		//_anim.Play ("Idle");				//start the idle animation when the script starts

		//initializing our AdvanceMovement to none which is idle
		_turn = AdvanceMovement.Turn.none;
		_forward = AdvanceMovement.Forward.none;
		_strafe = AdvanceMovement.Turn.none;
		_run = true;
		_jump = false;

		_state = AdvanceMovement.State.Run;
	}
	//reason i do this is because im going to use it for to be as FSM where the mob pick the action that is suppose to be performing
	private void ActionPicker()
	{
		//allow the player to turn(rotate) left and right
		_myTransform.Rotate(0,(int)_turn * Time.deltaTime *rotateSpeed, 0);

		
		//if we are on the ground, let us move
		if (_controller.isGrounded)
		{
			//reset the air timer if we are on the ground
			airTime = 0;
			
			//Get the user input if we shuold be moving forward or sidesways
			//we will calculate a new vector3 for where the player needs to be
			_moveDirection = new Vector3((int)_strafe,0,(int)_forward);
			//_moveDirection = Vector3.forward * Input.GetAxis("Vertical");
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(_forward != Forward.none)
			{									//if player is pressing forward
				if(_run)
				{										//and pressing the run key
					_moveDirection *= runMultiplier;							//move player at run speed
					Run();														//run animation
				}else
				{
					Walk();														//walk animation
				}
			}
			else if(_strafe != AdvanceMovement.Turn.none)
			{
				Strafe();
			}else
			{
				Idle();															//idle animation
			}
			if(_jump)
			{
				//If player are actually falling for any amount of time we don't want the person to be able to "double" jump in mid air. This will stop that
				if(airTime < jumpTime)
				{											//if we have not already been in the air too long
					_moveDirection.y += jumpHeight;   							//move them upwards
					Jump();														//jump animation
					_jump = false;
				}
			}
		}else
		{
			//if we have a collisionFlag and it is CollideBelow
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
			{
				airTime += Time.deltaTime;			//increase the airTime
				
				if(airTime > fallTime)				//if we have been in the air too long
				{
					Fall();						//fall animation
				}
			}
		}
		_moveDirection.y -= gravity * Time.deltaTime; //apply gravity
		
		//move the character and store any new COllision flags we get
		_collisionFlags = _controller.Move (_moveDirection * Time.deltaTime);
	}
	//reciever from sendmessage
	public void MoveMeForward(Forward z)
	{
		_forward = z;
	}
	public void RotateMe(Turn y)
	{
		_turn = y;
	}
	public void ToggleRun()
	{
		_run = !_run;
		Debug.Log (_run);
	}

	public void StrafeMe(Turn x)
	{
		_strafe = x;
	}

	public void JumpMe()
	{
		_jump = true;
		Debug.Log("Jumping");
	}
/**     
 * Below is alist of all the animations that every charater in the game can perform along with any parameters needed for them to work right
 **/
	public void Idle()
	{
		//_anim.CrossFade("idle");
	}
	public void Walk()
	{
		_anim.CrossFade("walk");
	}
	public void Strafe()
	{
		_anim.CrossFade("strafe");
	}
	public void Run()
	{
		_anim["run"].speed = 1.5f;
		_anim.CrossFade("run");
	}
	public void Jump()
	{
		_anim.CrossFade("jump");
	}
	public void Fall()
	{
		//_anim.CrossFade("Fall");
	}
}
