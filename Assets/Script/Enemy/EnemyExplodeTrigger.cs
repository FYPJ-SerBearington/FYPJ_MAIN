using UnityEngine;
using System.Collections;

public class EnemyExplodeTrigger : MonoBehaviour {
	public Rigidbody grenade;
	public float ThrowPower = 10.0f;
	public bool NextToTheEnemy;
	public GameObject EBodySwitch;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//Rigidbody clone;
			Rigidbody clone = Instantiate(grenade,transform.position,transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * ThrowPower);

			if(EBodySwitch.GetComponent<Light>().enabled){
				EBodySwitch.GetComponent<Light>().enabled = false;
			}
			else{
				EBodySwitch.GetComponent<Light>().enabled = true;
			}

		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			NextToTheEnemy = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			NextToTheEnemy = false;
		}
	}
}
