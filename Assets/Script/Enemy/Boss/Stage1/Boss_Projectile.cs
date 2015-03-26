using UnityEngine;
using System.Collections;

public class Boss_Projectile : MonoBehaviour
{
	// Use this for initialization
	Vector3 dir;
	int Rnum;
	void Start ()
	{
		dir.Set (0, 0, -1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Rnum = Random.Range (19, 25);
		this.gameObject.GetComponent<Rigidbody> ().AddForce (dir * Rnum);
	}
}
