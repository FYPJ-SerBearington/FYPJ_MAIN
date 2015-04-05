using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeCycler : MonoBehaviour {

	List<GameObject> Enemies; //List of enemies in range

	// Use this for initialization
	void Start () {
		Enemies = new List<GameObject> ();

		
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Tag_Enemy");
		foreach (GameObject enemy in go) {
			if((enemy.transform.position - gameObject.transform.position).magnitude <= 15) {
				Enemies.Add(enemy);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] go = GameObject.FindGameObjectsWithTag ("Tag_Enemy");

		foreach (GameObject enemy in go) {
			bool enemyInRange = false;
			foreach(GameObject inrange in Enemies) {
				if(enemy == inrange) enemyInRange = true;
			}

			if(enemyInRange) enemy.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
			else enemy.GetComponent<Renderer>().material.color = new Color(0, 0, 1);
		}

		
		if (  Input.GetKeyDown (KeyCode.Tab)) {
			CycleEnemy();
		}
	}

	void CycleEnemy() {

	}

	void OnTriggerEnter (Collider col) {
		Enemies.Add (col.gameObject);
	}

	void OnTriggerExit (Collider col) {
		Enemies.Remove (col.gameObject);
	}
}
