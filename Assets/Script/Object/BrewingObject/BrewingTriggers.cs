using UnityEngine;
using System.Collections;

public class BrewingTriggers : MonoBehaviour
{
	public bool trigger = false;
	// Use this for initialization
	void Start ()
	{
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "ShieldBase")
		{
			trigger = true;
		}
	}
	void OnTriggerExit(Collider other)
	{

	}
	// Update is called once per frame
	void Update ()
	{
		if(trigger)
		{
			GameObject.Find("pot").GetComponent<Brewing_Controller>().start = true;
			trigger = false;
		}
	}
}
