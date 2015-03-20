using UnityEngine;
using System.Collections;

public class Boss_Blocky : MonoBehaviour
{
	public float t;
	public float DestoryT;
	// Use this for initialization
	void Start ()
	{
		t = 0;
		DestoryT = 8.0f;
	}
//	void OnTriggerEnter(Collider other)
//	{
//		if(other.tag == "platform")
//		{
//			other.GetComponent<platform>().Cstate = 1;
//		}
//	}
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Tag_B1_platform")
		{
			collision.gameObject.GetComponent<Object_Platform>().Cstate = 1;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		t += Time.deltaTime;
		if (t >= DestoryT)
		{
			Destroy(this.gameObject);
		}
	}
}
