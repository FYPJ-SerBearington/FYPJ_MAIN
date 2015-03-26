/// <summary>
/// Player input.cs
/// Gibbie Chairul
/// 3/25/2015
/// 
/// This script that's going to capture all the player inputs
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AdvanceMovement))]
public class PlayerInput : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		//Moving forward and backward
		if (Input.GetButtonDown ("Vertical")) {
			if(Input.GetAxis("Vertical") > 0){
				SendMessage("MoveMeForward", AdvanceMovement.Forward.forward, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
			}
			else{
				SendMessage("MoveMeForward", AdvanceMovement.Forward.back, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
			}
		}
		if (Input.GetButtonUp ("Vertical")) {
			SendMessage("MoveMeForward",AdvanceMovement.Forward.none, SendMessageOptions.RequireReceiver);
		}

		//Rotating player
		if (Input.GetButtonDown ("Strafe")) {
			if(Input.GetAxis("Strafe") > 0){
				SendMessage("RotateMe", AdvanceMovement.Turn.right, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
			}
			else{
				SendMessage("RotateMe", AdvanceMovement.Turn.left, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
			}
		}
		if (Input.GetButtonUp ("Strafe")) {
			SendMessage("RotateMe",AdvanceMovement.Turn.none, SendMessageOptions.RequireReceiver);
		}

		//Strafe player
		if (Input.GetButtonDown ("Horizontal")) {
			if(Input.GetAxis("Horizontal") > 0){
				SendMessage("StrafeMe", AdvanceMovement.Turn.right, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
			}
			else{
				SendMessage("StrafeMe", AdvanceMovement.Turn.left, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
			}
		}
		if (Input.GetButtonUp ("Horizontal")) {
			SendMessage("StrafeMe",AdvanceMovement.Turn.none, SendMessageOptions.RequireReceiver);
		}

		//Jump
		if (Input.GetKeyDown(KeyCode.Space)) {
			//Debug.Log("Jumping");
			SendMessage("JumpMe");
		}
		//Run
		if (Input.GetButtonUp ("Run")) {
			SendMessage("ToggleRun");
		}

		// Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
		if(Input.GetMouseButton(1)) {
			transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
		} else {
			//SendMessage("RotateMe",AdvanceMovement.Turn.none, SendMessageOptions.RequireReceiver);
			
		}
	}
}
