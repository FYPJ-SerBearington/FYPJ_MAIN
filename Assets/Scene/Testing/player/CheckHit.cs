using UnityEngine;
using System.Collections;

public class CheckHit : MonoBehaviour
{
	public GameObject main;
	public int Damage;
	void OnCollisionEnter(Collision other)
	{
		if(main.GetComponent<BearController>().isAttacking)
		{
			Debug.Log (other.gameObject.name);
			if(other.gameObject.name == "Mob")
			{
				other.gameObject.GetComponent<MobStatus>().Health -= Damage;
			}
		}
	}
//	void OnTriggerEnter(Collider other)
//	{
//		if(main.GetComponent<BearController>().isAttacking)
//		{
//			Debug.Log (other.gameObject.name);
//			if(other.name == "Mob")
//			{
//				other.GetComponent<MobStatus>().Health -= Damage;
//			}
//		}
//		//Debug.Log (other.gameObject.name);
//	}

	// Use this for initialization
	void Start ()
	{
		main = GameObject.Find ("Player_Bear");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
