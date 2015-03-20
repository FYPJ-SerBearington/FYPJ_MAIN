using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour {
	public float radius = 5.0f;    //provides a radius at which the explosive will effect rigidbodies
	public float power = 10.0f;    //provides explosive power
	public float explosiveLift = 1.0f; //determines how the explosion reacts. A higher value means rigidbodies will fly upward
	public float explosiveDelay = 5.0f; //adds a delay in seconds to our explosive object
	public bool NextToTheEnemy;
	public GameObject EBodySwitch;

	void Start(){
		//StartCoroutine(Fire ());
	}
	//AudioClip explosionSound;
	void Update(){
		//StartCoroutine(Fire ());

		if (Input.GetMouseButtonDown(0) && NextToTheEnemy) {
			StartCoroutine(Fire ());
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
			//StartCoroutine(Fire ());
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			NextToTheEnemy = false;
		}
	}

	IEnumerator  Fire (){
		yield return new WaitForSeconds(explosiveDelay);
		Vector3 grenadeOrigin = transform.position;
		Collider[] colliders = Physics.OverlapSphere (grenadeOrigin, radius); //this is saying that if any collider within the radius of our object will feel the explosion
		
		foreach(Collider hit in colliders){  //for loop that says if we hit any colliders, then do the following below
			if (hit.GetComponent<Rigidbody>()){
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, grenadeOrigin, radius, explosiveLift); //if we hit any rigidbodies then add force based off our power, the position of the explosion object
				//AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1);
				//Destroy(gameObject);                        //the radius and finally the explosive lift. Afterwards destroy the game object.
			}
		}
	}
}
