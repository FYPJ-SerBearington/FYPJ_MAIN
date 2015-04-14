using UnityEngine;
using System.Collections;

public class ToyBookTrigger : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{

	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name == "ShieldBase")
		{
			Debug.Log("in");
			//play book floating and opening animation
			GameObject.Find("bookstem").GetComponent<ToyBookController>().Activated= true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "ShieldBase")
		{
			//play book floating and opening animation
			GameObject.Find("bookstem").GetComponent<ToyBookController>().Activated = false;
			//Debug.Log("out");
		}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
