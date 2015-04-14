using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
	public Vector3 MousePosition;
	public float CenterX;
	public float CenterY;
	// Use this for initialization
	void Start ()
	{
		CenterX = Screen.width / 2;
		CenterY = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		MousePosition = Input.mousePosition;
		if(MousePosition.x > (CenterX))
		{
			transform.rotation=Quaternion.Euler(new Vector3(0,MousePosition.x,0));
		}
		if(MousePosition.x < (CenterX))
		{
			transform.rotation=Quaternion.Euler(new Vector3(0,MousePosition.x,0));
		}
	}

}
