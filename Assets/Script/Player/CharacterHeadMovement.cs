using UnityEngine;
using System.Collections;

public class CharacterHeadMovement : MonoBehaviour {
	public float speed;
	public Transform headTransform;

	private Transform _myTransform;

	// Use this for initialization
	void Start () {
	
		_myTransform = headTransform;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void LateUpdate(){
		if (GUIUtility.hotControl == 0)
		{
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) 
			{ 
				_myTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * speed);
			} 
			 
		}
	}
}
