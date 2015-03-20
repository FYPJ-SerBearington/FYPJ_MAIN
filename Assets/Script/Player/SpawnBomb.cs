using UnityEngine;
using System.Collections;

public class SpawnBomb : MonoBehaviour
{
	public Rigidbody grenade;
	public float ThrowPower = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Rigidbody clone;
			clone = Instantiate(grenade,transform.position,transform.rotation)as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * ThrowPower);
		}
	}
}
