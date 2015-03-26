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
		Attack,					//attack the player
		Retreat,				//retreat to spawn point
		Flee					// run to the nearest spawn point with another mob
	}
	public float perceptionRadius = 10; // range that enemy can detect character at
	public float baseMeleeRange = 2;

	public Transform target;

	private Transform _myTransform;

	private const float _ROTATION_DAMP = 0.3f;
	private const float _FORWARD_DAMP = 0.9f;

	private Transform _home;

	private State _state;
	private bool _alive = true;

	private SphereCollider _sphereCollider;

	// Use this for initialization
	void Start () {
		_state = AI.State.Init;
		StartCoroutine ("FSM");

	}

	private IEnumerator FSM(){
		while (_alive) {
			yield return null;
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

		_state = AI.State.Search;
		_alive = false;
	}
	private void Search(){
		Move ();
		_state = AI.State.Attack;
	}
	private void Attack(){
		Move ();
		_state = AI.State.Retreat;
	}
	private void Retreat(){
		_myTransform.LookAt (target);
		Move ();
		_state = AI.State.Search;
	}
	private void Flee(){
		Move ();
		_state = AI.State.Search;
	}

	private void Move() {
		if (target) {
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);

			float dist = Vector3.Distance (target.position, _myTransform.position);
	
			//almost infront of player
			if (direction > _FORWARD_DAMP && dist > baseMeleeRange) {
				SendMessage ("MoveMeForward", AdvanceMovement.Forward.forward);
			} else {
				SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);
			}


			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot (dir, transform.right);

			if (direction > _ROTATION_DAMP) {
				SendMessage ("RotateMe", AdvanceMovement.Turn.right);
			} else if (direction < -_ROTATION_DAMP) {
				SendMessage ("RotateMe", AdvanceMovement.Turn.left);
			} else {
				SendMessage ("RotateMe", AdvanceMovement.Turn.none);
			}
		} 
		else {
			SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);			
			SendMessage ("RotateMe", AdvanceMovement.Turn.none);

		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			target = other.transform;
			_alive = true;
			StartCoroutine("FSM");
		}
	}
	public void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			target = _home;
//			_alive = false;
		}
	}
}
