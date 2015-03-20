using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Targetting : MonoBehaviour {
	public List<Transform> targets;
	public Transform selectedTarget;
	public bool EnemyWithinRange = false;
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;

		AddAllEnemies ();

	}

	public void AddAllEnemies(){
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in go) {
			AddTarget(enemy.transform);
		}
	}

	public void AddTarget(Transform enemy){
		targets.Add (enemy);
	}
	private void SortTagetsByDistance(){
		targets.Sort (delegate(Transform t1, Transform t2){ 
			return Vector3.Distance (t1.position, myTransform.position).CompareTo (Vector3.Distance (t2.position, myTransform.position));
		});
	}
	private void TargetEnemy(){
		//selectedTarget = targets [0];

		if (selectedTarget == null) {
			SortTagetsByDistance ();
			selectedTarget = targets [0];
		} else {
			int index = targets.IndexOf (selectedTarget);

			if (index < targets.Count - 1) {
				index++;
			} else {
				index = 0;
			}
			DeselectTarget();
			selectedTarget = targets [index];
		}
		SelectTarget();
	}

	private void SelectTarget(){
		selectedTarget.GetComponent<Renderer>().material.color = Color.blue;
	}
	private void DeselectTarget(){
		selectedTarget.GetComponent<Renderer>().material.color = Color.red;
		selectedTarget = null;
	}
	// Update is called once per frame
	void Update () {
		if (  Input.GetKeyDown (KeyCode.Tab)) {
			TargetEnemy();
		}
	}
//	void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "Ladder") {
//			return;
//		}
//		if (other.gameObject.tag == "Enemy" ) {
//			AddAllEnemies();
//			EnemyWithinRange = true;
//		}
//
//	}
//	void OnTriggerExit(Collider other){
//		if (other.gameObject.tag == "Enemy" ) {
//			EnemyWithinRange = false;
//			targets.Clear();
//			Debug.Log("No Enemy");
//		}
//	}
}
