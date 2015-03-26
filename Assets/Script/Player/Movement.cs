using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public float moveSpeed = 8;			//the speed our character walks at
	public float runMultiplier = 10; 	//how fast the player can run
	public float rotateSpeed = 250;		//the rotation of our player
	public float strafeSpeed = 2.5f; 		//the speed of our character strafes at 

	private Transform _myTransform;				//our cached transform
	private CharacterController _controller;	//our chaced CharacterController

	public void Awake(){
		_myTransform = transform;
		_controller = GetComponent<CharacterController> ();

	}

	void Start(){
		GetComponent<Animation> ().wrapMode = WrapMode.Loop;
		GetComponent<Animation> ()["Idle"].wrapMode = WrapMode.Loop;
	}

	void Update(){
		if (!_controller.isGrounded) {
			_controller.Move(Vector3.down *Time.deltaTime);
		}
		Turn ();
		Walk ();
		Strafe ();

	}

	private void Turn(){
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0) {
			_myTransform.Rotate(0,Input.GetAxis("Horizontal") * Time.deltaTime *rotateSpeed, 0);

		}
	}
	private void Walk(){
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0) {
			if(Input.GetButton("Run")){
				Debug.Log("Running");
				//GetComponent<Animation>().CrossFade("Run");
				//GetComponent<Animation>()["Walk"].speed = 2;
				_controller.SimpleMove (_myTransform.TransformDirection (Vector3.forward) * Input.GetAxis ("Vertical") * moveSpeed * runMultiplier);
			}
			else 
			{
				Debug.Log("Walk");
			//GetComponent<Animation>().CrossFade("Walk");
			//GetComponent<Animation>()["Walk"].speed = 2;
			_controller.SimpleMove (_myTransform.TransformDirection (Vector3.forward) * Input.GetAxis ("Vertical") * moveSpeed);
			}
		} 
		else {
			//GetComponent<Animation>().CrossFade("Idle");
		}
	}

	private void Strafe(){
		if (Mathf.Abs (Input.GetAxis ("Strafe")) > 0) {
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * strafeSpeed);
		}
	}
}
