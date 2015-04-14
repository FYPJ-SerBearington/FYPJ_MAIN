using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Transform RightHandBone;
	// Use this for initialization
	public GameObject weapon;
	bool test;
	void Start ()
	{
		this.transform.parent = RightHandBone.transform;

	}
	void Update()
	{

	}
}
