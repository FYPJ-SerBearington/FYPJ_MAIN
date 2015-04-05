using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AdvanceMovement))]
[RequireComponent(typeof(SphereCollider))]

public class AI : MonoBehaviour {
	private enum State
	{
		Init,					//Do nothing
		Idle,					//Make sure that everything we need is here
		Setup,					//assign the values to the things we need
		Search,					//find the player
		Decide,					//Decide what to do with the targetter player
		Attack,					//attack the player
		Retreat,				//retreat to spawn point
		Flee					// run to the nearest spawn point with another mob
	}
	public float perceptionRadius = 10; // range that enemy can detect character at
	public float baseMeleeRange = 2;

	private Transform _target;

	private Transform _myTransform;

	private const float _ROTATION_DAMP = 0.3f;
	private const float _FORWARD_DAMP = 0.9f;

	private Transform _home;

	private State _state;

	private SphereCollider _sphereCollider;

	// Use this for initialization
	void Start () {
		_state = AI.State.Init;
		StartCoroutine ("FSM");

	}

	private IEnumerator FSM(){
		while (_state != AI.State.Idle) {
			switch(_state){
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Search:
				Search();
				break;
			case State.Decide:
				Decide();
				break;
			case State.Attack:
				Attack();
				break;
			case State.Retreat:
				Retreat();
				break;
			case State.Flee:
				Flee();
				break;
			}
			yield return null;
		}
	}

	private void Init(){
		_myTransform = transform;
		_home = transform.parent.transform;
		_sphereCollider = GetComponent<SphereCollider> ();

		if (_sphereCollider == null) {
			Debug.LogError("SphereCollider Not Present");
			return;
		}
		_state = AI.State.Setup;
	}

	private void Setup(){
		_sphereCollider.center = GetComponent<CharacterController> ().center;
		_sphereCollider.radius = perceptionRadius;
		_sphereCollider.isTrigger = true;

		_state = AI.State.Idle;
	}

	private void Search(){
		if (_target == null) {
		//	Debug.Log ("#Target is null#");
			_state = AI.State.Idle;
		} else {
		//	Debug.Log ("########Serching#######");
			_state = AI.State.Decide;
		}
	}

	private void Decide(){
		//Debug.Log ("########Decide#######");


		_state = AI.State.Search;
	}

	private void Attack(){
		//Debug.Log ("########Attack#######");
		Move ();
		_state = AI.State.Retreat;
	}

	private void Retreat(){
		//Debug.Log ("########Retreat#######");
		_myTransform.LookAt (_target);
		Move ();
		_state = AI.State.Search;
	}

	private void Flee(){
		Move ();
		_state = AI.State.Search;
	}

	private void Move() {
		//Debug.Log ("########Move#######");
		if (_target) {
			float dist = Vector3.Distance (_target.position, _myTransform.position);
			if(_target.name == "Spawn Point"){
			//	Debug.LogWarning("Returning home" + dist);

				if(dist < baseMeleeRange){
					_target = null;
					_state = AI.State.Idle;

					SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);			
					SendMessage ("RotateMe", AdvanceMovement.Turn.none);
					return;
				}
			}
			//Will need to incoporate this new turning code in to the advanced movement script
			//one thing will have to do is lock the x axis so we do not tilt up or down
			Quaternion rot = Quaternion.LookRotation(_target.transform.position - _myTransform.position);
			_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rot, Time.deltaTime * 5.0f);

			Vector3 dir = (_target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);


	
			//almost infront of player
			if (direction > _FORWARD_DAMP && dist > baseMeleeRange) {
				SendMessage ("MoveMeForward", AdvanceMovement.Forward.forward);
			} else {
				SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);
			}


//			dir = (_target.position - _myTransform.position).normalized;
//			direction = Vector3.Dot (dir, transform.right);
//
//			if (direction > _ROTATION_DAMP) {
//				SendMessage ("RotateMe", AdvanceMovement.Turn.right);
//			} else if (direction < -_ROTATION_DAMP) {
//				SendMessage ("RotateMe", AdvanceMovement.Turn.left);
//			} else {
//				SendMessage ("RotateMe", AdvanceMovement.Turn.none);
//			}
		} 
		else {
			SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);			
			SendMessage ("RotateMe", AdvanceMovement.Turn.none);

		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			_target = other.transform;
			_state = AI.State.Search;
			StartCoroutine("FSM");
		}
	}
	public void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			_target = _home;
		}
	}
}
