using UnityEngine;
using System.Collections;

public class ladder : MonoBehaviour {
	private PlayerController _PC;
	public bool inside = false;
	public Transform OnController;
	public float heightFactor= 3.2f;
	void Start(){
		_PC = GetComponent<PlayerController>();
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			return;
		}
		if (other.gameObject.tag == "Ladder") {
			_PC.enabled = false;
			inside =  !inside;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			return;
		}
		if (other.gameObject.tag == "Ladder") {
			_PC.enabled = true;
			inside =  !inside;
		}
	}

	void Update(){
		if(inside == true && Input.GetKey(KeyCode.W))
			OnController.transform.position += Vector3.up / heightFactor;
		if(inside == true && Input.GetKey(KeyCode.S))
			OnController.transform.position -= Vector3.up / heightFactor;
	}
}
