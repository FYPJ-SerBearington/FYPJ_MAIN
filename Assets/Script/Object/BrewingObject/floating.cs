using UnityEngine;
using System.Collections;

public class floating : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Potion_weak")
		{
			Debug.Log ("hit");
			other.GetComponent<Potion>().inwater =true;
		}
	}
	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
