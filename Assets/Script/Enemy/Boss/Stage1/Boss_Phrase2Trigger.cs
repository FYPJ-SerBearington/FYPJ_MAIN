using UnityEngine;
using System.Collections;

public class Boss_Phrase2Trigger : MonoBehaviour
{
	public bool trigger=false;
	// Use this for initialization
	void Start ()
	{

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "ShieldBase")
		{
			trigger = true;
		}

	}
	// Update is called once per frame
	void Update ()
	{
		if(trigger)
		{
			GameObject.Find("Boss").GetComponent<Boss_AI>()._phrases = 2;
			Destroy(this.gameObject);
		}
	}
}
