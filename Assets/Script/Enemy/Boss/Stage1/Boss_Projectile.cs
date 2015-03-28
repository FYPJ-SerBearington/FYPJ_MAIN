using UnityEngine;
using System.Collections;

public class Boss_Projectile : MonoBehaviour
{
	// Use this for initialization
	Vector3 dir;
	public int Rnum;
	public int damage;
	void Start ()
	{
		damage = 10;
		dir.Set (0, 0, -1);
		Rnum = Random.Range (19, 25);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.GetComponent<Rigidbody> ().AddForce (dir * Rnum);
	}
}
