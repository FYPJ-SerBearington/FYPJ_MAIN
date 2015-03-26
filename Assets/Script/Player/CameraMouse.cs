using UnityEngine;
using System.Collections;

public class CameraMouse : MonoBehaviour {
	public Transform target; 							// Target to follow
	public float targetHeight= 1.7f; 						// Vertical offset adjustment
	public float distance= 12.0f;							// Default Distance
	public float offsetFromWall= 0.1f;						// Bring camera away from any colliding objects
	public float maxDistance= 20; 						// Maximum zoom Distance
	public float minDistance= 0.6f; 						// Minimum zoom Distance
	public float xSpeed= 200.0f; 							// Orbit speed (Left/Right)
	public float ySpeed= 200.0f; 							// Orbit speed (Up/Down)
	public float yMinLimit= -80; 							// Looking up limit
	public float yMaxLimit= 80; 							// Looking down limit
	public float zoomRate= 40; 							// Zoom Speed
	public float rotationDampening= 3.0f; 				// Auto Rotation speed (higher = faster)
	public float zoomDampening= 5.0f; 					// Auto Zoom speed (Higher = faster)
	LayerMask collisionLayers = -1;		// What the camera will collide with
	public bool lockToRearOfTarget= false;				// Lock camera to rear of target
	public bool allowMouseInputX= true;				// Allow player to control camera angle on the X axis (Left/Right)
	public bool allowMouseInputY= true;				// Allow player to control camera angle on the Y axis (Up/Down)
	
	private float _xDeg= 0.0f; 
	private float _yDeg= 0.0f; 
	private float _currentDistance; 
	private float _desiredDistance; 
	private float _correctedDistance; 
	private bool _rotateBehind= false;
	

	
	void  Start (){ 
		target = GameObject.FindGameObjectWithTag ("Player").transform.FindChild("head").transform;
		Vector3 angles = transform.eulerAngles; 
		_xDeg = angles.x; 
		_yDeg = angles.y; 
		_currentDistance = distance; 
		_desiredDistance = distance; 
		_correctedDistance = distance; 
		
		// Make the rigid body not change rotation 
		if (GetComponent<Rigidbody>()) 
			GetComponent<Rigidbody>().freezeRotation = true;
		
		if (lockToRearOfTarget)
			_rotateBehind = true;
	} 
	
	//Only Move camera after everything else has been updated
	void  LateUpdate (){ 
		// Don't do anything if target is not defined 
		if (!target) 
			return;
		
		Vector3 vTargetOffset;
		
		
		// If either mouse buttons are down, let the mouse govern camera position 
		if (GUIUtility.hotControl == 0)
		{
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) 
			{ 
				//Check to see if mouse input is allowed on the axis
				if (allowMouseInputX)
					_xDeg += Input.GetAxis ("Mouse X") * xSpeed * 0.02f; 
				else
					RotateBehindTarget();
				if (allowMouseInputY)
					_yDeg -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f; 
				
				//Interrupt rotating behind if mouse wants to control rotation
				if (!lockToRearOfTarget)
					_rotateBehind = false;
			} 
			
			// otherwise, ease behind the target if any of the directional keys are pressed 
			else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || _rotateBehind) 
			{
				RotateBehindTarget();
			} 
		}
		ClampAngle (_yDeg, yMinLimit, yMaxLimit); 
		
		// Set camera rotation 
		Quaternion rotation = Quaternion.Euler (_yDeg, _xDeg, 0); 
		
		// Calculate the desired distance 
		_desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (_desiredDistance); 
		_desiredDistance = Mathf.Clamp (_desiredDistance, minDistance, maxDistance); 
		_correctedDistance = _desiredDistance; 
		
		// Calculate desired camera position
		vTargetOffset =new Vector3 (0, -targetHeight, 0);
		Vector3 position = target.position - (rotation * Vector3.forward * _desiredDistance + vTargetOffset); 


		// Check for collision using the true target's desired registration point as set by user using height 
		RaycastHit collisionHit; 
		Vector3 trueTargetPosition =new Vector3 (target.position.x, target.position.y + targetHeight, target.position.z); 
				
				// If there was a collision, correct the camera position and calculate the corrected distance 
		bool isCorrected= false; 
		if (Physics.Linecast (trueTargetPosition, position,out collisionHit, collisionLayers)) 
		{ 
			// Calculate the distance from the original estimated position to the collision location,
			// subtracting out a safety "offset" distance from the object we hit.  The offset will help
			// keep the camera from being right on top of the surface we hit, which usually shows up as
			// the surface geometry getting partially clipped by the camera's front clipping plane.
			_correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - offsetFromWall; 
			isCorrected = true;
		}
		
		// For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance (TernaryOperator)
		_currentDistance = !isCorrected || _correctedDistance > _currentDistance ? Mathf.Lerp (_currentDistance, _correctedDistance, Time.deltaTime * zoomDampening) : _correctedDistance; 
		// Keep within limits
		_currentDistance = Mathf.Clamp (_currentDistance, minDistance, maxDistance); 
		
		// Recalculate position based on the new currentDistance 
		position = target.position - (rotation * Vector3.forward * _currentDistance + vTargetOffset); 
		
		//Finally Set rotation and position of camera
		transform.rotation = rotation; 
		transform.position = position; 
	} 
	
	void  RotateBehindTarget (){
		float targetRotationAngle = target.eulerAngles.y; 
		float currentRotationAngle = transform.eulerAngles.y; 
		_xDeg = Mathf.LerpAngle (currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
		
		// Stop rotating behind if not completed
		if (targetRotationAngle == currentRotationAngle)
		{
			if (!lockToRearOfTarget)
				_rotateBehind = false;
		}
		else
			_rotateBehind = true;
		
	}
	
	
	float  ClampAngle ( float angle ,   float min ,   float max  ){
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;

		return Mathf.Clamp(angle, min, max);
	}
}