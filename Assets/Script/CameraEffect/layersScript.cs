using UnityEngine;
using System.Collections;

public class layersScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		//8 = triggers 9 = player
		//10 = floor 11 = weapon
		Physics.IgnoreLayerCollision(8,9);
		Physics.IgnoreLayerCollision(9,11);
		Physics.IgnoreLayerCollision(10,11);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
