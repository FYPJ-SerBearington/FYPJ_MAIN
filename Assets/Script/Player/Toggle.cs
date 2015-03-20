using UnityEngine;
using System.Collections;

public class Toggle : MonoBehaviour {
	public GameObject Switch;
	public bool NextToTheSwitch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && NextToTheSwitch) {
			if(Switch.GetComponent<Light>().enabled){
				Switch.GetComponent<Light>().enabled = false;
			}
			else{
				Switch.GetComponent<Light>().enabled = true;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			NextToTheSwitch = true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			NextToTheSwitch = false;
		}
	}
}
