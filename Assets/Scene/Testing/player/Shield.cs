using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Shield : MonoBehaviour
{
	public Transform LeftHandBone;
	// Use this for initialization
	public GameObject shield;
	void Start ()
	{
		this.transform.parent = LeftHandBone.transform;
	}
	void Update()
	{
	}

}
