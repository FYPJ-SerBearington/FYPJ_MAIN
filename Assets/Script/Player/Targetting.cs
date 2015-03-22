/// <summary>
/// Targetting.cs
/// 1st version 3/11/2015
/// 2nd version improvised 3/20/2015
/// 
/// This script can be attached to any permanent gameobject, and is responsible for allowing the player to target different mobs with in range
/// </summary>

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
		Debug.Log ("adding all enemies target");

	}
	public void AddAllEnemies(){
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Tag_Enemy");

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
		if (targets.Count == 0)
			AddAllEnemies ();

		if (targets.Count > 0) {
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
				DeselectTarget ();
				selectedTarget = targets [index];
			}
			SelectTarget ();
		}
	}

	private void SelectTarget(){
		Transform Tname = selectedTarget.FindChild("Mob Name");//

		if (name == null) {
			Debug.LogError("Could not find the Name on " + selectedTarget.name);
			return;
		}

		Tname.GetComponent<TextMesh> ().text = selectedTarget.GetComponent<MobUI> ().CName;
		Tname.GetComponent<MeshRenderer> ().enabled = true;

		selectedTarget.GetComponent<MobUI> ().DisplayHealth();

		Messenger<bool>.Broadcast("show mob vital bars", true);
	}
	private void DeselectTarget(){
		selectedTarget.FindChild ("Mob Name").GetComponent<MeshRenderer> ().enabled = false;
		selectedTarget = null;

		Messenger<bool>.Broadcast("show mob vital bars", false);
	}
	// Update is called once per frame
	void Update () {
		if (  Input.GetKeyDown (KeyCode.Tab)) {
			TargetEnemy();
		}
	}

}
