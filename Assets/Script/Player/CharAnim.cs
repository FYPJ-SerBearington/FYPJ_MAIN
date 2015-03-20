using UnityEngine;
using System.Collections;

public class CharAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animation>().CrossFade("idle");
		if(Input.GetKeyDown("w"))
		   {
			GetComponent<Animation>().Play ("Walk");
		}
	}
}
