using UnityEngine;
using System.Collections;

public class Boss_Phrase2Trigger : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "_Player")
		{
			GameObject.Find("Boss").GetComponent<Boss_AI>()._phrases = 2;
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
