/// <summary>
/// Player input.cs
/// Gibbie Chairul
/// 3/25/2015
/// 
/// This script that's going to capture all the player inputs
/// 
/// keys
/// Toggle Inventory				-Open and close the inventory window - I
/// Toggle Character Window 		-Opend and close the character information window - C
/// Move Forward					-to move the character move side or backwards - W/S
/// Rotate Player					-to have the character move die to side - Q/E
/// strafe							-to have the character move side to side = A/D
/// jump							- Button to use to make the character jump - SPACE
/// Run								- Toggle to use to have the character run or walk - SHIFT
/// </summary>

using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/All Player Scripts")]
[RequireComponent(typeof(AdvanceMovement))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerInput : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.C)) {
			Messenger.Broadcast("ToggleCharacter");
		}
		if (Input.GetKeyUp (KeyCode.I)) {
			Messenger.Broadcast("ToggleInventory");
		}
		//Moving forward and backward
		if (Input.GetKeyDown (KeyCode.W)) {
			//if(Input.GetAxis("Vertical") > 0){
				SendMessage("MoveMeForward", AdvanceMovement.Forward.forward, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
		}
		else if (Input.GetKeyDown (KeyCode.S)){
				SendMessage("MoveMeForward", AdvanceMovement.Forward.back, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
		}

		//}
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S)) {
			SendMessage("MoveMeForward",AdvanceMovement.Forward.none, SendMessageOptions.RequireReceiver);
		}

		//Rotating player
		if (Input.GetKeyDown (KeyCode.E)) {
			SendMessage("RotateMe", AdvanceMovement.Turn.right, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
		}
		else if (Input.GetKeyDown (KeyCode.Q)){
			SendMessage("RotateMe", AdvanceMovement.Turn.left, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
		}

		if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q)) {
			SendMessage("RotateMe",AdvanceMovement.Turn.none, SendMessageOptions.RequireReceiver);
		}

		//Strafe player
		if (Input.GetKeyDown (KeyCode.D)) {
				SendMessage("StrafeMe", AdvanceMovement.Turn.right, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.forward);
			}
		else if (Input.GetKeyDown (KeyCode.A)){
				SendMessage("StrafeMe", AdvanceMovement.Turn.left, SendMessageOptions.RequireReceiver);
				//Debug.Log(AdvanceMovement.Forward.back);
			}


		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
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
