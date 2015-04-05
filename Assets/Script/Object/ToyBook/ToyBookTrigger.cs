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
		if (other.name == "_Player" || other.name == "bear model"||other.name == "_ _Ser Bearington")
		{
			//play book floating and opening animation
			GameObject.Find("bookstem").GetComponent<ToyBookController>().Activated= true;
			GameObject.Find("DialogueCanvas").GetComponent<Boss_DialogueManager>().startDialogue();
			//Debug.Log("in");
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.name == "_Player" || other.name == "bear model"||other.name == "_ _Ser Bearington")
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
