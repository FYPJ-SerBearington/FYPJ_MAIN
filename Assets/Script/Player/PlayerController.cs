
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float jumpSpeed = 15.0f;
	public float gravity = 32.0f;
	public float runSpeed = 15.0f;
	public float walkSpeed = 45.0f;
	public float rotateSpeed = 150.0f;
	
	private bool  grounded = false;
	private Vector3 moveDirection = Vector3.zero;
	private bool  isWalking = false;
	private string moveStatus = "idle";
	

	static bool  dead = false;
	void Start ()
	{
		//GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ().changestage(1);
		//this.gameObject.GetComponent<GameManager> ().SendMessage ("changestage", jumpSpeed);
	}
	void  Update ()
	{
		
		if(dead == false) {
			
			//animation.Play("idle");
			// Only allow movement and jumps while grounded
			if(grounded) {
				//animation.Play("idle");
				//animation.CrossFade("Locomotion");
				moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0),0,Input.GetAxis("Vertical"));
				
				// if moving forward and to the side at the same time, compensate for distance
				// TODO: may be better way to do this?
				if(Input.GetMouseButton(1)) {
					moveDirection *= 0.7f;

					
				}
				
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= isWalking ? walkSpeed : runSpeed;
				
				moveStatus = "idle";
				if(moveDirection != Vector3.zero)
				//{
					moveStatus = isWalking ? "walking" : "running";
					//animation.Play("Walk");
						//anim.SetFloat("wal;k",5);
				//}
				
				// Jump!
				//if(Input.GetButton("Jump"))
				
				if (Input.GetKeyDown(KeyCode.Space)){
					//animation.Play("jump");
					moveDirection.y = jumpSpeed;
				}
			}
			
			// Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
			if(Input.GetMouseButton(1)) {
				transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
			} else {
				transform.Rotate(0,Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
				
			}
			
			
			
			
			
			
			// Toggle walking/running with the T key
			if(Input.GetKeyDown("t"))
			isWalking = !isWalking;
			
			
			
			
			//Apply gravity
			moveDirection.y -= gravity * Time.deltaTime;
			
			
			//Move controller
			CharacterController controller = GetComponent<CharacterController>();
			 //controller.Move(moveDirection * Time.deltaTime);
			grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
			
			
		}
		
		
		if(Input.GetMouseButton(1) || Input.GetMouseButton(0)) {
			//Screen.lockCursor = true;
			
			//Cursor.visible = false; 
			
			
			//float mouse1= Input.mousePosition.y;
			//float mouse2= Input.mousePosition.x;
			
		}
		
		//Vector3 mousePos = Input.mousePosition;
		else  {
			//Screen.lockCursor = false;
			//Cursor.visible = false; 
			
			//Input.mousePosition.y = mouse1;
			//Input.mousePosition.x = mouse2;
			
			//Input.mousePosition = mousePos;
			
		}
		
		
	}
}