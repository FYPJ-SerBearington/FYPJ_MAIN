using UnityEngine;
using System.Collections;

public class EnemyAI_FollowPlayer : MonoBehaviour {
	public Transform _target; // Enemy Target which is players
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance; // The Maximun Distance enemy is away from player before enemy starts moving towards players

	private Transform _myTransform; // instead of calling transform from the object
	//called before anything in the script
	void Awake(){
		// instead of calling transform from the object, cached it to a variable _myTransform. It will make it much faster, don't need to keep looking it up. 
		_myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");

		_target = go.transform;

		maxDistance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (_target.position, _myTransform.position, Color.yellow);

		//Look at target Rotating to target
		_myTransform.rotation = Quaternion.Slerp (_myTransform.rotation, Quaternion.LookRotation (_target.position - _myTransform.position), rotationSpeed * Time.deltaTime);
		//To make Enemy stop when is greater than the maxDistance.(Solving the rotating around player when too close)
		if (Vector3.Distance (_target.position, _myTransform.position) > maxDistance) 
		{
			// Move towards target. Moving forward in our space
			_myTransform.position += _myTransform.forward * moveSpeed * Time.deltaTime;
		}


	}
}
